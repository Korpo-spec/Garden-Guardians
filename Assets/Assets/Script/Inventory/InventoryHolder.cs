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
            ThrowActiveItemInInventory();
        }
    }

    public void ThrowActiveItemInInventory()
    {
        var activeItemslot = Inventory.FindActiveInvSlot();
        if (activeItemslot.Slot.Stack._item&&TryGetComponent<GameItemSpawn>(out var itemSpawner))
        {
            Inventory.RemoveItem(activeItemslot.indexOfItem,itemSpawner, true);
            
        }
        
    }
    
    
    private bool TryChangeWeapon()
    {
        
        var activeslot = Inventory.FindActiveInvSlot();
        var slot = activeslot.Slot;
        var index = activeslot.indexOfItem;
        
        
        if (!slot.Stack._item)
        {
            Inventory.itemsSlots.Swap(weaponSlot.itemsSlots,index,0);
            GetComponent<EntityAttack>().weapon = weaponSlot.itemsSlots[1].Stack._item.equipment;
            return false;
        }
        
        if (slot.Stack._item.itemType != ItemType.Equipment)
        {
            return false;
        }
        
        Inventory.itemsSlots.Swap(weaponSlot.itemsSlots,index,0);
        return true;
    }
    
    
}
