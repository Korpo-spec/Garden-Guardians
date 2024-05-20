using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class SpecialAttack : ScriptableObject
{
    public AttackInfo attackInfo;
    [SerializeField] protected TransformHealthDictionary transformHealthDictionary;
    [SerializeField] protected LayerMask mask;
    [SerializeField] protected AnimationClip animationClip;

    public virtual void OnPressed(Animator animator)
    {
        
    }

    public virtual void OnHeld()
    {
        
    }

    public virtual void OnRelease()
    {
        
    }
    
    public virtual void OnSpecialAttack()
    {
        
    }
    
    public virtual void OnGizmos(GameObject obj)
    {
        
    }
    
    public virtual void OnSpecialSwitch(Animator animator)
    {
        var runtimeAnimatorController = animator.runtimeAnimatorController;
        AnimatorOverrideController overrideController;
        overrideController = (AnimatorOverrideController) runtimeAnimatorController;
        overrideController["Spin"] = animationClip;
        
        
        //AnimationClipOverrides clipOverrides = new AnimationClipOverrides(overrideController.overridesCount);
        //overrideController.GetOverrides(clipOverrides);
        //clipOverrides["Spin"] = animationClip;
        
        
        runtimeAnimatorController = overrideController;
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }
}
