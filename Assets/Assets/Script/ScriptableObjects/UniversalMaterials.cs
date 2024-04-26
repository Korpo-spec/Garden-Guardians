using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UniversalMaterials : ScriptableObject
{

    [SerializeField] private int _universalMaterial;
    [SerializeField] private int _specialUniversalMaterial;
    
    public int UniversalMaterial
    {
        get => _universalMaterial;
        set
        {
            _universalMaterial = value;
            UpdatedMaterialValue.Invoke(this,null);
        }
    }
    public int SpecialUniversalMaterial
    {
        get => _specialUniversalMaterial;
        set
        {
            _specialUniversalMaterial = value;
            UpdatedMaterialValue.Invoke(this,null);
        }
    }

    [Header("Press if you want to zero materials")]
    [SerializeField] private bool ClearMaterials;


    public event EventHandler UpdatedMaterialValue;

    public void addUniversalMaterials(int UM, int SUM)
    {
        UniversalMaterial += UM;
        SpecialUniversalMaterial += SUM;
    }

    public void removeUniversalMaterials(int UM, int SUM)
    {
        UniversalMaterial -= UM;
        SpecialUniversalMaterial -= SUM;
    }

    public void ClearUniMaterials()
    {
        UniversalMaterial = 0;
        SpecialUniversalMaterial = 0;
    }


    private void OnValidate()
    {
        UpdatedMaterialValue?.Invoke(this,null);
        if (ClearMaterials)
        {
            ClearUniMaterials();
            ClearMaterials = false;
        }
    }
}
