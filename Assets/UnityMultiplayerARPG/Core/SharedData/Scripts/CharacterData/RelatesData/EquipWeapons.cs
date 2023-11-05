using LiteNetLib.Utils;

namespace MultiplayerARPG
{
    [System.Serializable]
    public partial class EquipWeapons : INetSerializable
    {
        public CharacterItem rightHand;
        public CharacterItem leftHand;

        public EquipWeapons()
        {
            rightHand = new CharacterItem();
            leftHand = new CharacterItem();
        }

        private void Validate()
        {
            if (rightHand == null)
                rightHand = new CharacterItem();

            if (leftHand == null)
                leftHand = new CharacterItem();
        }

        public EquipWeapons Clone(bool generateNewId = false)
        {
            return new EquipWeapons()
            {
                rightHand = rightHand.Clone(generateNewId),
                leftHand = leftHand.Clone(generateNewId),
            };
        }

        public void Serialize(NetDataWriter writer)
        {
            Validate();
            // Right hand
            writer.Put(rightHand);
            // Left hand
            writer.Put(leftHand);
        }

        public void Deserialize(NetDataReader reader)
        {
            Validate();
            // Right hand
            rightHand = reader.Get(() => new CharacterItem());
            // Left hand
            leftHand = reader.Get(() => new CharacterItem());
        }
    }
}
