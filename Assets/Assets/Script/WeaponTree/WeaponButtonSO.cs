using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class WeaponButtonSO : ScriptableObject
{
    public ItemStack weaponModul;
    
    public List<WeaponButtonSO> parents;

    public Sprite weaponSprite;
    
    [Header("Player Inventories")]
    public InventorySO PlayerWeaponSlot;
    public InventorySO PlayerInventory;
    
    [Header("Requierments")]
    public List<ItemStack> ItemRequierments;
    public WeaponButton.UpgradeCost upgradeCost;
    
}
