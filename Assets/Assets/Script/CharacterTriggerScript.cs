using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class CharacterTriggerScript : MonoBehaviour
{
    private Character character;
    private PlayerController player;
    
    private bool canTrigger;

    [SerializeField] private BoxCollider collider;
    [SerializeField] private GameObject interactText;
    
    [SerializeField] private KeyCode interactKey;

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

    private void OnTriggerEnter(Collider other)
    {
        interactText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.SetActive(false);
    }

    public void ChangeTrigger()
    {
        interactText.SetActive(false);
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
    }
}
