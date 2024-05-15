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

    public virtual void OnPressed(Animator animator)
    {
        
    }

    public virtual void OnHeld()
    {
        
    }

    public virtual void OnRelease()
    {
        
    }
}
