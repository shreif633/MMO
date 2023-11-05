using LiteNetLibManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class MonsterCharacterEntity
    {
        [Header("Demo Developer Extension")]
        public bool writeAddonLog;
        [DevExtMethods("Awake")]
        protected void DevExtAwakeDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.Awake()");
            onStart += DevExtStartDemo;
            onEnable += DevExtOnEnableDemo;
            onDisable += DevExtOnDisableDemo;
            onUpdate += DevExtUpdateDemo;
            onSetup += DevExtOnSetupDemo;
            onSetupNetElements += DevExtSetupNetElementsDemo;
            onNetworkDestroy += DevExtOnNetworkDestroyDemo;
            onReceiveDamage += DevExtReceiveDamageDemo;
            onReceivedDamage += DevExtReceivedDamageDemo;
        }

        [DevExtMethods("OnDestroy")]
        protected void DevExtOnDestroyDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.OnDestroy()");
            onStart -= DevExtStartDemo;
            onEnable -= DevExtOnEnableDemo;
            onDisable -= DevExtOnDisableDemo;
            onUpdate -= DevExtUpdateDemo;
            onSetup -= DevExtOnSetupDemo;
            onSetupNetElements -= DevExtSetupNetElementsDemo;
            onNetworkDestroy -= DevExtOnNetworkDestroyDemo;
            onReceiveDamage -= DevExtReceiveDamageDemo;
            onReceivedDamage -= DevExtReceivedDamageDemo;
        }

        protected void DevExtStartDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.Start()");
        }

        protected void DevExtOnEnableDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.OnEnable()");
        }

        protected void DevExtOnDisableDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.OnDisable()");
        }

        protected void DevExtUpdateDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.Update()");
        }

        protected void DevExtOnSetupDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.OnSetup()");
        }

        protected void DevExtSetupNetElementsDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.SetupNetElements()");
        }

        protected void DevExtOnNetworkDestroyDemo(byte reasons)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.OnNetworkDestroy(" + reasons + ")");
        }

        protected void DevExtReceiveDamageDemo(HitBoxPosition position, Vector3 fromPosition, IGameEntity attacker, Dictionary<DamageElement, MinMaxFloat> allDamageAmounts, CharacterItem weapon, BaseSkill skill, int skillLevel)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.ReceiveDamage("
                + attacker.GetGameObject().name + ", " + weapon + ", " + allDamageAmounts.Count + ", " + (skill != null ? skill.Title : "No Debuff") + ")");
        }

        protected void DevExtReceivedDamageDemo(HitBoxPosition position, Vector3 fromPosition, IGameEntity attacker, CombatAmountType combatAmountType, int damage, CharacterItem weapon, BaseSkill skill, int skillLevel, CharacterBuff buff, bool isDamageOverTime)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] MonsterCharacterEntity.ReceivedDamage("
                + attacker.GetGameObject().name + ", " + combatAmountType + ", " + damage + ")");
        }
    }
}
