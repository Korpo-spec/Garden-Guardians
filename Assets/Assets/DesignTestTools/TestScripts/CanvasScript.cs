using System;
using System.Collections;
using System.Collections.Generic;
using Script.PlayerMovement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    // ------ Functionality ------
    private bool gameStarted;
    private int layerInt;
    
    // Get script for stamina points
    public PlayerInputHandler playerInputHandler;
    
    // ------ Objects In Canvas ------
    // HUD
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject sPoint1;
    [SerializeField] private GameObject sPoint2;
    [SerializeField] private GameObject FadeController;
    
    // Inventory
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject equipmentNMaterials;
    [SerializeField] private GameObject craftingOverview;
    [SerializeField] private Button inventoryButton;
    
    // Pause Menu
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseSubMenu;
    
    // Crafting Menu
    [SerializeField] private GameObject craftingMenu;
    [SerializeField] private GameObject mainHandObject;
    [SerializeField] private GameObject offHandObject;
    [SerializeField] private GameObject armObject;
    [SerializeField] private GameObject legObject;
    
    
    void Start()
    {
        gameStarted = true;
        layerInt = 0;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
        FadeController.SetActive(true);
        hud.SetActive(true);
        inventoryButton.onClick.Invoke();
        sPoint2.SetActive(false);
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
            //UpdateStaminaPoints();
        }
        
        // Activate Pause Menu
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     if (craftingMenu.activeSelf == false)
        //     {
        //         craftingMenu.SetActive(true);
        //         hud.SetActive(false);
        //     }
        //     else
        //     {
        //         craftingMenu.SetActive(false);
        //         hud.SetActive(true);
        //     }
        // }
        
        // Activate Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf == false)
            {
                OpenPauseMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    
    
    
    // Closes active menu and reactivates HUD on Button Press
    public void CloseMenu()
    {
        /*for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }*/
        pauseMenu.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1f;
    }
    
    //Opens the pause menu and disables the HUD
    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0f;
    }
    
    //Goes back to Pause Menu from the Sub Menu selected when setting it in a button action
    public void BackToPause(GameObject gameObjectToClose)
    {
        gameObjectToClose.SetActive(false);
        pauseSubMenu.SetActive(true);
    }
    
    //Opens the Sub Menu that's selected when setting it in a button action
    public void OpenSubMenu(GameObject subMenu)
    {
        pauseSubMenu.SetActive(false);
        subMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
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
    // private void UpdateStaminaPoints()
    // {
    //     switch (playerInputHandler.staminaPoints)
    //     {
    //         case 2:
    //             sPoint2.SetActive(true);
    //             sPoint1.SetActive(true);
    //             break;
    //         
    //         case 1:
    //             sPoint2.SetActive(false);
    //             sPoint1.SetActive(true);
    //             break;
    //     
    //         default:
    //             sPoint2.SetActive(false);
    //             sPoint1.SetActive(false);
    //             break;
    //     }
    // }
}
