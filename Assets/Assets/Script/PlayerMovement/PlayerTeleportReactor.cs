using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerTeleportReactor : MonoBehaviour
{

    [SerializeField] private CharacterController playerMovement;
    [SerializeField] private EntityAttack playerAttack;
    
    void Start()
    {
        TeleportController.PlayerTeleport += OnPlayerTeleport;
    }


    private void OnPlayerTeleport(object o, Vector3 newPos)
    {

        playerMovement.enabled = false;
        playerAttack.canAttack = false;
        StartCoroutine(TPPlayer(newPos));
    }

    private IEnumerator TPPlayer(Vector3 newPos)
    {
        yield return new WaitForSeconds(1);
        gameObject.transform.position = newPos;
        yield return new WaitForSeconds(1);
        playerMovement.enabled = true;
        playerAttack.canAttack = true;
    }

    private void OnDestroy()
    {
        TeleportController.PlayerTeleport -= OnPlayerTeleport;
    }
}
