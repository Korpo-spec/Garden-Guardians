using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/SpecialAttack/SlamAttack")]
public class SlamAttack : SpecialAttack
{
    private Animator _animator;
    private float time;
    [SerializeField] private float radius = 1.4f;
    public override void OnPressed(Animator animator)
    {
        _animator = animator;
        animator.SetBool("SpecialAttack", true);
    }

    public override void OnHeld()
    {
        
        
    }

    public override void OnSpecialAttack()
    {
        Debug.Log("TRYING TO HIT");
        transformHealthDictionary.TryGetHealth(_animator.transform, out EntityHealth thisHealth);

        Collider[] colliders = Physics.OverlapSphere(_animator.transform.position, radius, mask);
        Debug.Log(colliders.Length);
        Instantiate(attackInfo.slashEffect, _animator.transform.position, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.transform == _animator.transform)
            {
                continue;
            }

            if (transformHealthDictionary.TryGetHealth(collider.transform, out var health))
            {
                if (health.faction.faction == thisHealth.faction.faction)
                {
                    continue;
                }

                Vector3 knockBackVec = collider.transform.position - _animator.transform.position;
                knockBackVec = knockBackVec.RemoveY();
                health.DamageUnit(attackInfo.damage, attackInfo.knockBack* knockBackVec,false,null);
                if (attackInfo.hitEffect != null)
                {
                    Instantiate(attackInfo.hitEffect, collider.transform.position, Quaternion.identity);
                }

                /*if (_weapon.weaponEffect)
                {
                    health.effectManager.AddEffect(_weapon.weaponEffect);
                }
                */
            }
        }
    }

    public override void OnRelease()
    {
        _animator.SetBool("SpecialAttack", false);
    }

    public override void OnGizmos(GameObject gameObject)
    {
        var animator = gameObject.GetComponent<Animator>();
        Gizmos.DrawWireSphere(animator.transform.position, radius);
         
    }
}

