using LiteNetLib.Utils;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public struct ResponseOpenGachaMessage : INetSerializable
    {
        public UITextKeys message;
        public int dataId;
        public List<RewardedItem> rewardItems;

        public void Deserialize(NetDataReader reader)
        {
            message = (UITextKeys)reader.GetPackedUShort();
            if (message == UITextKeys.NONE)
            {
                dataId = reader.GetPackedInt();
                rewardItems = reader.GetList<RewardedItem>();
            }
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutPackedUShort((ushort)message);
            if (message == UITextKeys.NONE)
            {
                writer.PutPackedInt(dataId);
                writer.PutList(rewardItems);
            }
        }
    }
}
