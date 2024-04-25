using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTreeController : MonoBehaviour
{
   public List<WeaponButton> ButtonUpgrades;


   [SerializeField] private WeaponButtonSO _activeWeaponButtonSo;

   //public GameObject FivePath;
   public GameObject ThreePath;
   public GameObject TwoPath;

   private List<GameObject> TreePaths = new List<GameObject>();

   private void Start()
   {
      //TreePaths.Add(FivePath);
      TreePaths.Add(ThreePath);
      TreePaths.Add(TwoPath);
      WeaponButton.weaponBought += SetNewActiveWeapon;
   }

   private void OnDestroy()
   {
      WeaponButton.weaponBought -= SetNewActiveWeapon;
   }

   private void SetNewActiveWeapon(object sender,EventArgs e)
   {
      WeaponButton weaponButton = (WeaponButton) sender;
      ActiveWeaponButton = weaponButton._weaponButtonSo;
      setCorrectTreePath();
      
   }

   private void setCorrectTreePath()
   {
      foreach (var path in TreePaths)
      {
         path.SetActive(false);
      }
      if (ActiveWeaponButton.parents.Count==3)
      {
         ThreePath.SetActive(true);
      }
      if (ActiveWeaponButton.parents.Count == 2)
      {
         TwoPath.SetActive(true);
      }
      
      // }if (ActiveWeaponButton.parents.Count==5)
      // {
      //    FivePath.SetActive(true);
      // }
   }
   public WeaponButtonSO ActiveWeaponButton
   {
      get => _activeWeaponButtonSo;
      set
      {
         _activeWeaponButtonSo = value;
         UpdateWeaponTree();
      }
      
   }
   
   [SerializeField]
   private RawImage activeWeaponSprite;


   private void UpdateWeaponTree()
   {
      for (int i = 0; i < ActiveWeaponButton.parents.Count; i++)
      {
         ButtonUpgrades[i]._weaponButtonSo = ActiveWeaponButton.parents[i];
         ButtonUpgrades[i].Button.image.sprite = ActiveWeaponButton.parents[i].weaponSprite;
         ButtonUpgrades[i].CheckIfCanGet();
      }
      activeWeaponSprite.texture = ActiveWeaponButton.weaponSprite.texture;
   }
   
   
   private void OnValidate()
   {
      activeWeaponSprite.texture = ActiveWeaponButton.weaponSprite.texture;
      UpdateWeaponTree();
   }
   
   
}
