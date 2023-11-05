﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class DefaultServerBuildingHandlers : MonoBehaviour, IServerBuildingHandlers
    {
        public static readonly ConcurrentDictionary<string, IBuildingSaveData> BuildingEntities = new ConcurrentDictionary<string, IBuildingSaveData>();

        public int BuildingsCount
        {
            get { return BuildingEntities.Count; }
        }

        public IEnumerable<IBuildingSaveData> GetBuildings()
        {
            return BuildingEntities.Values;
        }

        public bool TryGetBuilding(string id, out IBuildingSaveData building)
        {
            return BuildingEntities.TryGetValue(id, out building);
        }

        public bool AddBuilding(string id, IBuildingSaveData building)
        {
            return BuildingEntities.TryAdd(id, building);
        }

        public bool RemoveBuilding(string id)
        {
            return BuildingEntities.TryRemove(id, out _);
        }

        public void ClearBuildings()
        {
            BuildingEntities.Clear();
        }
    }
}
