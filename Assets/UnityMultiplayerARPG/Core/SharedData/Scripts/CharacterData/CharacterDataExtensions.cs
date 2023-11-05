using System.Collections.Generic;

namespace MultiplayerARPG
{
    public static partial class CharacterDataExtensions
    {
        public static System.Type ClassType { get; private set; }

        static CharacterDataExtensions()
        {
            ClassType = typeof(CharacterDataExtensions);
        }

        public static List<EquipWeapons> Clone(this IList<EquipWeapons> src)
        {
            List<EquipWeapons> result = new List<EquipWeapons>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(new EquipWeapons()
                {
                    rightHand = src[i].rightHand.Clone(),
                    leftHand = src[i].leftHand.Clone(),
                });
            }
            return result;
        }

        public static List<CharacterAttribute> Clone(this IList<CharacterAttribute> src)
        {
            List<CharacterAttribute> result = new List<CharacterAttribute>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterBuff> Clone(this IList<CharacterBuff> src)
        {
            List<CharacterBuff> result = new List<CharacterBuff>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterItem> Clone(this IList<CharacterItem> src)
        {
            List<CharacterItem> result = new List<CharacterItem>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterSkill> Clone(this IList<CharacterSkill> src)
        {
            List<CharacterSkill> result = new List<CharacterSkill>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterSkillUsage> Clone(this IList<CharacterSkillUsage> src)
        {
            List<CharacterSkillUsage> result = new List<CharacterSkillUsage>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }

        public static List<CharacterSummon> Clone(this IList<CharacterSummon> src)
        {
            List<CharacterSummon> result = new List<CharacterSummon>();
            for (int i = 0; i < src.Count; ++i)
            {
                result.Add(src[i].Clone());
            }
            return result;
        }
    }
}
