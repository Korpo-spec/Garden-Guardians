using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public GameObject ToSpawn;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==7)
        {
            var ground=Instantiate(ToSpawn, other.ClosestPoint(transform.position)+new Vector3(0,0.2f,0), quaternion.identity);
            ground.GetComponentInChildren<BlightShader>().SpawnBlightAmount();
            Destroy(gameObject);
        }
    }
}
