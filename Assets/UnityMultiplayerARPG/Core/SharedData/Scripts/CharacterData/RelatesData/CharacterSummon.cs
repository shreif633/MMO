using LiteNetLib.Utils;

namespace MultiplayerARPG
{
    public enum SummonType : byte
    {
        None,
        Skill,
        PetItem,
        Custom = 254,
    }

    [System.Serializable]
    public partial class CharacterSummon : INetSerializable
    {
        public static readonly CharacterSummon Empty = new CharacterSummon();
        public string id;
        public SummonType type;
        public int dataId;
        public float summonRemainsDuration;
        public uint objectId;
        public int level;
        public int exp;
        public int currentHp;
        public int currentMp;

        public CharacterSummon Clone(bool generateNewId = false)
        {
            return new CharacterSummon()
            {
                id = generateNewId ? GenericUtils.GetUniqueId() : id,
                type = type,
                dataId = dataId,
                summonRemainsDuration = summonRemainsDuration,
                objectId = objectId,
                level = level,
                exp = exp,
                currentHp = currentHp,
                currentMp = currentMp,
            };
        }

        public static CharacterSummon Create(SummonType type, int dataId)
        {
            return new CharacterSummon()
            {
                id = GenericUtils.GetUniqueId(),
                type = type,
                dataId = dataId,
            };
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(id);
            writer.Put((byte)type);
            if (type != SummonType.None)
            {
                writer.PutPackedInt(dataId);
                switch (type)
                {
                    case SummonType.Skill:
                        writer.Put(summonRemainsDuration);
                        break;
                }
                writer.PutPackedUInt(objectId);
                writer.PutPackedInt(level);
                writer.PutPackedInt(exp);
                writer.PutPackedInt(currentHp);
                writer.PutPackedInt(currentMp);
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            id = reader.GetString();
            type = (SummonType)reader.GetByte();
            if (type != SummonType.None)
            {
                dataId = reader.GetPackedInt();
                switch (type)
                {
                    case SummonType.Skill:
                        summonRemainsDuration = reader.GetFloat();
                        break;
                }
                objectId = reader.GetPackedUInt();
                level = reader.GetPackedInt();
                exp = reader.GetPackedInt();
                currentHp = reader.GetPackedInt();
                currentMp = reader.GetPackedInt();
            }
        }
    }
}
