using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponUpgrade
{
   [SerializeField] private float DamageIncrease;
   [SerializeField] private float DashLengthIncrease;
   [SerializeField] private Effect WeaponEffect;
   [SerializeField] private effectUpgrades WeaponEffectIncreases;

   public void AddUpgradeOnExistingweapon(AttackComboSO activeWeaponInfo)
   {

      for (int i = 0; i < activeWeaponInfo.attackInfos.Length; i++)
      {
         activeWeaponInfo.attackInfos[i].damage += DamageIncrease;
         //activeWeaponInfo.attackInfos[i].dashLenght += DashLengthIncrease;
      }

      if (WeaponEffect)
      {
         activeWeaponInfo.weaponEffect.effectsApplied.Add(WeaponEffect);
      }
      else
      {
         //Upgrade effectstats
      }
   }
}

[Serializable]
public struct effectUpgrades
{
   
}
