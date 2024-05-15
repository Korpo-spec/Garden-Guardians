using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Custom/SpecialAttack/SpinAttack")]
public class SpinAttack : SpecialAttack
{
    private Animator _animator;
    private GameObject _activeParticle;
    public override void OnPressed(Animator animator)
    {
        _animator = animator;
        animator.SetBool("SpecialAttack", true);
        _activeParticle = Instantiate(AttackInfo.slashEffect, animator.transform);
    }

    public override void OnRelease()
    {
        _animator.SetBool("SpecialAttack", false);
        Destroy(_activeParticle);
    }
}
