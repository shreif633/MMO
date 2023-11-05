using Cysharp.Text;
using LiteNetLib.Utils;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    [System.Serializable]
    public partial class CharacterQuest : INetSerializable
    {
        public static readonly CharacterQuest Empty = new CharacterQuest();
        public int dataId;
        public bool isComplete;
        public bool isTracking;
        public Dictionary<int, int> killedMonsters = new Dictionary<int, int>();
        public List<int> completedTasks = new List<int>();

        public Dictionary<int, int> KilledMonsters
        {
            get
            {
                if (killedMonsters == null)
                    killedMonsters = new Dictionary<int, int>();
                return killedMonsters;
            }
        }

        public List<int> CompletedTasks
        {
            get
            {
                if (completedTasks == null)
                    completedTasks = new List<int>();
                return completedTasks;
            }
        }

        public Dictionary<int, int> ReadKilledMonsters(string killMonsters)
        {
            KilledMonsters.Clear();
            string[] splitSets = killMonsters.Split(';');
            foreach (string set in splitSets)
            {
                if (string.IsNullOrEmpty(set))
                    continue;
                string[] splitData = set.Split(':');
                if (splitData.Length != 2)
                    continue;
                KilledMonsters[int.Parse(splitData[0])] = int.Parse(splitData[1]);
            }
            return KilledMonsters;
        }

        public string WriteKilledMonsters()
        {
            using (Utf16ValueStringBuilder stringBuilder = ZString.CreateStringBuilder(true))
            {
                foreach (KeyValuePair<int, int> keyValue in KilledMonsters)
                {
                    stringBuilder.Append(keyValue.Key);
                    stringBuilder.Append(':');
                    stringBuilder.Append(keyValue.Value);
                    stringBuilder.Append(';');
                }
                return stringBuilder.ToString();
            }
        }

        public List<int> ReadCompletedTasks(string completedTasks)
        {
            CompletedTasks.Clear();
            string[] splitTexts = completedTasks.Split(';');
            foreach (string text in splitTexts)
            {
                if (string.IsNullOrEmpty(text))
                    continue;
                CompletedTasks.Add(int.Parse(text));
            }
            return CompletedTasks;
        }

        public string WriteCompletedTasks()
        {
            using (Utf16ValueStringBuilder stringBuilder = ZString.CreateStringBuilder(true))
            {
                foreach (int completedTask in CompletedTasks)
                {
                    stringBuilder.Append(completedTask);
                    stringBuilder.Append(';');
                }
                return stringBuilder.ToString();
            }
        }

        public CharacterQuest Clone()
        {
            CharacterQuest clone = new CharacterQuest();
            clone.dataId = dataId;
            clone.isComplete = isComplete;
            clone.isTracking = isTracking;
            // Clone killed monsters
            Dictionary<int, int> killedMonsters = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> cloneEntry in this.killedMonsters)
            {
                killedMonsters[cloneEntry.Key] = cloneEntry.Value;
            }
            clone.killedMonsters = killedMonsters;
            // Clone complete tasks
            clone.completedTasks = new List<int>(completedTasks);
            return clone;
        }

        public static CharacterQuest Create(int dataId)
        {
            return new CharacterQuest()
            {
                dataId = dataId,
                isComplete = false,
            };
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutPackedInt(dataId);
            writer.Put(isComplete);
            writer.Put(isTracking);
            byte killMonstersCount = (byte)KilledMonsters.Count;
            writer.Put(killMonstersCount);
            if (killMonstersCount > 0)
            {
                foreach (KeyValuePair<int, int> killedMonster in KilledMonsters)
                {
                    writer.PutPackedInt(killedMonster.Key);
                    writer.PutPackedInt(killedMonster.Value);
                }
            }
            byte completedTasksCount = (byte)CompletedTasks.Count;
            writer.Put(completedTasksCount);
            if (completedTasksCount > 0)
            {
                foreach (int talkedNpc in CompletedTasks)
                {
                    writer.PutPackedInt(talkedNpc);
                }
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            dataId = reader.GetPackedInt();
            isComplete = reader.GetBool();
            isTracking = reader.GetBool();
            int killMonstersCount = reader.GetByte();
            KilledMonsters.Clear();
            for (int i = 0; i < killMonstersCount; ++i)
            {
                KilledMonsters.Add(reader.GetPackedInt(), reader.GetPackedInt());
            }
            int completedTasksCount = reader.GetByte();
            CompletedTasks.Clear();
            for (int i = 0; i < completedTasksCount; ++i)
            {
                CompletedTasks.Add(reader.GetPackedInt());
            }
        }
    }
}
