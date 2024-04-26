using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponButton/WeaponButton")]
public class WeaponButtonSO : ScriptableObject
{
    public ItemStack weaponModul;
    
    public List<WeaponButtonSO> parents;

    public Sprite weaponSprite;
    public WeaponButton.UpgradeType upgradeType;
    
    
    [Header("Player Inventories")]
    public InventorySO PlayerWeaponSlot;
    public InventorySO PlayerInventory;
    
    [Header("Requierments")]
    public List<ItemStack> ItemRequierments;
    public WeaponButton.UpgradeCost upgradeCost;
    
}
