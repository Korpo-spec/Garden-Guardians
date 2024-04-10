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
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GetPlayerInput()
        {
            GetStandardMovementInput(Camera.main.transform.rotation);  

            if (Input.GetKeyDown(dashKey))
            {
                isDashing = true;
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
