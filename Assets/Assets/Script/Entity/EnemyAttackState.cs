using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using UnityEngine;

[CreateAssetMenu(menuName = "States/EnemyAttackState")]
public class EnemyAttackState : State
{
    
    private StateController _controller;
    private EntityMovement _movement;
    private Animator _animator;
    private Collider[] _colliders;
    private float _time;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _radius;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private Faction _factionToAttack;
    [SerializeField] private State _stateToTransistion;
    [SerializeField] private AttackModule _attackModule;
    [SerializeField] private Transform _target;
    [SerializeField] private bool recover = true;
    

    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _colliders = new Collider[15];
        _movement = controller.GetComponent<EntityMovement>();
        _animator = controller.GetComponent<Animator>();
        _time = _attackSpeed + 1;
        int amount = Physics.OverlapSphereNonAlloc(_controller.transform.position, _radius, _colliders, _mask);

        for (int i = 0; i < amount; i++)
        {
            if (_colliders[i].TryGetComponent<EntityFaction>(out var faction))
            {
                if (faction.faction == _factionToAttack)
                {
                    _target = _colliders[i].gameObject.transform;
                    _movement.SetDestinationTo(_target.position.RemoveY());
                    return;
                }
            }
            else
            {
                continue;
            }
        }
    }

    public override void UpdateState()
    {
        
        if (_radius<=Vector3.Distance(_controller.transform.position.RemoveY(),_target.position.RemoveY())&&_stateToTransistion)
        {
            _controller.Transistion(_stateToTransistion);
            return;
        }
        if (_movement.afterAttack)
        {
            _movement.SetDestinationTo(_target.position.RemoveY());
            _movement.UpdateMoveTo();
            
        }
        
        if (_attackSpeed > _time)
        {
            
            
            _time += Time.deltaTime;
            _movement.moveToPos = _target.position.RemoveY();
            return;
        }

       
        
        
        //turn enemy in direction of player
        
        _movement.RotatePlayer((_target.position-_movement.transform.position));
        if (_attackRange >= Vector3.Distance(_controller.transform.position.RemoveY(), _target.position.RemoveY()))
        {
            _movement.afterAttack = false;
            _attackModule.Attack(_animator);
            if (recover)
            {
                _animator.SetTrigger("Recover");
                
            }
            
            _time = 0;
        }
        else
        {
            _movement.SetDestinationTo(_target.position.RemoveY());
            _movement.UpdateMoveTo();
        }
    }
    
    
}
