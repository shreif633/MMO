﻿using LiteNetLib.Utils;

namespace MultiplayerARPG
{
    public enum StorageType : byte
    {
        None,
        Player,
        Guild,
        Building,
    }

    [System.Serializable]
    public partial class StorageCharacterItem : INetSerializable
    {
        public static readonly StorageCharacterItem Empty = new StorageCharacterItem();
        public StorageType storageType;
        // Owner Id, for `Default` it is character Id. `Building` it is building Id. `Guild` it is guild Id.
        public string storageOwnerId;
        public CharacterItem characterItem;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)storageType);
            writer.Put(storageOwnerId);
            writer.Put(characterItem);
        }

        public void Deserialize(NetDataReader reader)
        {
            storageType = (StorageType)reader.GetByte();
            storageOwnerId = reader.GetString();
            characterItem = reader.Get(() => new CharacterItem());
        }
    }

    public struct StorageId
    {
        public static readonly StorageId Empty = new StorageId(StorageType.None, string.Empty);
        public StorageType storageType;
        public string storageOwnerId;

        public StorageId(StorageType storageType, string storageOwnerId)
        {
            this.storageType = storageType;
            this.storageOwnerId = storageOwnerId;
        }

        public string GetId()
        {
            return (byte)storageType + "_" + storageOwnerId;
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        public override string ToString()
        {
            return GetId();
        }
    }

    public struct StorageItemId
    {
        public static readonly StorageItemId Empty = new StorageItemId(StorageType.None, string.Empty, -1);
        public StorageType storageType;
        public string storageOwnerId;
        public int indexOfData;

        public StorageItemId(StorageType storageType, string storageOwnerId, int indexOfData)
        {
            this.storageType = storageType;
            this.storageOwnerId = storageOwnerId;
            this.indexOfData = indexOfData;
        }

        public string GetId()
        {
            return (byte)storageType + "_" + storageOwnerId + "_" + indexOfData;
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        public override string ToString()
        {
            return GetId();
        }
    }
}
