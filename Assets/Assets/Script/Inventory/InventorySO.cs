using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private int maxInventorySize;
    [SerializeField]private List<InventorySlot> itemsSlots;
    public bool AddItem(Item item)
    {
        
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (!itemsSlots[i].active)
            {
                itemsSlots[i].itemStack._item = item;
                itemsSlots[i].active = true;
            }
            if (itemsSlots[i].itemStack._item.name == item.name)
            { 
                if (itemsSlots[i].itemStack.isFull) return false;
                
                itemsSlots[i].itemStack.numberOfItemsInStack += item.value;
                Debug.Log(itemsSlots[i].itemStack.numberOfItemsInStack);
                return true;
            }
        }

        return true;
    }

    private void AdjustSize()
    {
        itemsSlots ??= new List<InventorySlot>();
        if (itemsSlots.Count>maxInventorySize)itemsSlots.RemoveRange(maxInventorySize,itemsSlots.Count-maxInventorySize);
        if (itemsSlots.Count< maxInventorySize) itemsSlots.AddRange(new InventorySlot[maxInventorySize-itemsSlots.Count]);
    }

    private void OnValidate()
    {
        AdjustSize();
    }
}
