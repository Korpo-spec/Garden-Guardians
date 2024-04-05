using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int animatorLayer = 1;
    
    public void Attack(int comboIndex)
    {
        Debug.Log("Attack");
        animator = GetComponent<Animator>();
        
    }
}
