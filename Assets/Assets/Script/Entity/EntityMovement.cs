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
   public bool canDashCancel = false;

   private NavMeshAgent _agent;
   private int _currentCorner = 1;

   public Vector3 prevDirVector3
   {
      get
      {
         return _prevDirVector;
      }
      set
      {
         _prevDirVector = value;
      }
   }
   public CharacterController controller => _controller;
   public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       //Time.timeScale = 0.25f;
       animator = GetComponent<Animator>();
       _controller = GetComponent<CharacterController>();
       TryGetComponent<NavMeshAgent>(out _agent);
       _prevDirVector = transform.forward;

       if (!gameObject.CompareTag("Player"))
       {
          movementStats=Instantiate(movementStats);
       }
       

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
       if (canDashCancel)
       {
          _prevDirVector = moveDir.magnitude > 0 ? moveDir : _prevDirVector;
       }
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
 
    public void RotatePlayer( Vector3 moveDir)
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

    public void KnockBack(Vector3 knockBack)
    {
       if (!canMove) return;
       StopAllCoroutines();
       StartCoroutine(InternalKnockBack(knockBack));
       
    }
    public void Dash()
    {
       if (!canMove && !canDashCancel) return;
       if (canDashCancel)
       {
          animator.ResetTrigger("Combat");
          canDashCancel = false;
       }
       StartCoroutine(InternalDash());
    }

    [SerializeField] private float knockBackDuration = 1f;
    private IEnumerator InternalKnockBack(Vector3 dir)
    {
       float startTime = 0;
       float timeMult = 1f / knockBackDuration;
       Vector3 knockVector = dir;
       canMove = false;
       while (startTime < 1)
       {
          _controller.Move(knockVector * (Time.deltaTime * timeMult));
          RotatePlayer(_prevDirVector);
          startTime += Time.deltaTime * timeMult;
          yield return new WaitForEndOfFrame();
       }
       canMove = true;
 
    }
 
    private IEnumerator InternalDash()
    {
       float startTime = Time.time;
       Vector3 dashvector = _prevDirVector;
       canMove = false;
       animator.SetBool("Dash",true);
       while (Time.time<startTime+movementStats.dashTime)
       {
          _controller.Move(dashvector.normalized* (movementStats.dashSpeed * Time.deltaTime));
          RotatePlayer(_prevDirVector);
          yield return null;
       }
       animator.SetBool("Dash",false);
       canMove = true;
 
    }
    
 
    private void OnEnable()
    {
       
    }
}
