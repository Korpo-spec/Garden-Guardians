using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class CharacterTriggerScript : MonoBehaviour
{
    private Character character;
    [SerializeField] private KeyCode interactKey;
    
    private void Start()
    {
        character = gameObject.GetComponent<Character>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(interactKey))
        {
            Fungus.Flowchart.BroadcastFungusMessage(character.NameText + "Start");
        }
    }
}
