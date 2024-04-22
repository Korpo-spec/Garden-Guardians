using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHandler : MonoBehaviour
{
    [Header("WeaponTree")]
    public UnityEvent <bool>OpenWeaponTree;
    [SerializeField]
    private KeyCode OpenWeaponTreeKey;

    [SerializeField]
    private GameObject WeaponTreeUi;
    


    private void Update()
    {
        if (Input.GetKeyDown(OpenWeaponTreeKey))
        {
            OpenWeaponTree.Invoke(!WeaponTreeUi.gameObject.activeSelf);
        }
    }

    public void SetWeaponTreeUiActive(bool activestate)
    {
        WeaponTreeUi.SetActive(activestate);
    }
}
