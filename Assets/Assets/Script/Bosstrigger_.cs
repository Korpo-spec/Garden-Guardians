using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosstrigger_ : MonoBehaviour
{
    public static event EventHandler TriggerBoss;
    public GameObject Boss;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerBoss?.Invoke(this,null);
            Boss.gameObject.SetActive(true);
            AudioManagerEvents.TriggerAudio("Map1", false);
        }
    }
}
