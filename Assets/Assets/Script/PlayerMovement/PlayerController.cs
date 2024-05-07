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

   public bool inDialogue;
   private void FixedUpdate()
   {
      if (inDialogue) { return; }

      CalculateMoveDirFromMouse();
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

   private void CalculateMoveDirFromMouse()
   {

      var cam = Camera.main;
      Vector3 point = new Vector3();
      Vector2 mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
      

      point = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y, cam.nearClipPlane));

      
      var moveDir = transform.position-point ;
      Debug.Log(moveDir.normalized);
      Debug.DrawLine(transform.position,moveDir);
      //moveDir = moveDir.normalized;
      //_inputHandler.moveDir = moveDir;
   }
}
