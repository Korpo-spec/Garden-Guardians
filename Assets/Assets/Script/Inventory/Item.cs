
using System;using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name;
    //public int value;
    public Sprite icon;

    public bool isStackable;

    public ItemType itemType;
    public EquipmentType equipmentType;
    public AttackComboSO equipment;
}

public enum ItemType
{
    Item,
    Equipment,
    Biomass
}

public enum EquipmentType
{
    MainHand,
    Offhand,
    ChestPlate,
    Helmet
}
