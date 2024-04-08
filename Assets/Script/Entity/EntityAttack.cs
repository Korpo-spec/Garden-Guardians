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
    
    [Header("DEBUG OPTIONS")]
    [SerializeField] public bool activateDebug = false;
    [Range(0, 2)][SerializeField] public int comboIndex = 0;
    public void Attack(int comboIndex)
    {
        Debug.Log("Attack");
        //weapon.comboDamage[comboIndex];
        Collider[] colliders = Physics.OverlapBox(weapon.attackInfos[comboIndex].colliderInfo.center + transform.position, weapon.attackInfos[comboIndex].colliderInfo.halfsize, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.transform == transform)
            {
                continue;
            }

            if (targetDictionary.TryGetHealth(collider.transform, out var health))
            {
                health.DamageUnit(weapon.attackInfos[comboIndex].damage);
                if (weapon.attackInfos[comboIndex].hitEffect != null)
                {
                    Instantiate(weapon.attackInfos[comboIndex].hitEffect, collider.transform.position, Quaternion.identity);
                }
                
            }

            
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position + weapon.attackInfos[comboIndex].colliderInfo.center, weapon.attackInfos[comboIndex].colliderInfo.halfsize);
    }
}
