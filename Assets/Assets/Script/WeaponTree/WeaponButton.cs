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
    private bool canGet;
    public Button Button;
    [SerializeField] private Image WeaponTypeSprite;
    

    
    public static event EventHandler weaponBought;


    public WeaponButtonTypeSO WeaponButtonTypeSo;
    public WeaponButtonSO _weaponButtonSo;

    private void Start()
    {
        Button = GetComponentInChildren<Button>();
        _weaponButtonSo.PlayerWeaponSlot.UniversalMaterial.UpdatedMaterialValue += OnUpdateMaterial;
        InventoryHolder.UpdateWeaponButton += OnUpdateMaterial;
        GameItem.ItemPickUp += OnUpdateMaterial;
        foreach (var slot in _weaponButtonSo.PlayerInventory.itemsSlots)
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
        _weaponButtonSo.PlayerWeaponSlot.UniversalMaterial.UpdatedMaterialValue -= OnUpdateMaterial;
        InventoryHolder.UpdateWeaponButton -= OnUpdateMaterial;
        GameItem.ItemPickUp -= OnUpdateMaterial;
        foreach (var slot in _weaponButtonSo.PlayerInventory.itemsSlots)
        {
            slot.ValueChangedinStack -= OnUpdateMaterialStack;
            slot.Stack.NumberOfItemsChanged-= OnUpdateMaterial;
        }
    }

    private void OnEnable()
    {
        CheckIfCanGet();
        setWeaponTypeSprite();
    }

    public void CheckIfCanGet()
    {
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
    }

    private void OnUpdateMaterial(object sender,EventArgs e)
    {
        
        CheckIfCanGet();
    }
    private void OnUpdateMaterialStack(object sender,InventorySlot.InventoryStackChangeArgs e)
    {
        Debug.Log("Changed");
        CheckIfCanGet();
    }

    public void GetWeapon()
    {
        if (canGet)
        {
            BuyUpgrade();
            _weaponButtonSo.PlayerWeaponSlot.itemsSlots[0].Stack = _weaponButtonSo.weaponModul;
            weaponBought?.Invoke(this,null);
        }
    }

    private void BuyUpgrade()
    {
        removeFromUniversalMaterial();
        foreach (var reqItem in _weaponButtonSo.ItemRequierments)
        {
            _weaponButtonSo.PlayerInventory.RemoveReqItemFromInventory(reqItem);
        }
        
    }

    private void removeFromUniversalMaterial()
    {
        _weaponButtonSo.PlayerWeaponSlot.UniversalMaterial.removeUniversalMaterials(_weaponButtonSo.upgradeCost._UMCost,_weaponButtonSo.upgradeCost._SUMCost);
        
    }
    
    private void writeConnections()
    {
        
    }

    public void setWeaponTypeSprite()
    {
        switch (_weaponButtonSo.upgradeType)
        {
            case UpgradeType.Physical:
                WeaponTypeSprite.sprite = WeaponButtonTypeSo.PhysicalSprite;
                break;
            case UpgradeType.Elemental:
                WeaponTypeSprite.sprite = WeaponButtonTypeSo.ElementalSprite;
                break;
            case UpgradeType.Basic:
                WeaponTypeSprite.sprite = WeaponButtonTypeSo.BasicSprite;
                break;
        }
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
        foreach (var reqItem in _weaponButtonSo.ItemRequierments)
        {
            if (!_weaponButtonSo.PlayerInventory.HasItem(reqItem))
            {
                return false;
            }

            if (reqItem._numberofItemsInStack>_weaponButtonSo.PlayerInventory.TotalNumberOfItemsInInventory(reqItem._item))
            {
                return false;
            }
        }
        
        return true;
    }

    private bool checkIfEnoughUM()
    {
        if (_weaponButtonSo.PlayerWeaponSlot.UniversalMaterial.UniversalMaterial>=_weaponButtonSo.upgradeCost._UMCost&&
            _weaponButtonSo.PlayerWeaponSlot.UniversalMaterial.SpecialUniversalMaterial>=_weaponButtonSo.upgradeCost._SUMCost)
        {
            return true;
        }

        return false;
    }

    private void HandleIfCanGet(bool canGet)
    {
        Button.interactable = canGet;
    }

    private void OnValidate()
    {
        Button.GetComponent<Image>().sprite = _weaponButtonSo.weaponSprite;
        setWeaponTypeSprite();
    }

    [Serializable]
    public struct UpgradeCost
    {
        [SerializeField] private int UMCost;
        [SerializeField] private int SUMCost;

        public int _UMCost => UMCost;
        public int _SUMCost => SUMCost;

    }
    
    public enum UpgradeType
    {
        Physical,Elemental,Basic
    }
}
