using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int animatorLayer = 1;
    [SerializeField] public AttackComboSO weapon;
    [SerializeField] private TransformHealthDictionary targetDictionary;
    
    [Header("DEBUG OPTIONS")][Range(0, 2)]
    [SerializeField] public int comboIndex = 0;
    public void Attack(int comboIndex)
    {
        Debug.Log("Attack");
        //weapon.comboDamage[comboIndex];
        Collider[] colliders = Physics.OverlapBox(weapon.colliderInfo[comboIndex].center + transform.position, weapon.colliderInfo[comboIndex].halfsize, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.transform == transform)
            {
                continue;
            }

            if (targetDictionary.TryGetHealth(collider.transform, out var health))
            {
                health.DamageUnit(weapon.comboDamage[comboIndex]);
            }

            
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position + weapon.colliderInfo[comboIndex].center, weapon.colliderInfo[comboIndex].halfsize);
    }
}
