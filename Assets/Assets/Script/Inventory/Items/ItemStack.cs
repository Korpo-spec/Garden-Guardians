using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack
{
  public Item _item;
  
  public int numberOfItemsInStack;

  [HideInInspector]
  public int maxStackSize=8;
  
  public bool isFull => numberOfItemsInStack >= maxStackSize;
  
  public bool isEmpty=>numberOfItemsInStack<=0;
  
  

  // public ItemStack(int numberOfItemsInStack, Item item)
  // {
  //   this.numberOfItemsInStack = numberOfItemsInStack;
  //   _item = item;
  // }

}
