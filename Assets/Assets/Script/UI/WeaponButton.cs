using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Script;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class WeaponButton : MonoBehaviour
{
    public ItemStack weaponModul;

    private bool canGet;
    public static event EventHandler weaponBought;
    public Button Button;
    public InventorySO PlayerWeaponSlot;
    public InventorySO PlayerInventory;
    public List<WeaponButton> parents;

    [Header("Requierments")]
    public List<ItemStack> ItemRequierments;
    public UpgradeCost upgradeCost;

    private void Start()
    {
        PlayerWeaponSlot.UniversalMaterial.UpdatedMaterialValue += OnUpdateMaterial;
        InventoryHolder.UpdateWeaponButton += OnUpdateMaterial;
        GameItem.ItemPickUp += OnUpdateMaterial;
        foreach (var slot in PlayerInventory.itemsSlots)
        {
            slot.ValueChangedinStack += OnUpdateMaterialStack;
            slot.Stack.NumberOfItemsChanged+= OnUpdateMaterial;
        }
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
        writeConnections();
    }

    private void OnDestroy()
    {
        PlayerWeaponSlot.UniversalMaterial.UpdatedMaterialValue -= OnUpdateMaterial;
        InventoryHolder.UpdateWeaponButton -= OnUpdateMaterial;
        GameItem.ItemPickUp -= OnUpdateMaterial;
        foreach (var slot in PlayerInventory.itemsSlots)
        {
            slot.ValueChangedinStack -= OnUpdateMaterialStack;
            slot.Stack.NumberOfItemsChanged-= OnUpdateMaterial;
        }
    }

    private void OnEnable()
    {
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
    }

    private void OnUpdateMaterial(object sender,EventArgs e)
    {
        
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
    }
    private void OnUpdateMaterialStack(object sender,InventorySlot.InventoryStackChangeArgs e)
    {
        Debug.Log("Changed");
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
    }

    public void GetWeapon()
    {
        if (canGet)
        {
            BuyUpgrade();
            PlayerWeaponSlot.itemsSlots[0].Stack = weaponModul;
            weaponBought?.Invoke(this,null);
        }
    }

    private void BuyUpgrade()
    {
        removeFromUniversalMaterial();
        foreach (var reqItem in ItemRequierments)
        {
            PlayerInventory.RemoveReqItemFromInventory(reqItem);
        }
        
    }

    private void removeFromUniversalMaterial()
    {
        PlayerWeaponSlot.UniversalMaterial.removeUniversalMaterials(upgradeCost._UMCost,upgradeCost._SUMCost);
        
    }
    
    private void writeConnections()
    {
        
    }


    private bool CheckIfcanGet()
    {
        if (!checkIfEnoughUM())
        {
            return false;
        }

        if (!checkIfHaveRequieredItems())
        {
            Debug.Log("Not all items");
            return false;
        }

        return true;
    }

    private bool checkIfHaveRequieredItems()
    {
        foreach (var reqItem in ItemRequierments)
        {
            if (!PlayerInventory.HasItem(reqItem))
            {
                return false;
            }

            if (reqItem._numberofItemsInStack>PlayerInventory.TotalNumberOfItemsInInventory(reqItem._item))
            {
                return false;
            }
        }
        
        return true;
    }

    private bool checkIfEnoughUM()
    {
        if (PlayerWeaponSlot.UniversalMaterial.UniversalMaterial>=upgradeCost._UMCost&&
            PlayerWeaponSlot.UniversalMaterial.SpecialUniversalMaterial>=upgradeCost._SUMCost)
        {
            return true;
        }

        return false;
    }

    private void HandleIfCanGet(bool canGet)
    {
        Button.interactable = canGet;
    }

    [Serializable]
    public struct UpgradeCost
    {
        [SerializeField] private int UMCost;
        [SerializeField] private int SUMCost;

        public int _UMCost => UMCost;
        public int _SUMCost => SUMCost;

    }
}
