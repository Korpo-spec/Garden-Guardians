using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack
{
  public Item _item;
  
  public int numberOfItemsInStack;

  public readonly int maxStackSize=8;
  
  public bool isFull => numberOfItemsInStack >= maxStackSize;

}
