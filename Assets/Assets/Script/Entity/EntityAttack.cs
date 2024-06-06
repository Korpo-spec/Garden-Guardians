using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Entity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EntityAttack : MonoBehaviour
{
    private Animator animator;
    private RuntimeAnimatorController _originalController;
    [SerializeField] private int animatorLayer = 1;
    [SerializeField] private AttackComboSO _weapon;
    [SerializeField] private AttackComboSO _defaultWeapon;
    [SerializeField] private TransformHealthDictionary targetDictionary;
    [SerializeField] private Transform mainHand;

    private GameObject _currentWeapon;
    [HideInInspector]
    public EntityHealth _health;

    public Animator Animator => animator;
    

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


            if (value.Weapon)
            {
                Transform weaponObj = mainHand ? mainHand.Find(value.Weapon.name) : null;
                if (weaponObj)
                {
                    weaponObj.gameObject.SetActive(true);
                    _currentWeapon = weaponObj.gameObject; 
                }
            }
            
            
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
        _health = GetComponent<EntityHealth>();
        animator = GetComponent<Animator>();
        _originalController = animator.runtimeAnimatorController;
        weapon = _weapon;
        WeaponUpgrade.SetPlayervalues += increaseStatsFromWeaponUpgrade;
    }

    private void OnDestroy()
    {
        WeaponUpgrade.SetPlayervalues -= increaseStatsFromWeaponUpgrade;
    }

    public void increaseStatsFromWeaponUpgrade(object sender, EventArgs eventArgs)
    {
        if (sender is WeaponUpgrade upgrade&&gameObject.CompareTag("Player"))
        {
            animator.SetFloat("AttackSpeed",animator.GetFloat("AttackSpeed")+upgrade.AnimationSpeedIncrease);
            _health.Defense += upgrade.DefenseIncrease;

            if (!upgrade.IsThorn) return;
            _health.Thorn = _health.Thorn==null ? upgrade.WeaponEffect : _health.Thorn.UpgradedVersion;


        }
       
    }

    private void Start()
    {
        _movement = GetComponent<EntityMovement>();
        canAttack = true;
    }

    public bool canAttack=true;

    public void Attack(int comboIndex)
    {
        if (!canAttack)return;
        
            //Handle slash effect
        //Debug.Log("Attack");
        if (_weapon.attackInfos[comboIndex].slashEffect != null)
        {
            for (int i = 0; i < mainHand.childCount; i++)
            {
                Transform child = mainHand.GetChild(i);
                if (!child.CompareTag("Weapon")) continue;
                if (!child.gameObject.activeSelf) continue;
                
                Debug.Log("Slash effect");
                
                GameObject g = Instantiate(_weapon.attackInfos[comboIndex].slashEffect, child.position, Quaternion.Euler(0,0,0), child);
                g.transform.localRotation = Quaternion.Euler(_weapon.attackInfos[comboIndex].useCustomRot ? _weapon.attackInfos[comboIndex].customRot :new Vector3(-313.194489f,181.419785f,83.6417999f));
                g.transform.parent = transform;
                g.transform.localScale = new Vector3(1,1,1);
            }
        }
        
        // Handle effects and damage
        //weapon.comboDamage[comboIndex];
        Matrix4x4 rotColliderMatrix = transform.localToWorldMatrix;
        Vector3 correctionVec = new Vector3(-1, 1, -1);
        //Debug.Log(Vector3.Scale(rotColliderMatrix.MultiplyPoint(_weapon.attackInfos[comboIndex].colliderInfo.center), correctionVec));
        Collider[] colliders = Physics.OverlapBox(
            rotColliderMatrix.MultiplyPoint(Vector3.Scale(_weapon.attackInfos[comboIndex].colliderInfo.center, correctionVec))
            , _weapon.attackInfos[comboIndex].colliderInfo.halfsize,
            rotColliderMatrix.rotation);
        
        //Debug.Log(rotColliderMatrix.rotation.eulerAngles);
        //Debug.Log(colliders.Length);
        foreach (var collider in colliders)
        {
            if (collider.transform == transform)
            {
                continue;
            }

            if (targetDictionary.TryGetHealth(collider.transform, out var health))
            {
                if (health.faction.faction == _health.faction.faction)
                {
                    continue;
                }

                Vector3 knockBackVec = collider.transform.position - transform.position;
                knockBackVec = knockBackVec.RemoveY();
                health.DamageUnit(_weapon.attackInfos[comboIndex].damage, _weapon.attackInfos[comboIndex].knockBack* knockBackVec,CalcIfCrit(comboIndex),this);
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

    public void SpecialAttack()
    {
        _weapon.specialAttack.OnSpecialAttack();
    }

    private bool CalcIfCrit(int index)
    {
        var random = Random.Range(0, 100);
        return (random <= _weapon.attackInfos[index].CritChance);
    }

    public void AttackDash()
    {
        //Debug.Log("AttackDash");
        StartCoroutine(InternalDash());
    }
    
    private IEnumerator InternalDash()
    {
        int comboIndex = 0;
        float startTime = 0;
        float timecoeficient = 1 / _weapon.attackInfos[comboIndex].dashTime;
        //Debug.Log(_movement);
        Vector3 dashvector = _movement.prevDirVector3;
        float dashSpeed =  _weapon.attackInfos[comboIndex].dashLenght /_weapon.attackInfos[comboIndex].dashTime;
        _movement.canMove = false;
        _movement.canDashCancel = false;
        while (startTime < 1)
        {
            startTime += Time.deltaTime * timecoeficient;
            _movement.controller.Move(dashvector.normalized* (dashSpeed * Time.deltaTime *_weapon.attackInfos[comboIndex].dashCurve.Evaluate(startTime)));
            yield return new WaitForEndOfFrame();
        }
        //_movement.canMove = true;

    }
    
    public void ResumeMovement()
    {
        Debug.Log("ResumeMovement");
        _movement.canMove = true;
        _movement.canDashCancel = false;
    }
    
    public void ActivateDashCancel()
    {
        _movement.canDashCancel = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (!activateDebug)
        {
            return;
        }
        Gizmos.color = Color.red;
        weapon.specialAttack?.OnGizmos(gameObject);
        
        Gizmos.color = Color.green;
        
        Gizmos.matrix = transform.localToWorldMatrix;
        Vector3 correctionVec = new Vector3(-1, 1, -1);
        Gizmos.DrawCube(Vector3.Scale(_weapon.attackInfos[comboIndex].colliderInfo.center,correctionVec), _weapon.attackInfos[comboIndex].colliderInfo.halfsize);
    }

    public void ResetWeapon()
    {
        GetComponent<InventoryHolder>().Inventory.UniversalMaterial.ClearUniMaterials();
        weapon.weaponEffect.effectsApplied.Clear();
        weapon.ResettAttackInfo();
        weapon = _defaultWeapon;
        weapon.weaponEffect?.effectsApplied.Clear();
    }

    public void StartBlendCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
