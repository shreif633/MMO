using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MultiplayerARPG
{
    [System.Serializable]
    public partial class CharacterData : ICharacterData
    {
        private int _dataId;
        private int _entityId;
        private int _level;
        private byte _equipWeaponSet;
        private ObservableCollection<EquipWeapons> _selectableEquipWeapons;
        private ObservableCollection<CharacterAttribute> _attributes;
        private ObservableCollection<CharacterSkill> _skills;
        private List<CharacterSkillUsage> _skillUsages;
        private ObservableCollection<CharacterBuff> _buffs;
        private ObservableCollection<CharacterItem> _equipItems;
        private ObservableCollection<CharacterItem> _nonEquipItems;
        private ObservableCollection<CharacterSummon> _summons;

        public string Id { get; set; }
        public int DataId
        {
            get { return _dataId; }
            set
            {
                _dataId = value;
#if !NET && !NETCOREAPP
                this.MarkToMakeCaches();
#endif
            }
        }
        public int EntityId
        {
            get { return _entityId; }
            set
            {
                _entityId = value;
#if !NET && !NETCOREAPP
                this.MarkToMakeCaches();
#endif
            }
        }
        public string CharacterName { get; set; }
        public string Title
        {
            get { return CharacterName; }
            set { CharacterName = value; }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
#if !NET && !NETCOREAPP
                this.MarkToMakeCaches();
#endif
            }
        }
        public int Exp { get; set; }
        public int CurrentHp { get; set; }
        public int CurrentMp { get; set; }
        public int CurrentStamina { get; set; }
        public int CurrentFood { get; set; }
        public int CurrentWater { get; set; }
        public int IconDataId { get; set; }
        public int FrameDataId { get; set; }
        public int TitleDataId { get; set; }

        public EquipWeapons EquipWeapons
        {
            get
            {
                if (EquipWeaponSet < SelectableWeaponSets.Count)
                    return SelectableWeaponSets[EquipWeaponSet];
                return new EquipWeapons();
            }
            set
            {
#if !NET && !NETCOREAPP
                this.FillWeaponSetsIfNeeded(EquipWeaponSet);
#endif
                SelectableWeaponSets[EquipWeaponSet] = value;
            }
        }

        public byte EquipWeaponSet
        {
            get { return _equipWeaponSet; }
            set
            {
                _equipWeaponSet = value;
#if !NET && !NETCOREAPP
                this.MarkToMakeCaches();
#endif
            }
        }

        public IList<EquipWeapons> SelectableWeaponSets
        {
            get
            {
                if (_selectableEquipWeapons == null)
                {
                    _selectableEquipWeapons = new ObservableCollection<EquipWeapons>();
                    _selectableEquipWeapons.CollectionChanged += List_CollectionChanged;
                }
                return _selectableEquipWeapons;
            }
            set
            {
                if (_selectableEquipWeapons == null)
                {
                    _selectableEquipWeapons = new ObservableCollection<EquipWeapons>();
                    _selectableEquipWeapons.CollectionChanged += List_CollectionChanged;
                }
                _selectableEquipWeapons.Clear();
                foreach (EquipWeapons entry in value)
                    _selectableEquipWeapons.Add(entry);
            }
        }

        public IList<CharacterAttribute> Attributes
        {
            get
            {
                if (_attributes == null)
                {
                    _attributes = new ObservableCollection<CharacterAttribute>();
                    _attributes.CollectionChanged += List_CollectionChanged;
                }
                return _attributes;
            }
            set
            {
                if (_attributes == null)
                {
                    _attributes = new ObservableCollection<CharacterAttribute>();
                    _attributes.CollectionChanged += List_CollectionChanged;
                }
                _attributes.Clear();
                foreach (CharacterAttribute entry in value)
                    _attributes.Add(entry);
            }
        }

        public IList<CharacterSkill> Skills
        {
            get
            {
                if (_skills == null)
                {
                    _skills = new ObservableCollection<CharacterSkill>();
                    _skills.CollectionChanged += List_CollectionChanged;
                }
                return _skills;
            }
            set
            {
                if (_skills == null)
                {
                    _skills = new ObservableCollection<CharacterSkill>();
                    _skills.CollectionChanged += List_CollectionChanged;
                }
                _skills.Clear();
                foreach (CharacterSkill entry in value)
                    _skills.Add(entry);
            }
        }

        public IList<CharacterSkillUsage> SkillUsages
        {
            get
            {
                if (_skillUsages == null)
                    _skillUsages = new List<CharacterSkillUsage>();
                return _skillUsages;
            }
            set
            {
                if (_skillUsages == null)
                    _skillUsages = new List<CharacterSkillUsage>();
                _skillUsages.Clear();
                foreach (CharacterSkillUsage entry in value)
                    _skillUsages.Add(entry);
            }
        }

        public IList<CharacterBuff> Buffs
        {
            get
            {
                if (_buffs == null)
                {
                    _buffs = new ObservableCollection<CharacterBuff>();
                    _buffs.CollectionChanged += List_CollectionChanged;
                }
                return _buffs;
            }
            set
            {
                if (_buffs == null)
                {
                    _buffs = new ObservableCollection<CharacterBuff>();
                    _buffs.CollectionChanged += List_CollectionChanged;
                }
                _buffs.Clear();
                foreach (CharacterBuff entry in value)
                    _buffs.Add(entry);
            }
        }

        public IList<CharacterItem> EquipItems
        {
            get
            {
                if (_equipItems == null)
                {
                    _equipItems = new ObservableCollection<CharacterItem>();
                    _equipItems.CollectionChanged += List_CollectionChanged;
                }
                return _equipItems;
            }
            set
            {
                if (_equipItems == null)
                {
                    _equipItems = new ObservableCollection<CharacterItem>();
                    _equipItems.CollectionChanged += List_CollectionChanged;
                }
                _equipItems.Clear();
                foreach (CharacterItem entry in value)
                    _equipItems.Add(entry);
            }
        }

        public IList<CharacterItem> NonEquipItems
        {
            get
            {
                if (_nonEquipItems == null)
                {
                    _nonEquipItems = new ObservableCollection<CharacterItem>();
                    _nonEquipItems.CollectionChanged += List_CollectionChanged;
                }
                return _nonEquipItems;
            }
            set
            {
                if (_nonEquipItems == null)
                {
                    _nonEquipItems = new ObservableCollection<CharacterItem>();
                    _nonEquipItems.CollectionChanged += List_CollectionChanged;
                }
                _nonEquipItems.Clear();
                foreach (CharacterItem entry in value)
                    _nonEquipItems.Add(entry);
            }
        }

        public IList<CharacterSummon> Summons
        {
            get
            {
                if (_summons == null)
                {
                    _summons = new ObservableCollection<CharacterSummon>();
                    _summons.CollectionChanged += List_CollectionChanged;
                }
                return _summons;
            }
            set
            {
                if (_summons == null)
                {
                    _summons = new ObservableCollection<CharacterSummon>();
                    _summons.CollectionChanged += List_CollectionChanged;
                }
                _summons.Clear();
                foreach (CharacterSummon entry in value)
                    _summons.Add(entry);
            }
        }

        private void List_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
#if !NET && !NETCOREAPP
                this.MarkToMakeCaches();
#endif
        }
    }
}
