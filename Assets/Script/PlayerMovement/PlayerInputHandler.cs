using UnityEngine;
using UnityEngine.Serialization;

namespace Script.PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {

        [HideInInspector] public Vector3 moveDir;

        [SerializeField] private KeyCode dashKey;

        [HideInInspector]public bool isDashing;

        [HideInInspector] public bool attackButtonPressed;

        [SerializeField] public int staminaPoints;

        [SerializeField] private int staminaPointMax;

        private int timer;
        private int resetTime = 390;

        [SerializeField] private GameObject point1;

        [SerializeField] private GameObject point2;
    
        // Start is called before the first frame update
        void Start()
        {
            staminaPointMax = 2;
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
                timer = resetTime;
            }
            
            // Show how many stamina point you have
            if (staminaPoints > 0)
            {
                point1.SetActive(true);
            }
            else
            {
                point1.SetActive(false);
            }
            
            if (staminaPoints > 1)
            {
                point2.SetActive(true);
            }
            else
            {
                point2.SetActive(false);
            }
        }

        public void GetPlayerInput()
        {
            GetStandardMovementInput(Camera.main.transform.rotation);  

            if (Input.GetKeyDown(dashKey) && staminaPoints > 0)
            {
                isDashing = true;
                staminaPoints--;
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
