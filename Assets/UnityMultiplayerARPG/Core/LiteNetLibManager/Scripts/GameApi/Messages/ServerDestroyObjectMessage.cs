﻿using LiteNetLib.Utils;

namespace LiteNetLibManager
{
    public struct ServerDestroyObjectMessage : INetSerializable
    {
        public uint objectId;
        public byte reasons;

        public void Deserialize(NetDataReader reader)
        {
            objectId = reader.GetPackedUInt();
            reasons = reader.GetByte();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutPackedUInt(objectId);
            writer.Put(reasons);
        }
    }
}
