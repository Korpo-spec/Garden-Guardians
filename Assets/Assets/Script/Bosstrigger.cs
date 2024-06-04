using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosstrigger : MonoBehaviour
{
    
    public GameObject triggerbox;
    


    private void Start()
    {
        triggerbox.gameObject.SetActive(true);
        Bosstrigger_.TriggerBoss += DisaebleTrigger;
    }
    

    private void DisaebleTrigger(object o, EventArgs e)
    {
        triggerbox.SetActive(false);
    }

    private void OnDisable()
    {
        Bosstrigger_.TriggerBoss -= DisaebleTrigger;
    }
}

