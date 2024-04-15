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
    [SerializeField] public Effect weaponEffect;

    public GameObject Weapon => weapon;
    public AnimatorOverrideController NewAnims => newAnims;
    public int ComboLength => comboLength;
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
    public ColliderInfo colliderInfo;
    public GameObject slashEffect;
    public bool useCustomRot;
    public Vector3 customRot;
    public GameObject hitEffect;
    public float dashLenght;
    public float dashTime;
    public AnimationCurve dashCurve;
}
