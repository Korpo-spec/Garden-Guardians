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
    public List<InventorySlot> itemsSlots;
    
    public bool AddItem(ItemStack itemStack)
    {
        
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (!itemsSlots[i].active)
            {
                itemsSlots[i].Stack._item = itemStack._item;
                itemsSlots[i].active = true;
            }
            if (itemsSlots[i].Stack._item.name == itemStack._item.name)
            { 
                if (itemsSlots[i].Stack.isFull) return false;
                
                itemsSlots[i].Stack.numberOfItemsInStack += itemStack.numberOfItemsInStack;
                Debug.Log(itemsSlots[i].Stack.numberOfItemsInStack);
                return true;
            }
        }

        return true;
    }

    public bool TryAddItem(ItemStack itemStack)
    {
        
        //loop to check if there is an item of that type if there is add amount to the stack if not full
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].Stack.isFull||itemsSlots[i].Stack._item==null)
            {
                continue;
            }
            if (itemsSlots[i].Stack._item.name == itemStack._item.name)
            {
                itemsSlots[i].Stack.numberOfItemsInStack += itemStack.numberOfItemsInStack;
                Debug.Log(itemsSlots[i].Stack.numberOfItemsInStack);
                
                return true;
            }
            
            
        }
        
        
        //loop to check if there is an empty spot to add a new item
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            
            if (itemsSlots[i].Stack._item==null)
            {
                itemsSlots[i].Stack._item = itemStack._item;
                itemsSlots[i].Stack.numberOfItemsInStack += itemStack.numberOfItemsInStack;
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
