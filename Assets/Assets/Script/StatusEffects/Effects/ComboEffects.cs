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
    
    public override void Exit()
    {
        
    }
}
