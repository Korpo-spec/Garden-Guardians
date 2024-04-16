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

    public bool TryAddItem(Item item)
    {
        
        //loop to check if there is an item of that type if there is add amount to the stack if not full
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].itemStack.isFull||itemsSlots[i].itemStack._item==null)
            {
                continue;
            }
            if (itemsSlots[i].itemStack._item.name == item.name)
            {
                itemsSlots[i].itemStack.numberOfItemsInStack += item.value;
                Debug.Log(itemsSlots[i].itemStack.numberOfItemsInStack);
                
                return true;
            }
            
            
        }
        
        
        //loop to check if there is an empty spot to add a new item
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            
            if (itemsSlots[i].itemStack._item==null)
            {
                itemsSlots[i].itemStack._item = item;
                itemsSlots[i].itemStack.numberOfItemsInStack += item.value;
                return true;
            }
        }

        return false;
    }
    

    public void ChangeItemPlaceInInventory(int activeindex,int newIndex)
    {
        itemsSlots.Swap(activeindex,newIndex);
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
