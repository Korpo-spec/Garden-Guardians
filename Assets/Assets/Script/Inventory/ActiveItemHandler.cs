using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemHandler : MonoBehaviour
{
   [SerializeField] private InventorySO _inventorySo;
   private int currentPosInInventory;

   public event EventHandler <MouseWheelUpdateArg>MouseWheelUpdate;


   private void Start()
   {
      _inventorySo.itemsSlots[0].Active = true;
   }

   private void Update()
   {
      chooseActiveItem();
   }



   private void chooseActiveItem()
   {
      if (Input.mouseScrollDelta.y==0)
      {
         return;
      }
      //Check if negative or positive scroll
      switch (Input.mouseScrollDelta.y)
      {
         case < 0:
            currentPosInInventory--;
            break;
         case > 0:
            currentPosInInventory++;
            break;
      }
      OnMouseWheelUpdate(Input.mouseScrollDelta.y);
      
      //modulo operator + start from the back of the inventory if negative
      currentPosInInventory %= _inventorySo.itemsSlots.Count;

      if (currentPosInInventory<0)
      {
         currentPosInInventory -= -_inventorySo.itemsSlots.Count;
      }
      
      //every inventory to false
      foreach (var VARIABLE in _inventorySo.itemsSlots)
      {
         VARIABLE.Active = false;
      }
      //active inventoryspot to true
      _inventorySo.itemsSlots[currentPosInInventory].Active = true;
   }


   protected virtual void OnMouseWheelUpdate(float mouseDelta)
   {
      MouseWheelUpdate?.Invoke(this, new MouseWheelUpdateArg(mouseDelta));
   }
   
   public class MouseWheelUpdateArg
   {
      public float mouseDelta { get; }
      public int activeInventoryPos { get; }
      
      public MouseWheelUpdateArg(float MouseDelta)
      {
         mouseDelta = MouseDelta;
      }
   }
}
