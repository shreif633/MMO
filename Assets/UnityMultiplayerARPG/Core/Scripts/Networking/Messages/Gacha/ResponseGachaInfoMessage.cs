﻿using LiteNetLib.Utils;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public struct ResponseGachaInfoMessage : INetSerializable
    {
        public UITextKeys message;
        public int cash;
        public List<int> gachaIds;

        public void Deserialize(NetDataReader reader)
        {
            message = (UITextKeys)reader.GetPackedUShort();
            if (message == UITextKeys.NONE)
            {
                cash = reader.GetPackedInt();
                gachaIds = reader.GetList<int>();
            }
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutPackedUShort((ushort)message);
            if (message == UITextKeys.NONE)
            {
                writer.PutPackedInt(cash);
                writer.PutList(gachaIds);
            }
        }
    }
}
