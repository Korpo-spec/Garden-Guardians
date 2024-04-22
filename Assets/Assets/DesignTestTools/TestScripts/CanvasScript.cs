using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    // Get script for stamina points
    public PlayerInputHandler playerInputHandler;
    
    // ------ Objects In Canvas ------
    // HUD
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject sPoint1;
    [SerializeField] private GameObject sPoint2;
    
    // Inventory
    [SerializeField] private GameObject inventory;
    
    
    
    
    void Start()
    {
        // Sets all UI objects to correct Active or Inactive state
        hud.SetActive(true);
        inventory.SetActive(false);
    }
    
    

    void Update()
    {
        // Activates inventory and deactivates HUD when inventory is open. Activates HUD when inventory closes
        if (Input.GetKeyDown(KeyCode.Tab) && inventory.activeSelf == false)
        {
            hud.SetActive(false);
            inventory.SetActive(true);
        }
        else if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && inventory.activeSelf == true)
        {
            inventory.SetActive(false);
            hud.SetActive(true);
        }
        
        // Check signal if amount of stamina points have changed
        if (playerInputHandler.staminaPointsChanged == true)
        {
            playerInputHandler.staminaPointsChanged = false;
            UpdateStaminaPoints();
        }
    }
    
    
    
    // Closes active menu and reactivates HUD on Button Press
    public void CloseMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
        hud.SetActive(true);
    }
    
    
    
    // Updates the UI to match amount of stamina the player has
    private void UpdateStaminaPoints()
    {
        switch (playerInputHandler.staminaPoints)
        {
            case 2:
                sPoint2.SetActive(true);
                sPoint1.SetActive(true);
                break;
            
            case 1:
                sPoint2.SetActive(false);
                sPoint1.SetActive(true);
                break;
        
            default:
                sPoint2.SetActive(false);
                sPoint1.SetActive(false);
                break;
        }
    }
}
