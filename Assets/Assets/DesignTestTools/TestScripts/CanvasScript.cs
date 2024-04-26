using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    // ------ Functionality ------
    private bool gameStarted;
    
    // Get script for stamina points
    public PlayerInputHandler playerInputHandler;
    
    // ------ Objects In Canvas ------
    // HUD
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject sPoint1;
    [SerializeField] private GameObject sPoint2;
    
    // Inventory
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject equipmentNMaterials;
    [SerializeField] private GameObject craftingOverview;
    [SerializeField] private Button inventoryButton;
    
    // Crafting Menu
    [SerializeField] private GameObject craftingMenu;
    [SerializeField] private GameObject mainHandObject;
    [SerializeField] private GameObject offHandObject;
    [SerializeField] private GameObject armObject;
    [SerializeField] private GameObject legObject;
    
    
    void Start()
    {
        gameStarted = true;
        
        // Sets all UI objects to correct Active or Inactive state
        hud.SetActive(true);
        inventoryMenu.SetActive(false);
        equipmentNMaterials.SetActive(false);
        craftingOverview.SetActive(false);
        inventoryButton.onClick.Invoke();
        craftingMenu.SetActive(false);
        mainHandObject.SetActive(false);
        offHandObject.SetActive(false);
        armObject.SetActive(false);
        legObject.SetActive(false);
    }
    
    

    void Update()
    {
        // Activates inventory and deactivates HUD when inventory is open. Activates HUD when inventory closes
        if (Input.GetKeyDown(KeyCode.Tab) && inventoryMenu.activeSelf == false)
        {
            hud.SetActive(false);
            inventoryMenu.SetActive(true);

            if (gameStarted == true)
            {
                inventoryButton.Select();
                gameStarted = false;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && inventoryMenu.activeSelf == true)
        {
            inventoryMenu.SetActive(false);
            hud.SetActive(true);
        }
        
        // Check signal if amount of stamina points have changed
        if (playerInputHandler.staminaPointsChanged == true)
        {
            playerInputHandler.staminaPointsChanged = false;
            UpdateStaminaPoints();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            craftingMenu.SetActive(true);
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

    
    
    // Buttons to switch between crafting overview and inventory
    public void OpenCraftingOverview()
    {
        equipmentNMaterials.SetActive(false);
        craftingOverview.SetActive(true);
    }

    public void OpenEquipmentNMaterials()
    {
        equipmentNMaterials.SetActive(true);
        craftingOverview.SetActive(false);
    }
    
    
    
    // Buttons for Crafting Menu
    public void CraftingSlotMainHand()
    {
        mainHandObject.SetActive(true);
        offHandObject.SetActive(false);
        armObject.SetActive(false);
        legObject.SetActive(false);
    }

    public void CraftingSlotOffHand()
    {
        mainHandObject.SetActive(false);
        offHandObject.SetActive(true);
        armObject.SetActive(false);
        legObject.SetActive(false);
    }

    public void CraftingSlotArm()
    {
        mainHandObject.SetActive(false);
        offHandObject.SetActive(false);
        armObject.SetActive(true);
        legObject.SetActive(false);
    }

    public void CraftingSlotLeg()
    {
        mainHandObject.SetActive(false);
        offHandObject.SetActive(false);
        armObject.SetActive(false);
        legObject.SetActive(true);
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
