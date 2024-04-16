using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [Serializable]
    public class InventorySlot
    {
        public ItemStack itemStack;
        public bool active;
    }
}