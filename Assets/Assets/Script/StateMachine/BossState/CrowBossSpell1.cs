using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/CrowBossSpell")]
public class CrowBossSpell1 : State
{
    private StateController _controller;
    private Collider[] _colliders;
    [SerializeField] private State _stateToTransistion;
    private Animator _animator;
    private bool CanTransition;

    public GameObject Spell1Projectiles;

    private List<Transform> spawnPos;


    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _controller.CrowBossSpell1 = this;
        _movement = _controller.GetComponent<EntityMovement>();
        _target = FindFirstObjectByType<PlayerController>().transform;
        spawnPos = _controller.GetComponentInChildren<GetStaffPosition>().spawnPosSpell1;
        _animator = _controller.GetComponent<Animator>();
        _animator.Play("CrowBossSpell1");
    }

    
    [SerializeField] private Transform _target;
    private EntityMovement _movement;
    public IEnumerator SpawnProjectiles()
    {
        _movement.RotatePlayer((_target.position-_movement.transform.position));
        for (int i = 0; i < spawnPos.Count; i++)
        {
            Instantiate(Spell1Projectiles,spawnPos[i].position , spawnPos[i].transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        
        for (int i = spawnPos.Count-1; i >= 0; i--)
        {
            Instantiate(Spell1Projectiles,spawnPos[i].position , spawnPos[i].transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        
        
        _controller.Transistion(_stateToTransistion);
        
        yield return new WaitForEndOfFrame();

        
        
    }

    public override void OnAnimatorEvent(string eventName)
    {
        _controller.StartCoroutine(SpawnProjectiles());
    }

    public override void OnExit()
    {
        //_animator.SetTrigger("Recover");
        base.OnExit(); 
    }
    
}
