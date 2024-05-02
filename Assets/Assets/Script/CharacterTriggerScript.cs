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

    [SerializeField] private BoxCollider collider;

    private PlayerController player;
    
    private void Start()
    {
        character = gameObject.GetComponent<Character>();
        canTrigger = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (player.inDialogue)
        {
            player.inDialogue = false;
        }
        else if (!player.inDialogue)
        {
            player.inDialogue = true;
        }
        
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

    public void ChangeCharacterAvailability()
    {
        if (collider.enabled)
        {
            collider.enabled = false;
        }
        else if(!collider.enabled)
        {
            collider.enabled = true;
        }
    }
}
