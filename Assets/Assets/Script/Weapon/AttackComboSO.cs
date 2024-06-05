using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/AttackComboSO")]
public class AttackComboSO : ScriptableObject
{
    [SerializeField] private GameObject weapon;
    [SerializeField] public GameObject player;
    [SerializeField] private AnimatorOverrideController newAnims;
    [SerializeField] public int comboLength;
    [SerializeField] public AttackInfo[] attackInfos;
    
    [SerializeField] public ComboEffects weaponEffect;

    [SerializeField] public SpecialAttack specialAttack;

    public GameObject Weapon => weapon;
    public GameObject particleSystem;
    
    public AnimatorOverrideController NewAnims => newAnims;
    public int ComboLength => comboLength;


    [Header("Reset Settings Be careful before clicking here :)")]
    
    [SerializeField]
    [Tooltip("Save the current attackinfo as default")]
    private bool SaveAttackInfo = false;
    
    [SerializeField]
    [Tooltip("Resets attackinfo to default")]
    private bool ResetAttackInfo = false;
    
    
    
    [SerializeField] 
    private AttackInfo[] DefaultlAttackInfos;
    [SerializeField]
    public ComboEffects DefaultweaponEffect;

    private void OnValidate()
    {
        if (SaveAttackInfo)
        {
            SaveDefaultAttackInfo();
            SaveAttackInfo = false;
        }

        if (ResetAttackInfo)
        {
            ResettAttackInfo();
            ResetAttackInfo = false;
        }
    }

    private void SaveDefaultAttackInfo()
    {
        DefaultlAttackInfos = (AttackInfo[])attackInfos.Clone();
        DefaultweaponEffect = weaponEffect;
    } 
    public void ResettAttackInfo()
    {
        attackInfos = (AttackInfo[])DefaultlAttackInfos.Clone();
        weaponEffect = Instantiate(DefaultweaponEffect);
    }
}

[System.Serializable]
public struct ColliderInfo
{
    public Vector3 center;
    public Vector3 halfsize;
    public bool isTrigger;
}
[System.Serializable]
public struct AttackInfo
{
    public float damage;
    public float knockBack;
    public ColliderInfo colliderInfo;
    public GameObject slashEffect;
    public bool useCustomRot;
    public Vector3 customRot;
    public GameObject hitEffect;
    public float dashLenght;
    public float dashTime;
    public AnimationCurve dashCurve;
    public int CritChance;
}
