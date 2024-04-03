using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [HideInInspector] public Vector2 move;

    [SerializeField] private KeyCode DashKey;

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

        if (Input.GetKeyDown(DashKey))
        {
            isDashing = true;
        }
    }
}
