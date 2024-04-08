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
    public GameObject hitEffect;
}
