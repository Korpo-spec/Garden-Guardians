using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShortcut : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
