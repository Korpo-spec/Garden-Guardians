using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
{ 
   public PlayerMovementStats movementStats;
   private CharacterController _controller;
   private Rigidbody _thisRb;
   private Vector3 _prevDirVector;

   public bool canMove=true;

   private NavMeshAgent _agent;
   private int _currentCorner = 1;
   public Vector3 prevDirVector3 => _prevDirVector;
   public CharacterController controller => _controller;
   public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
       _controller = GetComponent<CharacterController>();
       TryGetComponent<NavMeshAgent>(out _agent);
       _prevDirVector = transform.forward;
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
       controller.Move(ApplyGravity(Vector3.zero));
    }

    public void SetDestinationTo(Vector3 destination)
    {
       if (_agent == null) return;
       _agent.destination = destination;
       _agent.isStopped = true;
    }
    public void UpdateMoveTo()
    {
       if (_agent.path.corners.Length <= 1)
       {
          return;
       }
        
       Vector3 movDir = _agent.path.corners[_currentCorner] - transform.position;
       movDir.y = 0;

       Move(movDir, movementStats.speed * Time.deltaTime);
    }
    
    public void Move(Vector3 moveDir, float speed)
    {
       if (!canMove) return;
       RotatePlayer(moveDir);
       Vector3 moveVector3 = new Vector3(moveDir.x, 0, moveDir.z).normalized;
       
       _controller.Move(moveVector3*speed);
       animator.SetBool("AmRunning",moveDir.magnitude != 0);
    }

    private float gravity = 0;
    private Vector3 ApplyGravity(Vector3 moveVec)
    {
       gravity =- 9.81f * Time.fixedDeltaTime*2;
       moveVec.y = gravity;
       if (controller.isGrounded)
       {
          gravity = 0;
          return moveVec;
       }
       
      
       
       return moveVec;

    }
 
    private void RotatePlayer( Vector3 moveDir)
    {
       if (moveDir.magnitude==0)
       {
          transform.forward = _prevDirVector;
       }
       else
       {
          Vector3 dirVector3 = new Vector3(moveDir.x, 0, moveDir.z);
          transform.forward = dirVector3;
          _prevDirVector = dirVector3;
       }
       
    }
    private IEnumerator WeaponDash()
    {
       float startTime = Time.time;
       Vector3 dashvector = _prevDirVector;
       canMove = false;
       while (Time.time<startTime+movementStats.dashTime)
       {
          _controller.Move(dashvector.normalized* (movementStats.dashSpeed*0.5f * Time.deltaTime));
          yield return null;
       }
       canMove = true;
 
    }

    public void Dash(Vector3 dashVector)
    {
       if (!canMove) return;
       StartCoroutine(InternalDash(dashVector));
    }
 
    private IEnumerator InternalDash(Vector3 dashVector3)
    {
       float startTime = Time.time;
       Vector3 dashvector = dashVector3;
       canMove = false;
       animator.SetBool("Dash",true);
       while (Time.time<startTime+movementStats.dashTime)
       {
          _controller.Move(dashvector.normalized* (movementStats.dashSpeed * Time.deltaTime));
          RotatePlayer(dashVector3);
          yield return null;
       }
       animator.SetBool("Dash",false);
       canMove = true;
 
    }
    
 
    private void OnEnable()
    {
       
    }
}
