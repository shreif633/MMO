﻿using UnityEngine;

namespace MultiplayerARPG
{
    [System.Serializable]
    public partial struct NpcSellItem
    {
        /// <summary>
        /// Selling item
        /// </summary>
        [Tooltip("Selling item")]
        public BaseItem item;
        /// <summary>
        /// Require gold to buy item
        /// </summary>
        [Tooltip("Sell price in gold")]
        public int sellPrice;
        /// <summary>
        /// Require currencies to buy item
        /// </summary>
        [Tooltip("Sell prices in custom currencies")]
        public CurrencyAmount[] sellPrices;
    }
}