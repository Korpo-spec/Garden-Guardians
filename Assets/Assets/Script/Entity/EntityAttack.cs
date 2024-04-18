using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class EntityAttack : MonoBehaviour
{
    private Animator animator;
    private RuntimeAnimatorController _originalController;
    [SerializeField] private int animatorLayer = 1;
    [SerializeField] private AttackComboSO _weapon;
    [SerializeField] private TransformHealthDictionary targetDictionary;
    [SerializeField] private Transform mainHand;

    private GameObject _currentWeapon;

    public AttackComboSO weapon
    {
        get => _weapon;
        set
        {
            if (_currentWeapon)
            {
                _currentWeapon.SetActive(false);
            }
            
            _weapon = value;
            
            
            GameObject weaponObj = mainHand.Find(value.Weapon.name).gameObject;
            weaponObj.SetActive(true);
            _currentWeapon = weaponObj;
            if (weapon.NewAnims)
            {
                animator.runtimeAnimatorController = _weapon.NewAnims;
            }
            else
            {
                animator.runtimeAnimatorController = _originalController;
            }
            
            OnWeaponChange?.Invoke(value);
        }
    }
    public event Action<AttackComboSO> OnWeaponChange;
    
    [Header("DEBUG OPTIONS")]
    [SerializeField] public bool activateDebug = false;
    [Range(0, 2)][SerializeField] public int comboIndex = 0;

    private EntityMovement _movement;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        _originalController = animator.runtimeAnimatorController;
        weapon = _weapon;
    }

    private void Start()
    {
        _movement = GetComponent<EntityMovement>();
    }

    public void Attack(int comboIndex)
    {
        //Handle slash effect
        Debug.Log("Attack");
        if (_weapon.attackInfos[comboIndex].slashEffect != null)
        {
           
            
            for (int i = 0; i < mainHand.childCount; i++)
            {
                Transform child = mainHand.GetChild(i);
                if (!child.CompareTag("Weapon")) continue;
                Debug.Log("Slash effect");
                
                GameObject g = Instantiate(_weapon.attackInfos[comboIndex].slashEffect, child.position, Quaternion.Euler(0,0,0), child);
                g.transform.localRotation = Quaternion.Euler(_weapon.attackInfos[comboIndex].useCustomRot ? _weapon.attackInfos[comboIndex].customRot :new Vector3(-313.194489f,181.419785f,83.6417999f));
                g.transform.localScale = new Vector3(0.01453377f, 0.01453377f, 0.01453377f);
                child.DetachChildren();
                g.transform.parent = transform;
            }
        }
        
        // Handle effects and damage
        //weapon.comboDamage[comboIndex];
        Collider[] colliders = Physics.OverlapBox(_weapon.attackInfos[comboIndex].colliderInfo.center + transform.position, _weapon.attackInfos[comboIndex].colliderInfo.halfsize, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.transform == transform)
            {
                continue;
            }

            if (targetDictionary.TryGetHealth(collider.transform, out var health))
            {
                health.DamageUnit(_weapon.attackInfos[comboIndex].damage);
                if (_weapon.attackInfos[comboIndex].hitEffect != null)
                {
                    Instantiate(_weapon.attackInfos[comboIndex].hitEffect, collider.transform.position, Quaternion.identity);
                }

                if (_weapon.weaponEffect)
                {
                    health.effectManager.AddEffect(_weapon.weaponEffect);
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
        int comboIndex = 0;
        float startTime = 0;
        float timecoeficient = 1 / _weapon.attackInfos[comboIndex].dashTime;
        Debug.Log(_movement);
        Vector3 dashvector = _movement.prevDirVector3;
        float dashSpeed =  _weapon.attackInfos[comboIndex].dashLenght /_weapon.attackInfos[comboIndex].dashTime;
        _movement.canMove = false;
        while (startTime < 1)
        {
            startTime += Time.deltaTime * timecoeficient;
            _movement.controller.Move(dashvector.normalized* (dashSpeed * Time.deltaTime *_weapon.attackInfos[comboIndex].dashCurve.Evaluate(startTime)));
            yield return new WaitForEndOfFrame();
        }
        _movement.canMove = true;

    }

    private void OnDrawGizmosSelected()
    {
        if (!activateDebug)
        {
            return;
        }
        Gizmos.DrawCube(transform.position + _weapon.attackInfos[comboIndex].colliderInfo.center, _weapon.attackInfos[comboIndex].colliderInfo.halfsize);
    }
}
