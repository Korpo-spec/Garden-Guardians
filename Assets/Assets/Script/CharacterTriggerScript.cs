using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class CharacterTriggerScript : MonoBehaviour
{
    private Character character;
    [SerializeField] private KeyCode interactKey;
    private bool canTrigger;
    
    private void Start()
    {
        character = gameObject.GetComponent<Character>();
        canTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(interactKey) && canTrigger)
        {
            Fungus.Flowchart.BroadcastFungusMessage(character.NameText + "Start");
        }
    }

    public void ChangeTrigger()
    {
        if (canTrigger)
        {
            canTrigger = false;
        }
        else if (!canTrigger)
        {
            canTrigger = true;
        }
        else
        {
            return;
        }
    }
}
