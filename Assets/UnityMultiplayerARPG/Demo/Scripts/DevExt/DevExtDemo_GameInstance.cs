using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class GameInstance
    {
        [DevExtMethods("Awake")]
        public void DevExtAwake()
        {
            Debug.Log("[DevExt] GameInstance - Awake");
        }

        [DevExtMethods("LoadedGameData")]
        public void DevExtLoadedGameData()
        {
            Debug.Log("[DevExt] GameInstance - LoadedGameData");
        }
    }
}
