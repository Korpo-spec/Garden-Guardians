using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    
    
    [Header("Requierments")]
    public List<WeaponButton> parents;

    public UpgradeCost upgradeCost;

    private void Start()
    {
        PlayerWeaponSlot.UniversalMaterial.UpdatedMaterialValue += OnUpdateMaterial;
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
        writeConnections();
    }

    private void OnDestroy()
    {
        PlayerWeaponSlot.UniversalMaterial.UpdatedMaterialValue -= OnUpdateMaterial;
    }

    private void OnUpdateMaterial(object sender,EventArgs e)
    {
        
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
            Debug.Log("Weapon got");
        }
    }

    private void BuyUpgrade()
    {
        PlayerWeaponSlot.UniversalMaterial.removeUniversalMaterials(upgradeCost._UMCost,upgradeCost._SUMCost);
    }

    private void writeConnections()
    {
        
    }


    private bool CheckIfcanGet()
    {
        if (checkIfEnoughUM())
        {
            return true;
        }

        return false;
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
