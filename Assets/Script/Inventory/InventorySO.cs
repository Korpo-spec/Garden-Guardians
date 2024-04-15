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
    private List<InventorySlot> itemsSlots=new List<InventorySlot>();
    public bool AddItem(Item item)
    {
        
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (!itemsSlots[i].active)
            {
                itemsSlots[i].Item = item;
                itemsSlots[i].active = true;
            }
            if (itemsSlots[i].Item.name == item.name)
            {
                if (itemsSlots[i].Item.isFull) return false;
                
                itemsSlots[i].Item.amountInStack += item.value;
                Debug.Log(item.amountInStack);
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
