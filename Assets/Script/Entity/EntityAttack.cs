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
    [SerializeField] private Transform mainHand;
    
    [Header("DEBUG OPTIONS")]
    [SerializeField] public bool activateDebug = false;
    [Range(0, 2)][SerializeField] public int comboIndex = 0;

    private EntityMovement _movement;

    private void Start()
    {
        _movement = GetComponent<EntityMovement>();
    }

    public void Attack(int comboIndex)
    {
        Debug.Log("Attack");
        if (weapon.attackInfos[comboIndex].slashEffect != null)
        {
           
            
            for (int i = 0; i < mainHand.childCount; i++)
            {
                Transform child = mainHand.GetChild(i);
                if (!child.CompareTag("Weapon")) continue;
                Debug.Log("Slash effect");
                
                GameObject g = Instantiate(weapon.attackInfos[comboIndex].slashEffect, child.position, Quaternion.Euler(0,0,0), child);
                g.transform.localRotation = Quaternion.Euler(new Vector3(313.194489f,181.419785f,83.6417999f));
                g.transform.localScale = new Vector3(0.01453377f, 0.01453377f, 0.01453377f);
                child.DetachChildren();
            }
        }
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

    public void AttackDash()
    {
        Debug.Log("AttackDash");
        StartCoroutine(InternalDash());
    }
    
    private IEnumerator InternalDash()
    {
        float startTime = Time.time;
        Vector3 dashvector = _movement.prevDirVector3;
        _movement.canMove = false;
        while (Time.time<startTime+weapon.attackInfos[0].dashTime)
        {
            _movement.controller.Move(dashvector.normalized* (weapon.attackInfos[0].dashSpeed * Time.deltaTime));
            yield return null;
        }
        _movement.canMove = true;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position + weapon.attackInfos[comboIndex].colliderInfo.center, weapon.attackInfos[comboIndex].colliderInfo.halfsize);
    }
}
