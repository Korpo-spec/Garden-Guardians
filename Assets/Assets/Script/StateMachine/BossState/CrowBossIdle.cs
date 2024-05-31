using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "States/CrowBossIdle")]
public class CrowBossIdle : State
{
    private StateController _controller;
    [SerializeField] private State Exhausted;
    [SerializeField] private State Spell1;
    [SerializeField] private State Spell2;
    [SerializeField] private State Meele;
    private bool CanTransition;

    private int CountToExhasuted=4;

    private EntityMovement _movement;
    private float _time;
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _target;

    private void Awake()
    {
        _target = FindFirstObjectByType<PlayerController>().transform;
    }

    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        
        if (_controller.Count==CountToExhasuted)
        {
            _controller.Count = 0;
            _controller.Transistion(Exhausted);
        }
    }
    
    
    
    public bool StartBoss=true;

    private float timer;
    private float Waitfor=3;
    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer>Waitfor)
        {
            if (_attackRange >= Vector3.Distance(_controller.transform.position.RemoveY(), _target.position.RemoveY()))
            {
            
                _controller.Transistion(Meele);
            }
            else
            {
                var random = Random.Range(1, 3);
                _controller.Transistion(random == 1 ? Spell1 : Spell2);
            }
            
        }
        
        
    }

    public override void OnExit()
    {
        timer = 0;
        _controller.Count++;
        Debug.Log(_controller.Count);
        base.OnExit(); 
    }
}
