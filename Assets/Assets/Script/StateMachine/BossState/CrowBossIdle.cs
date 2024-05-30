using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/CrowBossIdle")]
public class CrowBossIdle : State
{
    private StateController _controller;
    private Collider[] _colliders;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _radius;
    [SerializeField] private Faction _factionToAttack;
    [SerializeField] private State _stateToTransistion;
    private bool CanTransition;

    
    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _colliders = new Collider[15];
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _controller.Transistion(_stateToTransistion);
        }
    }

    public override void OnExit()
    {
        base.OnExit(); 
    }
}
