using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/AttackComboSO")]
public class AttackComboSO : ScriptableObject
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private AnimatorOverrideController newAnims;
    [SerializeField] private int comboLength;
    [SerializeField] private float[] comboDamage;

    public GameObject Weapon => weapon;
    public AnimatorOverrideController NewAnims => newAnims;
    public int ComboLength => comboLength;
}
