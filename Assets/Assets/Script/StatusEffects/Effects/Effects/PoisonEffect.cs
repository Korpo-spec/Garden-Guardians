using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/PoisonEffect")]
public class PoisonEffect : Effect
{
    public float damageOverTime;
    public float tickTime;
    private EntityHealth baloonRef;
    private float lastTime;
    private float damagePerTick;
    private float damageSum;
    public override void Enter(EffectManager obj)
    {
        base.Enter(obj);
        baloonRef = obj.GetComponent<EntityHealth>();
        damagePerTick = damageOverTime / (duration / tickTime);
        lastTime = duration-tickTime-Time.deltaTime;
    }

    public override void OnReapply()
    {
        time = duration;
        lastTime = duration-tickTime-Time.deltaTime;
    }

    public override void UpdateEffect()
    {
        if (lastTime-time > tickTime)
        {
            damageSum += damagePerTick;
            if (damageSum > 1)
            {
                int intDamage = (int) damageSum;
                damageSum -= intDamage;
                baloonRef.DamageUnit(intDamage,false,null);
            }

            lastTime = time;
        }
    }
}
