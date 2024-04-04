using UnityEngine;
using UnityEngine.Serialization;

namespace Script.PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {

        [HideInInspector] public Vector3 moveDir;

        [SerializeField] private KeyCode dashKey;

        [HideInInspector]public bool isDashing;
    
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
            var worldSpaceMoveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
            moveDir = worldSpaceMoveDir.Vector3WorldSpaceToIsometricSpace();
            
            Debug.Log(moveDir);
            if (Input.GetKeyDown(dashKey))
            {
                isDashing = true;
            }
        }
    }
}
