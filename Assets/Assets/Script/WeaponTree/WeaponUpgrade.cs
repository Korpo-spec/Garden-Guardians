using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponUpgrade
{
   [SerializeField] private float DamageIncrease;
   [SerializeField] private float KnockBackIncrease;
   [SerializeField] private Effect WeaponEffect;
   [SerializeField] public GameObject _particleSystem;
   

   public void AddUpgradeOnExistingweapon(AttackComboSO activeWeaponInfo)
   {

      for (int i = 0; i < activeWeaponInfo.attackInfos.Length; i++)
      {
         activeWeaponInfo.attackInfos[i].damage += DamageIncrease;
         activeWeaponInfo.attackInfos[i].knockBack += KnockBackIncrease;
      }

      if (WeaponEffect)
      {
         activeWeaponInfo.weaponEffect.TryAddEffect(WeaponEffect);
      }

      if (_particleSystem)
      {
         
         activeWeaponInfo.particleSystem = _particleSystem;
         activeWeaponInfo.particleSystem.transform.localPosition = activeWeaponInfo.Weapon.transform.localPosition;
      }
      
   }
}