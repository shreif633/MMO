﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MobileInputButtonByKeyCode : MonoBehaviour, IMobileInputArea, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private KeyCode keyCode = KeyCode.None;
    [SerializeField]
    [Range(0f, 1f)]
    private float alphaWhileIdling = 1f;
    [SerializeField]
    [Range(0f, 1f)]
    private float alphaWhilePressing = 0.75f;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onPointerDown = new UnityEvent();
    [SerializeField]
    private UnityEvent onPointerUp = new UnityEvent();

    private CanvasGroup canvasGroup;
    private MobileInputConfig config;
    private float alphaMultiplier = 1f;
    private bool buttonAlreadyDown;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = alphaWhileIdling * alphaMultiplier;
        }
        config = GetComponent<MobileInputConfig>();
        if (config != null)
        {
            // Updating default canvas group alpha when loading new config
            config.onLoadAlpha += OnLoadAlpha;
        }
    }

    private void OnDestroy()
    {
        if (config != null)
            config.onLoadAlpha -= OnLoadAlpha;
    }

    private void OnDisable()
    {
        if (buttonAlreadyDown)
            InputManager.SetKeyUp(keyCode);
        SetIdleState();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown.Invoke();
        SetPressedState();
        InputManager.SetKeyDown(keyCode);
        buttonAlreadyDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp.Invoke();
        SetIdleState();
        InputManager.SetKeyUp(keyCode);
        buttonAlreadyDown = false;
    }

    private void SetIdleState()
    {
        if (canvasGroup)
            canvasGroup.alpha = alphaWhileIdling * alphaMultiplier;
    }

    private void SetPressedState()
    {
        if (canvasGroup)
            canvasGroup.alpha = alphaWhilePressing * alphaMultiplier;
    }

    public void OnLoadAlpha(float alpha)
    {
        alphaMultiplier = alpha;
    }

    public void SimulateClick()
    {
        StartCoroutine(SimulateClickRoutine());
    }

    IEnumerator SimulateClickRoutine()
    {
        OnPointerDown(default);
        yield return null;
        OnPointerUp(default);
    }
}
