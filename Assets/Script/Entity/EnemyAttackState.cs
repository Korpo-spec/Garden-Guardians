using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/EnemyAttackState")]
public class EnemyAttackState : State
{
    
    private StateController _controller;
    private Collider[] _colliders;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _radius;
    [SerializeField] private Faction _factionToAttack;
    [SerializeField] private State _stateToTransistion;

    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _colliders = new Collider[15];
    }

    public override void UpdateState()
    {
        int amount = Physics.OverlapSphereNonAlloc(_controller.transform.position, _radius, _colliders, _mask);

        for (int i = 0; i < amount; i++)
        {
            if (_colliders[i].TryGetComponent<EntityFaction>(out var faction))
            {
                if (faction.faction == _factionToAttack)
                {
                    _controller.Transistion(_stateToTransistion);
                }
            }
            else
            {
                continue;
            }
        }
    }
}
