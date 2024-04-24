using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private int maxInventorySize;
    public List<InventorySlot> itemsSlots;

    public UniversalMaterials UniversalMaterial;

    public bool TryAddItem(ItemStack itemStack)
    {
        if (itemStack._item.itemType==ItemType.Biomass)
        {
            UniversalMaterial.addUniversalMaterials(itemStack._numberofItemsInStack,2);
            return true;
        }
        
        //loop to check if there is an item of that type if there is add amount to the stack if not full
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].Stack.isFull||itemsSlots[i].Stack._item==null||!itemsSlots[i].Stack._item.isStackable)
            {
                continue;
            }
            if (itemsSlots[i].Stack._item.name == itemStack._item.name)
            {
                
                return increaseAmountInstack(i,itemStack._numberofItemsInStack,itemStack);
                
            }
            
            
        }
        
        //loop to check if there is an empty spot to add a new item
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            
            if (itemsSlots[i].Stack._item==null)
            {
                itemsSlots[i].Stack._item = itemStack._item;
                
                
                return increaseAmountInstack(i,itemStack._numberofItemsInStack,itemStack);
            }
        }

        return false;
    }

    private bool increaseAmountInstack(int index,int amount,ItemStack itemStack)
    {
        
        itemsSlots[index].Stack._numberofItemsInStack += amount;

        if (itemsSlots[index].Stack._numberofItemsInStack>itemsSlots[index].Stack.maxStackSize)
        {
            
            var rest = itemsSlots[index].Stack._numberofItemsInStack - itemsSlots[index].Stack.maxStackSize;
            
            itemsSlots[index].Stack._numberofItemsInStack = itemsSlots[index].Stack.maxStackSize;
            
            for (int i = 0; i < itemsSlots.Count; i++)
            {
                if (!itemsSlots[i].Stack._item) { continue; }
                
                if(itemsSlots[i].Stack._item.name == itemStack._item.name&&!itemsSlots[i].Stack.isFull)
                {
                    itemsSlots[i].Stack._numberofItemsInStack += rest;
                    if (itemsSlots[i].Stack._numberofItemsInStack<=itemStack.maxStackSize)
                    {
                        return true;
                    }
                    rest = itemsSlots[i].Stack._numberofItemsInStack - itemsSlots[i].Stack.maxStackSize;
                    
                    itemsSlots[i].Stack._numberofItemsInStack = itemsSlots[i].Stack.maxStackSize;

                }
            }

            for (int i = 0; i < itemsSlots.Count; i++)
            {

                if (!itemsSlots[i].Stack._item)
                {
                    itemsSlots[i].Stack._item = itemStack._item;
                    itemsSlots[i].Stack._numberofItemsInStack += rest;
                    if (itemsSlots[i].Stack._numberofItemsInStack<=itemStack.maxStackSize)
                    {
                        return true;
                    }
                    rest = itemsSlots[i].Stack._numberofItemsInStack - itemsSlots[i].Stack.maxStackSize;
                    itemsSlots[i].Stack._numberofItemsInStack = itemsSlots[i].Stack.maxStackSize;
                }
                
            }

            itemStack._numberofItemsInStack = rest;
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
        if (itemSlot == null) return false;
        if (itemSlot.Stack._item==null)  return false;
        if (!CheckNumberOfItems) return true;

        if (itemStack._item.isStackable)
        {
            return itemSlot.NumberOfItems >= itemStack._numberofItemsInStack;
        }

        return itemsSlots.Count(slot => slot.Stack._item == itemStack._item) >= itemStack._numberofItemsInStack;
    }

    private InventorySlot FindSlot(Item item, bool onlyStackable=false)
    {
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].Stack==null)
            {
                continue;
            }
            if (itemsSlots[i].Stack._item==item)
            {
                return itemsSlots[i];
            }
        }

        return null;
        //return itemsSlots.FirstOrDefault(slot => slot.Stack._item == item && item.isStackable || !onlyStackable);
    }

    public ActiveItemInfo FindActiveInvSlot()
    {
        var slot = itemsSlots.Find(slot => slot.Active);
        var activeindex = 0;
        for (int i = 0; i < itemsSlots.Count; i++)
        {
            if (itemsSlots[i].Active)
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

        if (itemSlot.Stack._item.isStackable&&itemSlot.NumberOfItems<itemStack._numberofItemsInStack)
        {
            Debug.LogWarning("Not enough Items");
        }

        itemSlot.NumberOfItems -= itemStack._numberofItemsInStack;
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
