using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTreeController : MonoBehaviour
{
   public List<WeaponButton> ThreePathButtonUpgrades;
   public List<WeaponButton> TwoPathButtonUpgrades;
   public List<WeaponButton> ButtonUpgrades;

   public EventReference CraftedWeapon;


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
      ButtonUpgrades = ThreePathButtonUpgrades;
      UpdateWeaponTree();
      WeaponButton.weaponBought += SetNewActiveWeapon;
   }

   private void OnDestroy()
   {
      WeaponButton.weaponBought -= SetNewActiveWeapon;
   }

   private void SetNewActiveWeapon(object sender,EventArgs e)
   {
      var instance = RuntimeManager.CreateInstance(CraftedWeapon);
      instance.setParameterByNameWithLabel("Parameter 2", "CraftingDone");
      instance.start();
      WeaponButton weaponButton = (WeaponButton) sender;
      ActiveWeaponButton = weaponButton._weaponButtonSo;
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
         ButtonUpgrades = ThreePathButtonUpgrades;
      }
      if (ActiveWeaponButton.parents.Count == 2)
      {
         TwoPath.SetActive(true);
         ButtonUpgrades = TwoPathButtonUpgrades;
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
      setCorrectTreePath();
      
      for (int i = 0; i < ActiveWeaponButton.parents.Count; i++)
      {
         ButtonUpgrades[i]._weaponButtonSo = ActiveWeaponButton.parents[i];
         ButtonUpgrades[i].Button.image.sprite = ActiveWeaponButton.parents[i].weaponSprite;
         ButtonUpgrades[i].setWeaponTypeSprite();
         ButtonUpgrades[i].CheckIfCanGet();
      }
      activeWeaponSprite.texture = ActiveWeaponButton.weaponSprite.texture;
   }
   
   
   private void OnValidate()
   {
      activeWeaponSprite.texture = ActiveWeaponButton.weaponSprite.texture;
      if (TwoPath)
      {
         ButtonUpgrades = TwoPathButtonUpgrades;
      }if (ThreePath)
      {
         ButtonUpgrades = ThreePathButtonUpgrades;
      }
      UpdateWeaponTree();
   }
   
   
}
