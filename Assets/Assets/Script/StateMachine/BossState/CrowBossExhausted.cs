using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/BossExhausted")]
public class CrowBossExhausted : State
{

    private float exhaustedTimer=8;
    
    private StateController _controller;
    private Collider[] _colliders;
    [SerializeField] private State _stateToTransistion;
    private Animator _animator;
    private bool CanTransition;

    private List<Transform> spawnPos;


    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _animator = _controller.GetComponent<Animator>();
        _animator.Play("CrowBossExhausted");
    }

    private float timer;
    public override void UpdateState()
    {
        timer += Time.deltaTime;

        if (timer>exhaustedTimer)
        {
            _controller.Transistion(_stateToTransistion);
        }
    }
    

    public override void OnExit()
    {
        timer = 0;
        base.OnExit(); 
    }
}
