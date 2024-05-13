using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Script.PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {

        [HideInInspector] public Vector3 moveDir;
        
        [HideInInspector] public Vector3 attackDir;

        [SerializeField] private KeyCode dashKey;

        [HideInInspector]public bool isDashing;

        [HideInInspector] public bool attackButtonPressed;

        [SerializeField] public int staminaPoints;

        [SerializeField] private int staminaPointMax;
        
        [SerializeField] public bool staminaPointsChanged;

        private int timer;
        private int resetTime = 60;

        [SerializeField] private GameObject point1;

        [SerializeField] private GameObject point2;

       
    
        // Start is called before the first frame update
        void Start()
        {
            staminaPointMax = 1;
            staminaPoints = staminaPointMax;
            timer = resetTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (staminaPoints < staminaPointMax)
            {
                timer--;
            }

            if (timer <= 0 && staminaPoints < staminaPointMax)
            {
                staminaPoints++;
                staminaPointsChanged = true;
                timer = resetTime;
            }

            // Show how many stamina point you have
            
        }

        public void GetPlayerInput()
        {
            GetStandardMovementInput(Camera.main.transform.rotation);  

            if (Input.GetKeyDown(dashKey) && staminaPoints > 0)
            {
                isDashing = true;
                staminaPoints--;
                staminaPointsChanged = true;
                timer = resetTime;
            }
            if (Input.GetMouseButton(0))
            {
                attackButtonPressed = true;
            }
            else
            {
                attackButtonPressed = false;
            }
            
        }

        private void GetStandardMovementInput(Quaternion rotation)
        {
            var worldSpaceMoveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
            moveDir = worldSpaceMoveDir.Vector3WorldSpaceToIsometricSpace(rotation.eulerAngles);
        }
        
        
    }
}
