using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Effects/StunEffect")]
public class StunEffect : Effect
{
    
    private EntityMovement _entityMovement;
    private EntityAttack _entityAttack;
    private Animator _animator;
    public override void Enter(EffectManager obj)
    {
        base.Enter(obj);
        
        if (obj.TryGetComponent(out _entityMovement))
        {
            _entityMovement.canMove = false;
        }if (obj.TryGetComponent(out _entityAttack))
        {
             _animator = _entityAttack.GetComponent<Animator>();
             _entityAttack.canAttack = false;
            _entityAttack.StopAllCoroutines();
            _animator.speed= 0;
        }

        
        

    }

    public override void UpdateEffect()
    {
        if (_entityMovement)
        {
            _entityMovement.canMove = false;
        }

        if (_entityAttack)
        {
            _entityAttack.canAttack = false;
            _animator.speed= 0;
        }
    }

    public override void OnReapply()
    {
        time = duration;
    }

    public override void Exit()
    {
        base.Exit();
        if (_entityMovement)
        {
            _entityMovement.canMove = true;
        }
        if (_entityAttack)
        {
            _entityAttack.canAttack = true;
            _animator.speed= 1;
        }
    }
}
