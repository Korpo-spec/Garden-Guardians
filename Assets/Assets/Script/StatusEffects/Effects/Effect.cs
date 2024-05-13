using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Effect : ScriptableObject
{
    public new string name;
    public EffectType effectType;
    protected float time;
    public float duration;
    public GameObject ParticleSystem;
    public Effect UpgradedVersion;

    
    
    

    public bool Timer()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            return true;
        }
        return false;
    }

    public void SetDuration()
    {
        time = duration;
    }

    public virtual void Enter(EffectManager obj)
    {
        if (ParticleSystem)
        {
            ParticleSystem = Instantiate(ParticleSystem, obj.transform, false);
        }
 
    }

    public virtual void OnReapply()
    {
        
    }

    public virtual void UpdateEffect()
    {
        
    }

    public virtual void Exit()
    {
        Destroy(ParticleSystem);
    }


    

}

public enum EffectType
{
    KockBack,
    Power,
    Bleed,
    Speed,
    Crit,
    Thorns,
    Defense,
    Stun,
    Burn,
    Fire,
    Explosion,
    LChain,
    Lightning,
    Paralysis,
    Slow,
    Ice,
    ColdZone,
    
    
    
}
