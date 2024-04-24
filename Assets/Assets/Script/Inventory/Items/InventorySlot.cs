using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [Serializable]
    public class InventorySlot
    {
        [SerializeField]
        private ItemStack itemStack;
        [SerializeField]
        private bool active;

        public event EventHandler<InventoryStackChangeArgs> ValueChangedinStack; 


        public void ClearSlot()
        {
            Stack = null;
        }

        public ItemStack Stack
        {
            get => itemStack;
            set
            {
                itemStack = value;
                OnValueChangedinStack();
            }
        }

        public bool Active
        {
            get => active;
            set
            {
                active = value;
                OnValueChangedinStack();
            }
        }
        

        public int NumberOfItems
        {
            get => itemStack._numberofItemsInStack;
            set
            {
                itemStack._numberofItemsInStack = value;
                OnValueChangedinStack();
            }
        }

        protected virtual void OnValueChangedinStack()
        {
            ValueChangedinStack?.Invoke(this, new InventoryStackChangeArgs(itemStack,active));
        }
        
        public class InventoryStackChangeArgs
        {
            public ItemStack newStack { get; }
            public bool Active { get; }

            public InventoryStackChangeArgs(ItemStack itemStack, bool active)
            {
                newStack = itemStack;
                Active = active;
            }
        }
    }
    
    
}