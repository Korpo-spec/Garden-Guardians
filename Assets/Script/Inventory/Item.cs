﻿
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string name;
    public int amount;
    public readonly int stackSize;
    public bool isFull => amount >= stackSize;

    public Sprite icon;

}
