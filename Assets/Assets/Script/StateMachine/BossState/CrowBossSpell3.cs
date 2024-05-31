using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using UnityEngine;
[CreateAssetMenu(menuName = "States/CrowBossMeele")]
public class CrowBossSpell3 : State
{
    private StateController _controller;
    private EntityMovement _movement;
    private Animator _animator;
    private float _time;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private State _stateToTransistion1;
    [SerializeField] private State _stateToTransistion2;
    [SerializeField] private AttackModule _attackModule;
    [SerializeField] private Transform _target;
    [SerializeField] private bool recover = true;
    private bool canRotate;

    public override void OnEnter(StateController controller)
    {
        canRotate = true;
        _controller = controller;
        _target = FindFirstObjectByType<PlayerController>().transform;
        _movement = controller.GetComponent<EntityMovement>();
        _animator = controller.GetComponent<Animator>();
        _time = _attackSpeed + 1;
        
    }

    public override void UpdateState()
    {
        

       
        //turn enemy in direction of player
        if (canRotate)
        {
            _movement.RotatePlayer((_target.position-_movement.transform.position));
        }
        
        if (_attackRange >= Vector3.Distance(_controller.transform.position.RemoveY(), _target.position.RemoveY()))
        {
            if (_attackSpeed > _time)
            {
            
                _time += Time.deltaTime;
                _movement.moveToPos = _target.position.RemoveY();
                return;
            } 
            
            _movement.afterAttack = false;
            _attackModule.Attack(_animator);
            canRotate = false;
            if (recover)
            {
                _animator.SetTrigger("Recover");
                
            }
            
            _time = 0;
        }
        else
        {
            var random = Random.Range(1, 3);
            _controller.Transistion(random == 1 ? _stateToTransistion1 : _stateToTransistion2);
        }
    }

    public override void OnAnimatorEvent(string eventName)
    {
        canRotate = true;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
