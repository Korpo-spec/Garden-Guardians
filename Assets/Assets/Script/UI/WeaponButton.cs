using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class WeaponButton : MonoBehaviour
{
    public AttackComboSO weaponModul;

    private bool canGet;

    public UnityEvent<AttackComboSO> weaponBought;

    public Button Button;
    

    private void Start()
    {
        canGet=CheckIfcanGet();
        HandleIfCanGet(canGet);
    }
    
    public void GetWeapon()
    {
        if (canGet)
        {
            weaponBought.Invoke(weaponModul);
        }
    }


    private bool CheckIfcanGet()
    {
        return true;
    }

    private void HandleIfCanGet(bool canGet)
    {
        if (!canGet)
        {
            
        }
    }
}
