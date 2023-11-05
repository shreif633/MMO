﻿using LiteNetLib.Utils;

namespace MultiplayerARPG
{
    public struct ResponseGetGuildInfoMessage : INetSerializable
    {
        public UITextKeys message;
        public GuildListEntry guild;

        public void Deserialize(NetDataReader reader)
        {
            message = (UITextKeys)reader.GetPackedUShort();
            if (message == UITextKeys.NONE)
                guild = reader.Get<GuildListEntry>();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutPackedUShort((ushort)message);
            if (message == UITextKeys.NONE)
                writer.Put(guild);
        }
    }
}
