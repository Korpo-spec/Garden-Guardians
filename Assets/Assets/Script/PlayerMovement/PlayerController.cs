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
      
      if (_inputHandler.isDashing)
      {
         CalculateMoveDirFromMouse();
         _inputHandler.isDashing = false;
         _movement.Dash(_inputHandler.moveDir);
      }
      else if (_inputHandler.attackButtonPressed)
      {
         CalculateMoveDirFromMouse();
         _inputHandler.attackButtonPressed = false;
         attackModule.Attack(animator);
         
      }
      _movement.Move(_inputHandler.moveDir,movementStats.speed*Time.fixedDeltaTime);

   }
   private void OnEnable()
   {
      if (_inputHandler)
      {
         _inputHandler.moveDir = Vector2.zero;
      }
   }

   public LayerMask Mask;
   private void CalculateMoveDirFromMouse()
   {

      var cam = Camera.main;
      Vector3 point = Input.mousePosition;
      point.z = 100;
      //Vector2 mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);


      Ray ray = cam.ScreenPointToRay(point);
      RaycastHit hit;
      if (Physics.Raycast(ray,out hit,100,Mask))
      {
         
         var direction = hit.point-transform.position;
         direction = direction.normalized;
         direction = new Vector3(direction.x, 0, direction.z);
         _inputHandler.moveDir = direction.normalized;
         Debug.DrawLine(transform.position,_inputHandler.moveDir,Color.black,5);
      }
   }
}
