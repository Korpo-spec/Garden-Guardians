using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/AttackComboSO")]
public class AttackComboSO : ScriptableObject
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private AnimatorOverrideController newAnims;
    [SerializeField] public int comboLength;
    [SerializeField] public AttackInfo[] attackInfos;
    
    [SerializeField] public ComboEffects weaponEffect;

    public GameObject Weapon => weapon;
    public AnimatorOverrideController NewAnims => newAnims;
    public int ComboLength => comboLength;


    [Header("Reset Settings Be careful before clicking here :)")]
    
    [SerializeField]
    [Tooltip("Save the current attackinfo as default")]
    private bool SaveAttackInfo;
    
    [SerializeField]
    [Tooltip("Resets attackinfo to default")]
    private bool ResetAttackInfo;
    
    
    
    [SerializeField] 
    private AttackInfo[] DefaultlAttackInfos;
    [SerializeField]
    public ComboEffects DefaultweaponEffect;

    private void OnValidate()
    {
        if (ResetAttackInfo)
        {
            ResetAttackInfo = false;
            ResettAttackInfo();
        } if (SaveAttackInfo)
        {
            SaveAttackInfo = false;
            SaveDefaultAttackInfo();
        }
    }

    private void SaveDefaultAttackInfo()
    {
        DefaultlAttackInfos = attackInfos;
        DefaultweaponEffect = weaponEffect;
    } private void ResettAttackInfo()
    {
        attackInfos = DefaultlAttackInfos;
        weaponEffect = DefaultweaponEffect;
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
}
