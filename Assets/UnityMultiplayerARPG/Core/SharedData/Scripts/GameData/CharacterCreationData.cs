using System.Collections.Generic;

namespace MultiplayerARPG
{
    [System.Serializable]
    public partial class CharacterCreationData
    {
        public Dictionary<int, Dictionary<int, PlayerCharacterData>> AvailableCharacters { get; private set; } = new Dictionary<int, Dictionary<int, PlayerCharacterData>>();
        public List<int> AvailableFactionIds { get; private set; } = new List<int>();

        public bool CanCreateCharacter(int dataId, int entityId, int factionId)
        {
            return AvailableCharacters.ContainsKey(entityId) && AvailableCharacters[entityId].ContainsKey(dataId) && AvailableFactionIds.Contains(factionId);
        }

        public PlayerCharacterData GetCreateCharacterData(string id, string userId, string characterName, int dataId, int entityId, int factionId)
        {
            PlayerCharacterData result = AvailableCharacters[entityId][dataId].CloneTo(new PlayerCharacterData());
            result.Id = id;
            result.UserId = userId;
            result.CharacterName = characterName;
            result.FactionId = factionId;
            return result;
        }

        public void SetCreateCharacterData(PlayerCharacterData data, string id, string userId, string characterName, int dataId, int entityId, int factionId)
        {
            data = AvailableCharacters[entityId][dataId].CloneTo(data);
            data.Id = id;
            data.UserId = userId;
            data.CharacterName = characterName;
            data.FactionId = factionId;
        }
    }
}
