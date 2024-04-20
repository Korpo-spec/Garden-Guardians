using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using Script;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private int maxInventorySize;
    public List<InventorySlot> itemsSlots;

    public bool TryAddItem(ItemStack itemStack)
    {
        
        //loop to check if there is an item of that type if there is add amount to the stack if not full
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].Stack.isFull||itemsSlots[i].Stack._item==null||!itemsSlots[i].Stack._item.isStackable)
            {
                continue;
            }
            if (itemsSlots[i].Stack._item.name == itemStack._item.name)
            {
                
                return increaseAmountInstack(i,itemStack.numberOfItemsInStack,itemStack);
                
            }
            
            
        }
        
        //loop to check if there is an empty spot to add a new item
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            
            if (itemsSlots[i].Stack._item==null)
            {
                itemsSlots[i].Stack._item = itemStack._item;
                
                
                return increaseAmountInstack(i,itemStack.numberOfItemsInStack,itemStack);
            }
        }

        return false;
    }

    private bool increaseAmountInstack(int index,int amount,ItemStack itemStack)
    {
        
        itemsSlots[index].Stack.numberOfItemsInStack += amount;

        if (itemsSlots[index].Stack.numberOfItemsInStack>itemsSlots[index].Stack.maxStackSize)
        {
            
            var rest = itemsSlots[index].Stack.numberOfItemsInStack - itemsSlots[index].Stack.maxStackSize;
            
            itemsSlots[index].Stack.numberOfItemsInStack = itemsSlots[index].Stack.maxStackSize;
            
            for (int i = 0; i < itemsSlots.Count; i++)
            {
                if (!itemsSlots[i].Stack._item) { continue; }
                
                if(itemsSlots[i].Stack._item.name == itemStack._item.name&&!itemsSlots[i].Stack.isFull)
                {
                    itemsSlots[i].Stack.numberOfItemsInStack += rest;
                    if (itemsSlots[i].Stack.numberOfItemsInStack<=itemStack.maxStackSize)
                    {
                        return true;
                    }
                    rest = itemsSlots[i].Stack.numberOfItemsInStack - itemsSlots[i].Stack.maxStackSize;
                    
                    itemsSlots[i].Stack.numberOfItemsInStack = itemsSlots[i].Stack.maxStackSize;

                }
            }

            for (int i = 0; i < itemsSlots.Count; i++)
            {

                if (!itemsSlots[i].Stack._item)
                {
                    itemsSlots[i].Stack._item = itemStack._item;
                    itemsSlots[i].Stack.numberOfItemsInStack += rest;
                    if (itemsSlots[i].Stack.numberOfItemsInStack<=itemStack.maxStackSize)
                    {
                        return true;
                    }
                    rest = itemsSlots[i].Stack.numberOfItemsInStack - itemsSlots[i].Stack.maxStackSize;
                    itemsSlots[i].Stack.numberOfItemsInStack = itemsSlots[i].Stack.maxStackSize;
                }
                
            }

            itemStack.numberOfItemsInStack = rest;
            return false;
        }
        
        return true;
    }
    

    public void ChangeItemPlaceInInventory(int activeindex,int newIndex)
    {
        itemsSlots.Swap(activeindex,newIndex);
    }
    

    public bool HasItem(ItemStack itemStack, bool CheckNumberOfItems=false)
    {
        var itemSlot = FindSlot(itemStack._item);
        if (itemSlot==null)  return false;
        if (!CheckNumberOfItems) return true;

        if (itemStack._item.isStackable)
        {
            return itemSlot.NumberOfItems >= itemStack.numberOfItemsInStack;
        }

        return itemsSlots.Count(slot => slot.Stack._item == itemStack._item) >= itemStack.numberOfItemsInStack;
    }

    private InventorySlot FindSlot(Item item, bool onlyStackable=false)
    {
        return itemsSlots.FirstOrDefault(slot => slot.Stack._item == item && item.isStackable || !onlyStackable);
    }

    public ActiveItemInfo FindActiveInvSlot()
    {
        var slot = itemsSlots.Find(slot => slot.active);
        var activeindex = 0;
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].active)
            {
                activeindex = i;
                break;
            }
        }
        return new ActiveItemInfo(activeindex,slot);
    }
    
    public struct ActiveItemInfo
    {
        public int indexOfItem;
        public InventorySlot Slot;

        public ActiveItemInfo(int index,InventorySlot slot)
        {
            indexOfItem = index;
            Slot = slot;
        }
    }

    public ItemStack RemoveItem(int Index,GameItemSpawn itemSpawner, bool spwawn = false)
    {
        
        if (spwawn)
        {
            itemSpawner.SpawnItem(itemsSlots[Index].Stack);
        }
        
        clearSlot(Index);

        return new ItemStack();
    }

    public ItemStack RemoveItem(ItemStack itemStack)
    {
        var itemSlot = FindSlot(itemStack._item);
        if (itemSlot==null)
        {
            throw new Exception("No Item in the Inventory");
        }

        if (itemSlot.Stack._item.isStackable&&itemSlot.NumberOfItems<itemStack.numberOfItemsInStack)
        {
            Debug.LogWarning("Not enough Items");
        }

        itemSlot.NumberOfItems -= itemStack.numberOfItemsInStack;
        if (itemSlot.Stack._item.isStackable&&itemSlot.NumberOfItems>0)
        {
            return itemSlot.Stack;
        }
        
        itemSlot.ClearSlot();
        return new ItemStack();

    }

    public void clearSlot(int index)
    {
        itemsSlots[index].ClearSlot();
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
