using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Effects/SlowEffect")]
public class SlowEffect : Effect
{
    [SerializeField][Range(0,100)]private float slowAmount;
   
    private float originalSpeed;
    private EntityMovement _entityMovement;
    
    
    
    public override void Enter(EffectManager obj)
    {
        base.Enter(obj);
        _entityMovement = obj.GetComponent<EntityMovement>();
        originalSpeed = _entityMovement.movementStats.speed;
        _entityMovement.movementStats.speed = originalSpeed *(1-(slowAmount/100));
    }

    public override void UpdateEffect()
    {
        Debug.Log(slowAmount);
    }

    public override void OnReapply()
    {
        time = duration;
    }

    public override void Exit()
    {
        base.Exit();
        _entityMovement.movementStats.speed = originalSpeed;
    }
}
