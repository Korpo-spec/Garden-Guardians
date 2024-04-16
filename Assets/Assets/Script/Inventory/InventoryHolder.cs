using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public InventorySO Inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Inventory.ChangeItemPlaceInInventory(3);
        }
    }
}
