using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{ 
   public PlayerMovementStats movementStats;
   private CharacterController _controller;
   private Rigidbody _thisRb;
   private Vector3 _prevDirVector;
   public bool canMove=true;


   public Vector3 prevDirVector3 => _prevDirVector;
   public CharacterController controller => _controller;
   public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
       _controller = GetComponent<CharacterController>();
       _prevDirVector = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Move(Vector3 moveDir, float speed)
    {
       if (!canMove) return;
       RotatePlayer(moveDir);
       Vector3 moveVector3 = new Vector3(moveDir.x, 0, moveDir.z).normalized;
       _controller.Move(moveVector3*speed);
       animator.SetBool("AmRunning",moveDir.magnitude != 0);
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

    public void Dash()
    {
       if (!canMove) return;
       StartCoroutine(InternalDash());
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
          yield return null;
       }
       animator.SetBool("Dash",false);
       canMove = true;
 
    }
    
 
    private void OnEnable()
    {
       
    }
}
