using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Combo")]
public class ComboEffects : Effect
{
    public List<Effect> effectsApplied;
    public override void Enter(EffectManager obj)
    {
        
        foreach (var effect in effectsApplied)
        {
            obj.AddEffect(Instantiate(effect));
        }
    }

    public void TryAddEffect(Effect effect)
    {
        for (int i = 0; i < effectsApplied.Count; i++)
        {
            if (effectsApplied[i].effectType==effect.effectType)
            {
                effectsApplied[i] = effectsApplied[i].UpgradedVersion;
                return;
            }
            
        }
        effectsApplied.Add(effect);
    }
    
    public override void Exit()
    {
        
    }
}
