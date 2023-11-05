using LiteNetLib.Utils;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public static partial class PlayerCharacterDataExtensions
    {
        public static System.Type ClassType { get; private set; }

        static PlayerCharacterDataExtensions()
        {
            ClassType = typeof(PlayerCharacterDataExtensions);
        }

        public static T CloneTo<T>(this IPlayerCharacterData from, T to,
            bool withEquipWeapons = true,
            bool withAttributes = true,
            bool withSkills = true,
            bool withSkillUsages = true,
            bool withBuffs = true,
            bool withEquipItems = true,
            bool withNonEquipItems = true,
            bool withSummons = true,
            bool withHotkeys = true,
            bool withQuests = true,
            bool withCurrencies = true) where T : IPlayerCharacterData
        {
            to.Id = from.Id;
            to.DataId = from.DataId;
            to.EntityId = from.EntityId;
            to.UserId = from.UserId;
            to.FactionId = from.FactionId;
            to.CharacterName = from.CharacterName;
            to.Level = from.Level;
            to.Exp = from.Exp;
            to.CurrentHp = from.CurrentHp;
            to.CurrentMp = from.CurrentMp;
            to.CurrentStamina = from.CurrentStamina;
            to.CurrentFood = from.CurrentFood;
            to.CurrentWater = from.CurrentWater;
            to.StatPoint = from.StatPoint;
            to.SkillPoint = from.SkillPoint;
            to.Gold = from.Gold;
            to.UserGold = from.UserGold;
            to.UserCash = from.UserCash;
            to.PartyId = from.PartyId;
            to.GuildId = from.GuildId;
            to.GuildRole = from.GuildRole;
            to.SharedGuildExp = from.SharedGuildExp;
            to.EquipWeaponSet = from.EquipWeaponSet;
            to.CurrentMapName = from.CurrentMapName;
            to.CurrentPosition = from.CurrentPosition;
            to.CurrentRotation = from.CurrentRotation;
            to.RespawnMapName = from.RespawnMapName;
            to.RespawnPosition = from.RespawnPosition;
            to.MountDataId = from.MountDataId;
            to.IconDataId = from.IconDataId;
            to.FrameDataId = from.FrameDataId;
            to.TitleDataId = from.TitleDataId;
            to.LastDeadTime = from.LastDeadTime;
            to.UnmuteTime = from.UnmuteTime;
            to.LastUpdate = from.LastUpdate;
            if (withEquipWeapons)
                to.SelectableWeaponSets = from.SelectableWeaponSets.Clone();
            if (withAttributes)
                to.Attributes = from.Attributes.Clone();
            if (withSkills)
                to.Skills = from.Skills.Clone();
            if (withSkillUsages)
                to.SkillUsages = from.SkillUsages.Clone();
            if (withBuffs)
                to.Buffs = from.Buffs.Clone();
            if (withEquipItems)
                to.EquipItems = from.EquipItems.Clone();
            if (withNonEquipItems)
                to.NonEquipItems = from.NonEquipItems.Clone();
            if (withSummons)
                to.Summons = from.Summons.Clone();
            if (withHotkeys)
                to.Hotkeys = from.Hotkeys.Clone();
            if (withQuests)
                to.Quests = from.Quests.Clone();
            if (withCurrencies)
                to.Currencies = from.Currencies.Clone();
            DevExtUtils.InvokeStaticDevExtMethods(ClassType, "CloneTo", from, to);
            return to;
        }

        public static List<CharacterHotkey> Clone(this IList<CharacterHotkey> src)
        {
            List<CharacterHotkey> result = new List<CharacterHotkey>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterQuest> Clone(this IList<CharacterQuest> src)
        {
            List<CharacterQuest> result = new List<CharacterQuest>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterCurrency> Clone(this IList<CharacterCurrency> src)
        {
            List<CharacterCurrency> result = new List<CharacterCurrency>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static void SerializeCharacterData<T>(this T characterData, NetDataWriter writer,
            bool withTransforms = true,
            bool withEquipWeapons = true,
            bool withAttributes = true,
            bool withSkills = true,
            bool withSkillUsages = true,
            bool withBuffs = true,
            bool withEquipItems = true,
            bool withNonEquipItems = true,
            bool withSummons = true,
            bool withHotkeys = true,
            bool withQuests = true,
            bool withCurrencies = true) where T : IPlayerCharacterData
        {
            writer.Put(characterData.Id);
            writer.PutPackedInt(characterData.DataId);
            writer.PutPackedInt(characterData.EntityId);
            writer.Put(characterData.UserId);
            writer.PutPackedInt(characterData.FactionId);
            writer.Put(characterData.CharacterName);
            writer.PutPackedInt(characterData.Level);
            writer.PutPackedInt(characterData.Exp);
            writer.PutPackedInt(characterData.CurrentHp);
            writer.PutPackedInt(characterData.CurrentMp);
            writer.PutPackedInt(characterData.CurrentStamina);
            writer.PutPackedInt(characterData.CurrentFood);
            writer.PutPackedInt(characterData.CurrentWater);
            writer.Put(characterData.StatPoint);
            writer.Put(characterData.SkillPoint);
            writer.PutPackedInt(characterData.Gold);
            writer.PutPackedInt(characterData.UserGold);
            writer.PutPackedInt(characterData.UserCash);
            writer.PutPackedInt(characterData.PartyId);
            writer.PutPackedInt(characterData.GuildId);
            writer.Put(characterData.GuildRole);
            writer.PutPackedInt(characterData.SharedGuildExp);
            writer.Put(characterData.CurrentMapName);
            if (withTransforms)
            {
                writer.Put(characterData.CurrentPosition.x);
                writer.Put(characterData.CurrentPosition.y);
                writer.Put(characterData.CurrentPosition.z);
                writer.Put(characterData.CurrentRotation.x);
                writer.Put(characterData.CurrentRotation.y);
                writer.Put(characterData.CurrentRotation.z);
            }
            writer.Put(characterData.RespawnMapName);
            if (withTransforms)
            {
                writer.Put(characterData.RespawnPosition.x);
                writer.Put(characterData.RespawnPosition.y);
                writer.Put(characterData.RespawnPosition.z);
            }
            writer.PutPackedInt(characterData.MountDataId);
            writer.PutPackedInt(characterData.IconDataId);
            writer.PutPackedInt(characterData.FrameDataId);
            writer.PutPackedInt(characterData.TitleDataId);
            writer.PutPackedLong(characterData.LastDeadTime);
            writer.PutPackedLong(characterData.UnmuteTime);
            writer.PutPackedLong(characterData.LastUpdate);
            // Attributes
            if (withAttributes)
            {
                writer.PutPackedInt(characterData.Attributes.Count);
                foreach (CharacterAttribute entry in characterData.Attributes)
                {
                    writer.Put(entry);
                }
            }
            // Buffs
            if (withBuffs)
            {
                writer.PutPackedInt(characterData.Buffs.Count);
                foreach (CharacterBuff entry in characterData.Buffs)
                {
                    writer.Put(entry);
                }
            }
            // Skills
            if (withSkills)
            {
                writer.PutPackedInt(characterData.Skills.Count);
                foreach (CharacterSkill entry in characterData.Skills)
                {
                    writer.Put(entry);
                }
            }
            // Skill Usages
            if (withSkillUsages)
            {
                writer.PutPackedInt(characterData.SkillUsages.Count);
                foreach (CharacterSkillUsage entry in characterData.SkillUsages)
                {
                    writer.Put(entry);
                }
            }
            // Summons
            if (withSummons)
            {
                writer.PutPackedInt(characterData.Summons.Count);
                foreach (CharacterSummon entry in characterData.Summons)
                {
                    writer.Put(entry);
                }
            }
            // Equip Items
            if (withEquipItems)
            {
                writer.PutPackedInt(characterData.EquipItems.Count);
                foreach (CharacterItem entry in characterData.EquipItems)
                {
                    writer.Put(entry);
                }
            }
            // Non Equip Items
            if (withNonEquipItems)
            {
                writer.PutPackedInt(characterData.NonEquipItems.Count);
                foreach (CharacterItem entry in characterData.NonEquipItems)
                {
                    writer.Put(entry);
                }
            }
            // Hotkeys
            if (withHotkeys)
            {
                writer.PutPackedInt(characterData.Hotkeys.Count);
                foreach (CharacterHotkey entry in characterData.Hotkeys)
                {
                    writer.Put(entry);
                }
            }
            // Quests
            if (withQuests)
            {
                writer.PutPackedInt(characterData.Quests.Count);
                foreach (CharacterQuest entry in characterData.Quests)
                {
                    writer.Put(entry);
                }
            }
            // Currencies
            if (withCurrencies)
            {
                writer.PutPackedInt(characterData.Currencies.Count);
                foreach (CharacterCurrency entry in characterData.Currencies)
                {
                    writer.Put(entry);
                }
            }
            // Equip weapon set
            writer.Put(characterData.EquipWeaponSet);
            // Selectable weapon sets
            if (withEquipWeapons)
            {
                writer.PutPackedInt(characterData.SelectableWeaponSets.Count);
                foreach (EquipWeapons entry in characterData.SelectableWeaponSets)
                {
                    writer.Put(entry);
                }
            }
            DevExtUtils.InvokeStaticDevExtMethods(ClassType, "SerializeCharacterData", characterData, writer);
        }

        public static PlayerCharacterData DeserializeCharacterData(this NetDataReader reader)
        {
            return new PlayerCharacterData().DeserializeCharacterData(reader);
        }

        public static void DeserializeCharacterData(this NetDataReader reader, ref PlayerCharacterData characterData)
        {
            characterData = reader.DeserializeCharacterData();
        }

        public static T DeserializeCharacterData<T>(this T characterData, NetDataReader reader,
            bool withTransforms = true,
            bool withEquipWeapons = true,
            bool withAttributes = true,
            bool withSkills = true,
            bool withSkillUsages = true,
            bool withBuffs = true,
            bool withEquipItems = true,
            bool withNonEquipItems = true,
            bool withSummons = true,
            bool withHotkeys = true,
            bool withQuests = true,
            bool withCurrencies = true) where T : IPlayerCharacterData
        {
            characterData.Id = reader.GetString();
            characterData.DataId = reader.GetPackedInt();
            characterData.EntityId = reader.GetPackedInt();
            characterData.UserId = reader.GetString();
            characterData.FactionId = reader.GetPackedInt();
            characterData.CharacterName = reader.GetString();
            characterData.Level = reader.GetPackedInt();
            characterData.Exp = reader.GetPackedInt();
            characterData.CurrentHp = reader.GetPackedInt();
            characterData.CurrentMp = reader.GetPackedInt();
            characterData.CurrentStamina = reader.GetPackedInt();
            characterData.CurrentFood = reader.GetPackedInt();
            characterData.CurrentWater = reader.GetPackedInt();
            characterData.StatPoint = reader.GetFloat();
            characterData.SkillPoint = reader.GetFloat();
            characterData.Gold = reader.GetPackedInt();
            characterData.UserGold = reader.GetPackedInt();
            characterData.UserCash = reader.GetPackedInt();
            characterData.PartyId = reader.GetPackedInt();
            characterData.GuildId = reader.GetPackedInt();
            characterData.GuildRole = reader.GetByte();
            characterData.SharedGuildExp = reader.GetPackedInt();
            characterData.CurrentMapName = reader.GetString();
            if (withTransforms)
            {
                characterData.CurrentPosition = new Vec3(reader.GetFloat(), reader.GetFloat(), reader.GetFloat());
                characterData.CurrentRotation = new Vec3(reader.GetFloat(), reader.GetFloat(), reader.GetFloat());
            }
            characterData.RespawnMapName = reader.GetString();
            if (withTransforms)
            {
                characterData.RespawnPosition = new Vec3(reader.GetFloat(), reader.GetFloat(), reader.GetFloat());
            }
            characterData.MountDataId = reader.GetPackedInt();
            characterData.IconDataId = reader.GetPackedInt();
            characterData.FrameDataId = reader.GetPackedInt();
            characterData.TitleDataId = reader.GetPackedInt();
            characterData.LastDeadTime = reader.GetPackedLong();
            characterData.UnmuteTime = reader.GetPackedLong();
            characterData.LastUpdate = reader.GetPackedLong();
            int count;
            // Attributes
            if (withAttributes)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Attributes.Add(reader.Get(() => new CharacterAttribute()));
                }
            }
            // Buffs
            if (withBuffs)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Buffs.Add(reader.Get(() => new CharacterBuff()));
                }
            }
            // Skills
            if (withSkills)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Skills.Add(reader.Get(() => new CharacterSkill()));
                }
            }
            // Skill Usages
            if (withSkillUsages)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.SkillUsages.Add(reader.Get(() => new CharacterSkillUsage()));
                }
            }
            // Summons
            if (withSummons)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Summons.Add(reader.Get(() => new CharacterSummon()));
                }
            }
            // Equip Items
            if (withEquipItems)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.EquipItems.Add(reader.Get(() => new CharacterItem()));
                }
            }
            // Non Equip Items
            if (withNonEquipItems)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.NonEquipItems.Add(reader.Get(() => new CharacterItem()));
                }
            }
            // Hotkeys
            if (withHotkeys)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Hotkeys.Add(reader.Get(() => new CharacterHotkey()));
                }
            }
            // Quests
            if (withQuests)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Quests.Add(reader.Get(() => new CharacterQuest()));
                }
            }
            // Currencies
            if (withCurrencies)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.Currencies.Add(reader.Get(() => new CharacterCurrency()));
                }
            }
            // Equip weapon set
            characterData.EquipWeaponSet = reader.GetByte();
            // Selectable weapon sets
            if (withEquipWeapons)
            {
                count = reader.GetPackedInt();
                for (int i = 0; i < count; ++i)
                {
                    characterData.SelectableWeaponSets.Add(reader.Get(() => new EquipWeapons()));
                }
            }
            DevExtUtils.InvokeStaticDevExtMethods(ClassType, "DeserializeCharacterData", characterData, reader);
            return characterData;
        }
    }
}
