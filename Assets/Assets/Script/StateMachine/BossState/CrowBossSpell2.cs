using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "States/CrowBossSpell2")]
public class CrowBossSpell2 : State
{
    private StateController _controller;
    private Collider[] _colliders;
    [SerializeField] private State _stateToTransistion;
    private Animator _animator;
    private bool CanTransition;

    public GameObject Spell2Projectiles;
    public GameObject newProjectiles;

    [HideInInspector]
    public List<Transform> spawnPos;


    public override void OnEnter(StateController controller)
    {
        _controller = controller;
        _controller.CrowBossSpell2 = this;
        spawnPos = _controller.GetComponentInChildren<GetStaffPosition>().spawnPosSpell2;
        _animator = _controller.GetComponent<Animator>();
        _target = FindFirstObjectByType<PlayerController>().transform;
        _animator.speed = 1f;
        _animator.Play("CrowBossSpell2");
        
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _controller.Transistion(_stateToTransistion);
        }
    }
    
    [SerializeField] private Transform _target;
    
    public IEnumerator SpawnProjectiles()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Spell2Projectiles, spawnPos[i].position, spawnPos[i].transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.8f);
        
        for (int i = -1; i <= 1; i++)
        {
            spawnNewProjectiles(i*5);
            yield return new WaitForSeconds(0.3f);
        }
        


    }

    private void spawnNewProjectiles(float xpos)
    {
        
        Instantiate(newProjectiles, _target.position + new Vector3(xpos, 15, 0), quaternion.identity);
    }

    public override void OnExit()
    {
        _animator.SetTrigger("Recover");
        base.OnExit(); 
    }
}
