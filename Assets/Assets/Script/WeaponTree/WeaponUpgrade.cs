using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Entity;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct WeaponUpgrade
{
   [SerializeField] private float DamageIncrease;
   [SerializeField] private float KnockBackIncrease;
   [SerializeField] private int CritChanceIncrease;
   [SerializeField] public int DefenseIncrease;
   [SerializeField] public float AnimationSpeedIncrease;
   [SerializeField] public Effect WeaponEffect;
   [SerializeField] public GameObject _particleSystem;
   [SerializeField] public bool IsThorn;
   public static event EventHandler SetPlayervalues;


   public void AddUpgradeOnExistingweapon(AttackComboSO activeWeaponInfo)
   {
      
      for (int i = 0; i < activeWeaponInfo.attackInfos.Length; i++)
      {
         activeWeaponInfo.attackInfos[i].damage += DamageIncrease;
         activeWeaponInfo.attackInfos[i].knockBack += KnockBackIncrease;
         activeWeaponInfo.attackInfos[i].CritChance += CritChanceIncrease;
         
      }
      SetPlayervalues?.Invoke(this,null);
      if (WeaponEffect&&!IsThorn)
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