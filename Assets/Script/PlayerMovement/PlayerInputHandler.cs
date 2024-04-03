using UnityEngine;

namespace Script.PlayerMovement
{
    public class PlayerInputHandler : MonoBehaviour
    {

        [HideInInspector] public Vector2 move;

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
            move = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(dashKey))
            {
                isDashing = true;
            }
        }
    }
}
