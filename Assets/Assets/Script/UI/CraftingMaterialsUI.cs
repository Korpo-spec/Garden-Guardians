using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CraftingMaterialsUI : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private UniversalMaterials universalMaterials;
    private List<Image> _slotIcons;

    private void Awake()
    {
        _slotIcons = new List<Image>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _slotIcons.Add(transform.GetChild(i).GetChild(0).GetComponent<Image>());
        }
    }

    private void OnEnable()
    {
        int index = 0;
        foreach (var itemsSlot in inventory.itemsSlots)
        {
            if (itemsSlot.Stack._item == null)
            {
                continue;
            }
            _slotIcons[index].sprite = itemsSlot.Stack._item.icon;
            index++;
        }
    }
}
