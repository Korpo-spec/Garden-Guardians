using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UMAmountUI : MonoBehaviour
{
    [SerializeField] private InventorySO PlayerInventory;
    [SerializeField] private TextMeshProUGUI AmountText;


    private void Start()
    {
        PlayerInventory.UniversalMaterial.UpdatedMaterialValue += UpdateUMAmount;
        AmountText.text = PlayerInventory.UniversalMaterial.UniversalMaterial.ToString();
    }

    private void OnDestroy()
    {
        PlayerInventory.UniversalMaterial.UpdatedMaterialValue -= UpdateUMAmount;
    }

    public void UpdateUMAmount(object sender, EventArgs eventArgs)
    {
        AmountText.text = PlayerInventory.UniversalMaterial.UniversalMaterial.ToString();
    }
}
