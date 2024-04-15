using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Explosion")]
public class ExplosionEffect : Effect
{
    public float range;
    public int explosionDmg;
    public List<Effect> effectsApplied;
    public override void Enter(EffectManager obj)
    {
        //ParticleSystem.transform.localScale = new Vector3(range, range, range);
        ParticleSystem = Instantiate(ParticleSystem, obj.transform.position, Quaternion.identity);

        Collider[] cols = Physics.OverlapSphere(obj.transform.position, range);

        foreach (var col in cols)
        {
            if (col.TryGetComponent<EntityHealth>(out EntityHealth entityHealth))
            {
                entityHealth.DamageUnit(explosionDmg);
                foreach (var effect in effectsApplied)
                {
                    entityHealth.GetComponent<EffectManager>().AddEffect(Instantiate(effect));
                }
            }

            
            
        }

    }

    public override void Exit()
    {
        
    }
}
