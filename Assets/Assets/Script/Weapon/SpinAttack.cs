using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;
[CreateAssetMenu(menuName = "Custom/SpecialAttack/SpinAttack")]
public class SpinAttack : SpecialAttack
{
    private Animator _animator;
    private GameObject _activeParticle;
    [SerializeField] private float radius = 1.4f;
    [SerializeField] private float attackSpeed = 0.5f;

    private float time;
    public override void OnPressed(Animator animator)
    {
        _animator = animator;
        time = 0;
        animator.SetBool("SpecialAttack", true);
        _activeParticle = Instantiate(attackInfo.slashEffect, animator.rootPosition + new Vector3(0,1.5f,0),Quaternion.identity, animator.transform);
    }

    public override void OnHeld()
    {
        time += Time.deltaTime;
        if (time < attackSpeed)
        {
            return;
        }

        time = 0;
        Debug.Log("TRYING TO HIT");
        transformHealthDictionary.TryGetHealth(_animator.transform, out EntityHealth thisHealth);

        Collider[] colliders = Physics.OverlapSphere(_animator.transform.position, radius, mask);
        Debug.Log(colliders.Length);
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
        Destroy(_activeParticle);
    }
}
