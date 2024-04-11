using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using UnityEngine;

[CreateAssetMenu(menuName = "States/EnemyAttackState")]
public class EnemyAttackState : State
{
    
    private StateController _controller;
    private EntityMovement _movement;
    private Collider[] _colliders;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _radius;
    [SerializeField] private Faction _factionToAttack;
    [SerializeField] private State _stateToTransistion;
    [SerializeField] private AttackModule _attackModule;
    [SerializeField] private Transform _target;
    

    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _colliders = new Collider[15];
        _movement = controller.GetComponent<EntityMovement>();
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
        _movement.SetDestinationTo(_target.position.RemoveY());
        _movement.UpdateMoveTo();
    }
}
