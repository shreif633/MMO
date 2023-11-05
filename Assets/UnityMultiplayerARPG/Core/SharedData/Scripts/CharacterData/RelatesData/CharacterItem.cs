using Cysharp.Text;
using LiteNetLib.Utils;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    [System.Flags]
    internal enum CharacterItemSyncState : byte
    {
        None = 0,
        IsEquipment = 1 << 1,
        IsWeapon = 1 << 2,
        IsPet = 1 << 3,
        IsEmpty = 1 << 4,
    }

    internal static class CharacterItemSyncStateExtensions
    {
        internal static bool Has(this CharacterItemSyncState self, CharacterItemSyncState flag)
        {
            return (self & flag) == flag;
        }
    }

    [System.Serializable]
    public partial class CharacterItem : INetSerializable
    {
        public static readonly CharacterItem Empty = new CharacterItem();
        public string id;
        public int dataId;
        public int level;
        public int amount;
        public byte equipSlotIndex;
        public float durability;
        public int exp;
        public float lockRemainsDuration;
        public long expireTime;
        public int randomSeed;
        public int ammo;
        public List<int> sockets = new List<int>();

        public List<int> Sockets
        {
            get
            {
                if (sockets == null)
                    sockets = new List<int>();
                return sockets;
            }
        }

        public List<int> ReadSockets(string sockets, char separator = ';')
        {
            Sockets.Clear();
            string[] splitTexts = sockets.Split(separator);
            foreach (string text in splitTexts)
            {
                if (string.IsNullOrEmpty(text))
                    continue;
                Sockets.Add(int.Parse(text));
            }
            return Sockets;
        }

        public string WriteSockets(char separator = ';')
        {
            using (Utf16ValueStringBuilder stringBuilder = ZString.CreateStringBuilder(true))
            {
                foreach (int socket in Sockets)
                {
                    stringBuilder.Append(socket);
                    stringBuilder.Append(separator);
                }
                return stringBuilder.ToString();
            }
        }

        public CharacterItem Clone(bool generateNewId = false)
        {
            return new CharacterItem()
            {
                id = generateNewId ? GenericUtils.GetUniqueId() : id,
                dataId = dataId,
                level = level,
                amount = amount,
                equipSlotIndex = equipSlotIndex,
                durability = durability,
                exp = exp,
                lockRemainsDuration = lockRemainsDuration,
                expireTime = expireTime,
                randomSeed = randomSeed,
                ammo = ammo,
                sockets = new List<int>(sockets),
            };
        }

        public void Serialize(NetDataWriter writer)
        {
            MakeCache();
            if (amount <= 0 || _cacheItem == null)
            {
                writer.Put((byte)CharacterItemSyncState.IsEmpty);
                writer.Put(id);
                return;
            }
            bool isEquipment = _cacheEquipmentItem != null;
            bool isWeapon = isEquipment && _cacheWeaponItem != null;
            bool isPet = _cachePetItem != null;
            CharacterItemSyncState syncState = CharacterItemSyncState.None;
            if (isEquipment)
            {
                syncState |= CharacterItemSyncState.IsEquipment;
            }
            if (isWeapon)
            {
                syncState |= CharacterItemSyncState.IsWeapon;
            }
            if (isPet)
            {
                syncState |= CharacterItemSyncState.IsPet;
            }
            writer.Put((byte)syncState);

            writer.Put(id);
            writer.PutPackedLong(expireTime);
            writer.PutPackedInt(dataId);
            writer.PutPackedInt(level);
            writer.PutPackedInt(amount);
            writer.Put(equipSlotIndex);
            writer.Put(lockRemainsDuration);

            if (isEquipment)
            {
                writer.Put(durability);
                writer.PutPackedInt(exp);

                byte socketCount = (byte)Sockets.Count;
                writer.Put(socketCount);
                if (socketCount > 0)
                {
                    foreach (int socketDataId in Sockets)
                    {
                        writer.PutPackedInt(socketDataId);
                    }
                }

                writer.PutPackedInt(randomSeed);
            }

            if (isWeapon)
            {
                writer.PutPackedInt(ammo);
            }

            if (isPet)
            {
                writer.PutPackedInt(exp);
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            CharacterItemSyncState syncState = (CharacterItemSyncState)reader.GetByte();
            if (syncState == CharacterItemSyncState.IsEmpty)
            {
                id = reader.GetString();
                dataId = 0;
                level = 0;
                amount = 0;
                equipSlotIndex = 0;
                durability = 0;
                exp = 0;
                lockRemainsDuration = 0;
                expireTime = 0;
                randomSeed = 0;
                ammo = 0;
                Sockets.Clear();
                return;
            }

            id = reader.GetString();
            expireTime = reader.GetPackedLong();
            dataId = reader.GetPackedInt();
            level = reader.GetPackedInt();
            amount = reader.GetPackedInt();
            equipSlotIndex = reader.GetByte();
            lockRemainsDuration = reader.GetFloat();

            if (syncState.Has(CharacterItemSyncState.IsEquipment))
            {
                durability = reader.GetFloat();
                exp = reader.GetPackedInt();

                byte socketCount = reader.GetByte();
                Sockets.Clear();
                for (byte i = 0; i < socketCount; ++i)
                {
                    Sockets.Add(reader.GetPackedInt());
                }

                randomSeed = reader.GetPackedInt();
            }

            if (syncState.Has(CharacterItemSyncState.IsWeapon))
            {
                ammo = reader.GetPackedInt();
            }

            if (syncState.Has(CharacterItemSyncState.IsPet))
            {
                exp = reader.GetPackedInt();
            }
        }
    }
}
