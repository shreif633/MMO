using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace SimpleABC
{
    public partial class AssetBundleManager : MonoBehaviour
    {
        public enum LoadMode
        {
            None,
            FromServerUrl,
            FromLocalPath,
        }

        public enum LoadState
        {
            None,
            LoadManifest,
            LoadAssetBundles,
            Done,
        }

        [Serializable]
        public struct AssetBundleSetting
        {
            public string overrideServerUrl;
            public string overrideLocalFolderPath;
            public LoadMode overrideLoadMode;
            public string platformFolderName;
        }

        [SerializeField]
        public struct AssetBundleInfo
        {
            public string url;
            public Hash128 hash;
        }

        public static AssetBundleManager Singleton { get; private set; }

        [SerializeField]
        protected string serverUrl = "http://localhost/AssetBundles";
        [SerializeField]
        protected string localFolderPath = "AssetBundles/";
        [SerializeField]
        protected string initSceneName = "init";
        [SerializeField]
        protected float delayBeforeLoadInitScene = 0.5f;
        [SerializeField]
        protected LoadMode loadMode = LoadMode.FromServerUrl;
        [SerializeField]
        protected LoadMode editorLoadMode = LoadMode.FromLocalPath;
        [SerializeField]
        protected bool loadAssetBundleOnStart = true;
        [SerializeField]
        protected AssetBundleSetting androidSetting = new AssetBundleSetting()
        {
            platformFolderName = "Android",
        };
        [SerializeField]
        protected AssetBundleSetting iosSetting = new AssetBundleSetting()
        {
            platformFolderName = "iOS",
        };
        [SerializeField]
        protected AssetBundleSetting windowsSetting = new AssetBundleSetting()
        {
            platformFolderName = "StandaloneWindows64",
        };
        [SerializeField]
        protected AssetBundleSetting osxSetting = new AssetBundleSetting()
        {
            platformFolderName = "StandaloneOSXIntel64",
        };
        [SerializeField]
        protected AssetBundleSetting linuxSetting = new AssetBundleSetting()
        {
            platformFolderName = "StandaloneLinux64",
        };
        [SerializeField]
        protected AssetBundleSetting serverSetting = new AssetBundleSetting()
        {
            platformFolderName = "StandaloneLinux64",
            overrideLoadMode = LoadMode.FromLocalPath,
        };
        public UnityEvent onManifestLoaded = new UnityEvent();
        public UnityEvent onManifestLoadedFail = new UnityEvent();
        public UnityEvent onAssetBundlesLoaded = new UnityEvent();
        public UnityEvent onAssetBundlesLoadedFail = new UnityEvent();
        [SerializeField]
        protected bool clearCache = false;

        public LoadMode CurrentLoadMode
        {
            get
            {
                LoadMode currentLoadMode = loadMode;
                if (Application.isEditor)
                    currentLoadMode = editorLoadMode;
                else if (CurrentSetting.overrideLoadMode != LoadMode.None)
                    currentLoadMode = CurrentSetting.overrideLoadMode;
                return currentLoadMode;
            }
        }
        public LoadState CurrentLoadState { get; protected set; } = LoadState.None;
        public int LoadingAssetBundlesCount { get; protected set; } = 0;
        public int LoadingAssetBundlesFromCacheCount { get; protected set; } = 0;
        public int LoadedAssetBundlesCount { get; protected set; } = 0;
        public long LoadingAssetBundleFileSize { get; protected set; } = 0;
        public double LoadingSpeedPerSeconds { get; protected set; } = 0;
        public double LoadingRemainingSeconds { get; protected set; } = 0;
        public float TotalLoadProgress { get { return LoadedAssetBundlesCount == LoadingAssetBundlesCount ? 1f : ((float)LoadedAssetBundlesCount / (float)LoadingAssetBundlesCount); } }
        public string LoadingAssetBundleFileName { get; protected set; }
        public AssetBundleSetting CurrentSetting { get; protected set; }
        public string ServerUrl { get { return !string.IsNullOrEmpty(CurrentSetting.overrideServerUrl) ? CurrentSetting.overrideServerUrl : serverUrl; } }
        public string LocalFolderPath { get { return !string.IsNullOrEmpty(CurrentSetting.overrideLocalFolderPath) ? CurrentSetting.overrideLocalFolderPath : localFolderPath; } }
        public Dictionary<string, AssetBundle> Dependencies { get; private set; } = new Dictionary<string, AssetBundle>();
        public UnityWebRequest CurrentWebRequest { get; protected set; }

        private bool _tempErrorOccuring = false;
        private AssetBundle _tempAssetBundle = null;
        private readonly Dictionary<string, AssetBundleInfo> _loadingAssetBundles = new Dictionary<string, AssetBundleInfo>();
        private float _downloadProgressFromLastOneSeconds = 0f;
        private float _sumDeltaProgress = 0f;
        private float _oneSecondsCounter = 0f;
        private float _secondsCount = 0;

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
            {
                CurrentSetting = serverSetting;
            }
            else
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        CurrentSetting = androidSetting;
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        CurrentSetting = iosSetting;
                        break;
                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.WindowsPlayer:
                        CurrentSetting = windowsSetting;
                        break;
                    case RuntimePlatform.OSXEditor:
                    case RuntimePlatform.OSXPlayer:
                        CurrentSetting = osxSetting;
                        break;
                    case RuntimePlatform.LinuxEditor:
                    case RuntimePlatform.LinuxPlayer:
                        CurrentSetting = linuxSetting;
                        break;
                }
            }
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (clearCache)
                Caching.ClearCache();
