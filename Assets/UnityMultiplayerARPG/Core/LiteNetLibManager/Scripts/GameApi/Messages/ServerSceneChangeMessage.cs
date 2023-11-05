﻿using LiteNetLib.Utils;

namespace LiteNetLibManager
{
    public struct ServerSceneChangeMessage : INetSerializable
    {
        public string serverSceneName;

        public void Deserialize(NetDataReader reader)
        {
            serverSceneName = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(serverSceneName);
        }
    }
}
