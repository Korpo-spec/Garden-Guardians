using System;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHolder : MonoBehaviour
{
    public InventorySO Inventory;
    public InventorySO weaponSlot;

    public static event EventHandler UpdateWeaponButton;
    
    private void Start()
    {
        WeaponButton.weaponBought += OnactivateWeapon;
    }

    private void OnDestroy()
    {
        WeaponButton.weaponBought -= OnactivateWeapon;
    }
    

    public void ThrowActiveItemInInventory()
    {
        var activeItemslot = Inventory.FindActiveInvSlot();
        if (activeItemslot.Slot.Stack._item && TryGetComponent<GameItemSpawn>(out var itemSpawner))
        {
            Inventory.RemoveItem(activeItemslot.indexOfItem, itemSpawner, true);
        }
    }




    private void OnactivateWeapon(object sender,EventArgs eventArgs)
    {
        GetComponent<EntityAttack>().weapon = weaponSlot.itemsSlots[0].Stack._item.equipment;
    }
    

    private bool TryChangeWeapon()
    {
        var activeslot = Inventory.FindActiveInvSlot();
        var slot = activeslot.Slot;
        var index = activeslot.indexOfItem;


        if (!slot.Stack._item)
        {
            Inventory.itemsSlots.Swap(weaponSlot.itemsSlots, index, 0);
            GetComponent<EntityAttack>().weapon = weaponSlot.itemsSlots[1].Stack._item.equipment;
            return false;
        }

        if (slot.Stack._item.itemType != ItemType.Equipment)
        {
            return false;
        }

        Inventory.itemsSlots.Swap(weaponSlot.itemsSlots, index, 0);
        return true;
    }
}