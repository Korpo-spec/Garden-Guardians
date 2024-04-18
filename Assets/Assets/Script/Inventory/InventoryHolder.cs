using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public InventorySO Inventory;
    public InventorySO weaponSlot;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (TryChangeWeapon())
            {
                GetComponent<EntityAttack>().weapon = weaponSlot.itemsSlots[0].Stack._item.equipment;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Inventory.ThrowActiveItem();
        }
    }


    private bool TryChangeWeapon()
    {
        
        var activeslot = Inventory.FindActiveInvSlot();
        if (!activeslot.Stack._item)
        {
            return false;
        }
        var activeindex = 0;
        for (int i = 0; i < Inventory.itemsSlots.Count; i++)
        {
            if (Inventory.itemsSlots[i].active)
            {
                activeindex = i;
                break;
            }
        }
        
        if (activeslot.Stack._item.itemType != ItemType.Equipment)
        {
            return false;
        }
        
        Inventory.itemsSlots.Swap(weaponSlot.itemsSlots,activeindex,0);
        return true;
    }
}
