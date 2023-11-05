﻿using UnityEngine;

namespace MultiplayerARPG
{
    [CreateAssetMenu(fileName = GameDataMenuConsts.FACTION_FILE, menuName = GameDataMenuConsts.FACTION_MENU, order = GameDataMenuConsts.FACTION_ORDER)]
    public partial class Faction : BaseGameData
    {
        [SerializeField]
        protected bool isLocked;
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
    }
}
