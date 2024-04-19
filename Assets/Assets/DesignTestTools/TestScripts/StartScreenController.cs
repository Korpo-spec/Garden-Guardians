using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Break = Unity.VisualScripting.Break;

public class StartScreenController : MonoBehaviour
{
    // Main Menu
    [SerializeField] private GameObject startMenu;
    
    // Sub-Menus of Main Menu
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    
    // Options Menu Categories
    [SerializeField] private GameObject soundOptions;
    [SerializeField] private GameObject controlsOption;
    [SerializeField] private GameObject keyBindingsOption;
    [SerializeField] private GameObject displayOptions;
    
    [SerializeField] private GameObject errorMessage;

    [SerializeField] private int menuLayer;
    
    private void Start()
    {
        menuLayer = 0;
        
        // Activates and Deactivates everything correctly regardless of edits in editor
        startMenu.SetActive(true);
        soundOptions.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        controlsOption.SetActive(false);
        keyBindingsOption.SetActive(false);
        displayOptions.SetActive(false);
    }
    
    
    
    
    // ------ Buttons ------
    public void BackButtonPressed()
    {
        menuLayer = 0;
        ShowMenu();
    }
    public void OptionsPressed()
    {
        menuLayer = 1;
        ShowMenu();
    }
    
    public void CreditsPressed()
    {
        menuLayer = 2;
        ShowMenu();
    }

    public void SoundButtonPressed()
    {
        menuLayer = 3;
        ShowMenu();
    }
    
    public void ControlsButtonPressed()
    {
        menuLayer = 4;
        ShowMenu();
    }
    
    public void KeyBindingsButtonPressed()
    {
        menuLayer = 5;
        ShowMenu();
    }
    
    public void DisplayButtonPressed()
    {
        menuLayer = 6;
        ShowMenu();
    }

    public void NewGamePressed()
    {
        //SceneManager.LoadScene("");
    }

    public void ShowErrorMessage()
    {
        errorMessage.SetActive(true);
    }

    public void QuitGamePressed()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
    
    
    
    
    // ------ Keyboard Commands ------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            menuLayer = 0;
            ShowMenu();
        }
    }
    
    
    
    
    // ------ Controller for when to show what menu on screen ------
    private void ShowMenu()
    {
        switch (menuLayer)
        {
            case 6:
                ResetOptions();
                displayOptions.SetActive(true);
                break;
            
            case 5:
                ResetOptions();
                keyBindingsOption.SetActive(true);
                break;
            
            case 4:
                ResetOptions();
                controlsOption.SetActive(true);
                break;
            
            case 3:
                ResetOptions();
                soundOptions.SetActive(true);
                break;
            
            case 2:
                ResetMenu();
                creditsMenu.SetActive(true);
                break;
            
            case 1:
                ResetMenu();
                optionsMenu.SetActive(true);
                break;
            
            default:
                ResetMenu();
                startMenu.SetActive(true);
                break;
        }
    }
    
    
    
    
    // ------ Wipes the screen before activating target menu object ------
    private void ResetMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ResetOptions()
    {
        soundOptions.SetActive(false);
        controlsOption.SetActive(false);
        keyBindingsOption.SetActive(false);
        displayOptions.SetActive(false);
    }
}
