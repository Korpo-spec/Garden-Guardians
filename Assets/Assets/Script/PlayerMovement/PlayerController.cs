using System;
using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

   public PlayerMovementStats movementStats;
   [SerializeField]public AttackModule attackModule;
   public PlayerAudio PlayerAudio;
   
   private PlayerInputHandler _inputHandler;
   private CharacterController _controller;
   private Rigidbody _thisRb;
   private EntityMovement _movement;
   
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
      _movement = GetComponent<EntityMovement>();
      _prevDirVector = transform.forward;
   }

   private void Update()
   {
      _inputHandler.GetPlayerInput();
   }

   private void FixedUpdate()
   {
      
      _movement.Move(_inputHandler.moveDir,movementStats.speed*Time.fixedDeltaTime);
      
      if (_inputHandler.isDashing)
      {
         _inputHandler.isDashing = false;
         _movement.Dash();
      }
      else if (_inputHandler.attackButtonPressed)
      {
         _inputHandler.attackButtonPressed = false;
         attackModule.Attack(animator);
         
      }
      

   }
   private void OnEnable()
   {
      if (_inputHandler)
      {
         _inputHandler.moveDir = Vector2.zero;
      }
   }
}
