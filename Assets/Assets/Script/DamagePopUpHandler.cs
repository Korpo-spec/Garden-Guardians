using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Script.Entity;
using UnityEngine;

public class DamagePopUpHandler : MonoBehaviour
{
   [SerializeField] private GameObject spawnText;
 
   


   
  

   public void SpawnText(int Damage)
   {
      SpawnPopUpText(Damage);
   }
   
   private void SpawnPopUpText(int DamageTaken)
   {
      Instantiate(spawnText,transform.position,Quaternion.identity);

      foreach (var VARIABLE in  spawnText.GetComponentsInChildren<TextMesh>())
      {
         VARIABLE.text = DamageTaken.ToString();
      }

     
   }
}
