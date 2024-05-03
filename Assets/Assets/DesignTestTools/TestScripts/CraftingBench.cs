using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject craftingMenu;

    private void OnTriggerEnter(Collider other)
    {
        prompt.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        prompt.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            prompt.SetActive(false);
            craftingMenu.SetActive(true);
        }
    }
}
