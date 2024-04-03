using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   public PlayerMovementStats MovementStats;
   
   private PlayerInputHandler _inputHandler;
   private CharacterController Controller;
   private Rigidbody _thisRb;
   
   public Animator animator;

   private Vector3 _prevDirVector;


   private void Awake()
   {
      
   }

   private void Start()
   {
      animator = GetComponent<Animator>();
      Controller = GetComponent<CharacterController>();
      _inputHandler = GetComponent<PlayerInputHandler>();
      _prevDirVector = transform.forward;
   }

   private void Update()
   {
      _inputHandler.GetPlayerInput();
   }

   private void FixedUpdate()
   {
      
      Move(_inputHandler.move,MovementStats.speed*Time.fixedDeltaTime);
      if (_inputHandler.isDashing)
      {
         _inputHandler.isDashing = false;
         StartCoroutine(Dash());
      }
      RotatePlayer(_inputHandler.move);
   }

   private void Move(Vector2 moveDir, float speed)
   {
      Vector3 moveVector3 = new Vector3(moveDir.x, 0, moveDir.y).normalized;
      Controller.Move(moveVector3*speed);
      animator.SetBool("AmRunning",_inputHandler.move.magnitude != 0);
   }

   private void RotatePlayer(Vector2 targetDirection)
   {
      if (_inputHandler.move.magnitude==0)
      {
         transform.forward = _prevDirVector;
      }
      else
      {
         Vector3 dirVector3 = new Vector3(targetDirection.x, 0, targetDirection.y);
         transform.forward = dirVector3;
         _prevDirVector = dirVector3;
      }
      
   }

   private IEnumerator Dash()
   {
      Debug.Log("dashed");
      yield break;
   }

   private void OnEnable()
   {
      if (_inputHandler)
      {
         _inputHandler.move = Vector2.zero;
      }
   }
}
