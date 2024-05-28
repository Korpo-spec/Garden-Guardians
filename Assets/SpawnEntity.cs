using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntity : MonoBehaviour
{
    
    [SerializeField]private GameObject spawn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
        }
    }
    
}
