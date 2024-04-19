using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    
    public void OptionsPressed()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CreditsPressed()
    {
        startMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
}
