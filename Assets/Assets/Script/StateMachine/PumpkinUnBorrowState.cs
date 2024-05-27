using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/PumpkinTransitionState")]
public class PumpkinUnBorrowState : State
{
    private StateController _controller;
    [SerializeField] private State _stateToTransistion;
    private Animator _animator;
    
    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _controller.EnemyAttackState = (EnemyAttackState)_stateToTransistion;
        _animator = controller.GetComponent<Animator>();
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _animator.Play("PumpkinTransition");
    }
    

    public override void OnExit()
    {
        base.OnExit(); 
    }
}
