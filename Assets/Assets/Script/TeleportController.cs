using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public static event EventHandler <Vector3>PlayerTeleport;

    public Transform TeleportPosition;

    [SerializeField] private ResetNpcs resetNpcs;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTeleport?.Invoke(this,TeleportPosition.position);
            if (resetNpcs != null)
            {
                resetNpcs.ResetNpc();
            }
        }
    }
}
