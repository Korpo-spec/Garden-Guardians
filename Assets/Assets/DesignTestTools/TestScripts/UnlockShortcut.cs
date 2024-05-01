using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShortcut : MonoBehaviour
{
    void Start()
    {
        Collider collider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
