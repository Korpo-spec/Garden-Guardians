using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShortcut : MonoBehaviour
{
    [SerializeField] private GameObject prompt;
    void Start()
    {
        Collider collider = GetComponent<Collider>();
    }

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
            Destroy(gameObject);
        }
    }
}
