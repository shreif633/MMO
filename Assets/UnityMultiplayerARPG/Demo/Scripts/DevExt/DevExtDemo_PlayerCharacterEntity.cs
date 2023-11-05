using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class PlayerCharacterEntity
    {
        [Header("Demo Developer Extension")]
        public bool writeAddonLog;
        [DevExtMethods("Awake")]
        protected void DevExtAwakeDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.Awake()");
            onStart += DevExtStartDemo;
            onEnable += DevExtOnEnableDemo;
            onDisable += DevExtOnDisableDemo;
            onUpdate += DevExtUpdateDemo;
            onSetup += DevExtOnSetupDemo;
            onSetupNetElements += DevExtSetupNetElementsDemo;
            onNetworkDestroy += DevExtOnNetworkDestroyDemo;
            onReceiveDamage += DevExtReceiveDamageDemo;
            onReceivedDamage += DevExtReceivedDamageDemo;
            onApplyBuff += DevExtApplyBuff;
        }

        [DevExtMethods("OnDestroy")]
        protected void DevExtOnDestroyDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.OnDestroy()");
            onStart -= DevExtStartDemo;
            onEnable -= DevExtOnEnableDemo;
            onDisable -= DevExtOnDisableDemo;
            onUpdate -= DevExtUpdateDemo;
            onSetup -= DevExtOnSetupDemo;
            onSetupNetElements -= DevExtSetupNetElementsDemo;
            onNetworkDestroy -= DevExtOnNetworkDestroyDemo;
            onReceiveDamage -= DevExtReceiveDamageDemo;
            onReceivedDamage -= DevExtReceivedDamageDemo;
            onApplyBuff -= DevExtApplyBuff;
        }

        protected void DevExtStartDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.Start()");
        }

        protected void DevExtOnEnableDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.OnEnable()");
        }

        protected void DevExtOnDisableDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.OnDisable()");
        }

        protected void DevExtUpdateDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.Update()");
        }

        protected void DevExtOnSetupDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.OnSetup()");
        }

        protected void DevExtSetupNetElementsDemo()
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.SetupNetElements()");
        }

        protected void DevExtOnNetworkDestroyDemo(byte reasons)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.OnNetworkDestroy(" + reasons + ")");
        }

        protected void DevExtReceiveDamageDemo(HitBoxPosition position, Vector3 fromPosition, IGameEntity attacker, Dictionary<DamageElement, MinMaxFloat> allDamageAmounts, CharacterItem weapon, BaseSkill skill, int skillLevel)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.ReceiveDamage("
                + attacker.GetGameObject().name + ", " + weapon + ", " + allDamageAmounts.Count + ", " + (skill != null ? skill.Title : "No Debuff") + ")");
        }

        protected void DevExtReceivedDamageDemo(HitBoxPosition position, Vector3 fromPosition, IGameEntity attacker, CombatAmountType combatAmountType, int damage, CharacterItem weapon, BaseSkill skill, int skillLevel, CharacterBuff buff, bool isDamageOverTime)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.ReceivedDamage("
                + attacker.GetGameObject().name + ", " + combatAmountType + ", " + damage + ")");
        }

        protected void DevExtApplyBuff(int dataId, BuffType type, int level, EntityInfo applierInfo)
        {
            if (writeAddonLog) Debug.Log("[" + name + "] PlayerCharacterEntity.ApplyBuff("
                + dataId + ", " + type + ", " + level + ")");
        }
    }
}
