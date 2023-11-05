using LiteNetLib.Utils;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    [System.Serializable]
    public partial class PlayerCharacterData : CharacterData, IPlayerCharacterData, INetSerializable
    {
        private List<CharacterHotkey> _hotkeys = new List<CharacterHotkey>();
        private List<CharacterQuest> _quests = new List<CharacterQuest>();
        private List<CharacterCurrency> _currencies = new List<CharacterCurrency>();

        public string UserId { get; set; }
        public int FactionId { get; set; }
        public float StatPoint { get; set; }
        public float SkillPoint { get; set; }
        public int Gold { get; set; }
        public int UserGold { get; set; }
        public int UserCash { get; set; }
        public int PartyId { get; set; }
        public int GuildId { get; set; }
        public byte GuildRole { get; set; }
        public int SharedGuildExp { get; set; }
        public string CurrentMapName { get; set; }
        public Vec3 CurrentPosition { get; set; }
        public Vec3 CurrentRotation { get; set; }
        public string RespawnMapName { get; set; }
        public Vec3 RespawnPosition { get; set; }
        public int MountDataId { get; set; }
        public long LastDeadTime { get; set; }
        public long UnmuteTime { get; set; }
        public long LastUpdate { get; set; }

        public IList<CharacterHotkey> Hotkeys
        {
            get { return _hotkeys; }
            set
            {
                _hotkeys = new List<CharacterHotkey>();
                _hotkeys.AddRange(value);
            }
        }

        public IList<CharacterQuest> Quests
        {
            get { return _quests; }
            set
            {
                _quests = new List<CharacterQuest>();
                _quests.AddRange(value);
            }
        }

        public IList<CharacterCurrency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = new List<CharacterCurrency>();
                _currencies.AddRange(value);
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            this.DeserializeCharacterData(reader);
        }

        public void Serialize(NetDataWriter writer)
        {
            this.SerializeCharacterData(writer);
        }
    }

    public class PlayerCharacterDataLastUpdateComparer : IComparer<PlayerCharacterData>
    {
        private int _sortMultiplier = 1;

        public PlayerCharacterDataLastUpdateComparer Asc()
        {
            _sortMultiplier = 1;
            return this;
        }

        public PlayerCharacterDataLastUpdateComparer Desc()
        {
            _sortMultiplier = -1;
            return this;
        }

        public int Compare(PlayerCharacterData x, PlayerCharacterData y)
        {
            if (x == null && y == null)
                return 0;

            if (x == null && y != null)
                return -1;

            if (x != null && y == null)
                return 1;

            return x.LastUpdate.CompareTo(y.LastUpdate) * _sortMultiplier;
        }
    }
}