#endif
            if (loadAssetBundleOnStart)
                LoadAssetBundle();
        }

        private void Update()
        {
            if (CurrentLoadState != LoadState.LoadAssetBundles)
                return;
            if (string.IsNullOrEmpty(LoadingAssetBundleFileName))
                return;
            if (CurrentWebRequest == null || CurrentWebRequest.isDone)
                return;
            _oneSecondsCounter += Time.deltaTime;
            if (_oneSecondsCounter >= 1f)
            {
                _secondsCount += _oneSecondsCounter;
                _oneSecondsCounter = 0f;
                float deltaProgress = CurrentWebRequest.downloadProgress - _downloadProgressFromLastOneSeconds;
                _sumDeltaProgress += deltaProgress;
                LoadingSpeedPerSeconds = (_sumDeltaProgress / _secondsCount) * LoadingAssetBundleFileSize;
                LoadingRemainingSeconds = (1 - CurrentWebRequest.downloadProgress) / (_sumDeltaProgress / _secondsCount);
                _downloadProgressFromLastOneSeconds = CurrentWebRequest.downloadProgress;
            }
        }

        public void LoadAssetBundle()
        {
            switch (CurrentLoadMode)
            {
                case LoadMode.FromServerUrl:
                    LoadAssetBundleFromServer();
                    break;
                case LoadMode.FromLocalPath:
                    LoadAssetBundleFromLocalFolder();
                    break;
            }
        }

        public void LoadAssetBundleFromServer()
        {
            StartCoroutine(LoadAssetBundlesFromUrlRoutine(ServerUrl));
        }

        public void LoadAssetBundleFromLocalFolder()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    StartCoroutine(LoadAssetBundlesFromUrlRoutine($"file:///{Path.GetFullPath(".")}/{LocalFolderPath}"));
                    break;
                default:
                    StartCoroutine(LoadAssetBundlesFromUrlRoutine($"{Path.GetFullPath(".")}/{LocalFolderPath}"));
                    break;
            }
        }

        private bool IsWebRequestLoadedFail(string key, UnityEvent evt)
        {
            if (WebRequestIsError(CurrentWebRequest))
            {
                OnAssetBundleLoadedFail(key, evt);
                return true;
            }
            return false;
        }

        public bool WebRequestIsError(UnityWebRequest unityWebRequest)
        {
#if UNITY_2020_2_OR_NEWER
            UnityWebRequest.Result result = unityWebRequest.result;
            return (result == UnityWebRequest.Result.ConnectionError)
                || (result == UnityWebRequest.Result.DataProcessingError)
                || (result == UnityWebRequest.Result.ProtocolError);
#else
            return unityWebRequest.isHttpError || unityWebRequest.isNetworkError;
#endif
        }

        private void OnAssetBundleLoadedFail(string key, UnityEvent evt, string error = "")
        {
            _tempErrorOccuring = true;
            evt.Invoke();
            CurrentLoadState = LoadState.None;
            string logError = error;
            if (!string.IsNullOrEmpty(CurrentWebRequest.error))
                logError = CurrentWebRequest.error;
            Debug.LogError($"[AssetBundleManager] Load {key} from {CurrentWebRequest.url}, error: {logError}");
        }

        private IEnumerator GetAssetBundleSize(string url, string loadKey)
        {
            UnityWebRequest webRequest = UnityWebRequest.Head(url);
            yield return webRequest.SendWebRequest();
            string contentLength = webRequest.GetResponseHeader("Content-Length");
            if (WebRequestIsError(webRequest))
                LoadingAssetBundleFileSize = -1;
            else
                LoadingAssetBundleFileSize = Convert.ToInt64(contentLength);
            Debug.Log($"[AssetBundleManager] Get Asset Bundle Size {loadKey} Content-Length: {LoadingAssetBundleFileSize}");
        }

        private IEnumerator LoadAssetBundleFromUrlRoutine(string url, string loadKey, UnityEvent successEvt, UnityEvent errorEvt, Hash128? hash)
        {
            Debug.Log($"[AssetBundleManager] Load {loadKey}");
            // Get asset bundle size
            yield return StartCoroutine(GetAssetBundleSize(url, loadKey));
            // Create request to get asset bundle from cache or download from server
            if (hash.HasValue)
                CurrentWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(url, hash.Value);
            else
                CurrentWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(url);
            _downloadProgressFromLastOneSeconds = 0f;
            _sumDeltaProgress = 0f;
            _oneSecondsCounter = 0f;
            _secondsCount = 0;
            LoadingSpeedPerSeconds = 0f;
            LoadingRemainingSeconds = float.PositiveInfinity;
            // Send request
            yield return CurrentWebRequest.SendWebRequest();
            // Error handling
            if (IsWebRequestLoadedFail(loadKey, errorEvt))
                yield break;
            _tempAssetBundle = DownloadHandlerAssetBundle.GetContent(CurrentWebRequest);
            if (_tempAssetBundle == null)
            {
                OnAssetBundleLoadedFail(loadKey, errorEvt, "No Asset Bundle");
                yield break;
            }
            // Done
            successEvt.Invoke();
            Debug.Log($"[AssetBundleManager] Load {loadKey} done");
        }

        private IEnumerator LoadAssetBundlesFromUrlRoutine(string url)
        {
            Dependencies.Clear();
            string downloadingUrl;
            Hash128 downloadHash;
            bool isCached;
            _tempErrorOccuring = false;
            _tempAssetBundle = null;
            // Load platform's manifest to get all asset bundles
            CurrentLoadState = LoadState.LoadManifest;
            downloadingUrl = UriAppend(new Uri(url), CurrentSetting.platformFolderName, CurrentSetting.platformFolderName).AbsoluteUri;
            yield return StartCoroutine(LoadAssetBundleFromUrlRoutine(downloadingUrl, "manifest", onManifestLoaded, onManifestLoadedFail, null));
            if (_tempErrorOccuring)
                yield break;
            // Read platform's manifest to get all asset bundles info
            AssetBundleManifest manifest = _tempAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] assetBundles = manifest.GetAllAssetBundles();
            LoadingAssetBundlesCount = 0;
            LoadingAssetBundlesFromCacheCount = 0;
            foreach (string assetBundle in assetBundles)
            {
                string[] dependencies = manifest.GetAllDependencies(assetBundle);
                foreach (string dependency in dependencies)
                {
                    downloadingUrl = UriAppend(new Uri(url), CurrentSetting.platformFolderName, dependency).AbsoluteUri;
                    downloadHash = manifest.GetAssetBundleHash(dependency);
                    if (!_loadingAssetBundles.ContainsKey(dependency))
                    {
                        // Unity 2022+ has no `Caching` for WebGL
#if !UNITY_WEBGL
                        isCached = Caching.IsVersionCached(downloadingUrl, downloadHash);
#else
                        isCached = false;
#endif
                        _loadingAssetBundles.Add(dependency, new AssetBundleInfo()
                        {
                            url = downloadingUrl,
                            hash = downloadHash,
                        });
                        if (!isCached)
                            LoadingAssetBundlesCount++;
                        else
                            LoadingAssetBundlesFromCacheCount++;
                    }
                }
                downloadingUrl = UriAppend(new Uri(url), CurrentSetting.platformFolderName, assetBundle).AbsoluteUri;
                downloadHash = manifest.GetAssetBundleHash(assetBundle);
                if (!_loadingAssetBundles.ContainsKey(assetBundle))
                {
                    // Unity 2022+ has no `Caching` for WebGL
#if !UNITY_WEBGL
                    isCached = Caching.IsVersionCached(downloadingUrl, downloadHash);
#else
                    isCached = false;
#endif
                    _loadingAssetBundles.Add(assetBundle, new AssetBundleInfo()
                    {
                        url = downloadingUrl,
                        hash = downloadHash,
                    });
                    if (!isCached)
                        LoadingAssetBundlesCount++;
                    else
                        LoadingAssetBundlesFromCacheCount++;
                }
            }
            // Load all asset bundles
            LoadedAssetBundlesCount = 0;
            CurrentLoadState = LoadState.LoadAssetBundles;
            foreach (KeyValuePair<string, AssetBundleInfo> loadingAssetBundle in _loadingAssetBundles)
            {
                LoadingAssetBundleFileName = string.Empty;
                LoadingAssetBundleFileName = loadingAssetBundle.Key;
                yield return StartCoroutine(LoadAssetBundleFromUrlRoutine(loadingAssetBundle.Value.url, $"dependency: {loadingAssetBundle.Key}", onAssetBundlesLoaded, onAssetBundlesLoadedFail, loadingAssetBundle.Value.hash));
                if (_tempErrorOccuring)
                    yield break;
                Dependencies[loadingAssetBundle.Key] = _tempAssetBundle;
                LoadedAssetBundlesCount++;
            }
            // All asset bundles loaded, load init scene
            CurrentLoadState = LoadState.Done;
            yield return new WaitForSecondsRealtime(delayBeforeLoadInitScene);
            SceneManager.LoadScene(initSceneName);
        }

        public Uri UriAppend(Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }
    }
}
