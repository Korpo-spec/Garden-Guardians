using System;
using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

   public PlayerMovementStats movementStats;
   
   private PlayerInputHandler _inputHandler;
   private CharacterController _controller;
   private Rigidbody _thisRb;
   
   public Animator animator;

   private Vector3 _prevDirVector;
   private bool canMove=true;


   private void Awake()
   {
      
   }

   private void Start()
   {
      animator = GetComponent<Animator>();
      _controller = GetComponent<CharacterController>();
      _inputHandler = GetComponent<PlayerInputHandler>();
      _prevDirVector = transform.forward;
   }

   private void Update()
   {
      _inputHandler.GetPlayerInput();
   }

   private void FixedUpdate()
   {
      if (!canMove) return;
      Move(_inputHandler.moveDir,movementStats.speed*Time.fixedDeltaTime);
      if (_inputHandler.isDashing)
      {
         _inputHandler.isDashing = false;
         StartCoroutine(Dash());
      }
      RotatePlayer(_inputHandler.moveDir);

   }

   private void Move(Vector3 moveDir, float speed)
   {
      Vector3 moveVector3 = new Vector3(moveDir.x, 0, moveDir.z).normalized;
      _controller.Move(moveVector3*speed);
      animator.SetBool("AmRunning",_inputHandler.moveDir.magnitude != 0);
   }

   private void RotatePlayer(Vector3 targetDirection)
   {
      if (_inputHandler.moveDir.magnitude==0)
      {
         transform.forward = _prevDirVector;
      }
      else
      {
         Vector3 dirVector3 = new Vector3(targetDirection.x, 0, targetDirection.z);
         transform.forward = dirVector3;
         _prevDirVector = dirVector3;
      }
      
   }

   private IEnumerator Dash()
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
      if (_inputHandler)
      {
         _inputHandler.moveDir = Vector2.zero;
      }
   }
}
