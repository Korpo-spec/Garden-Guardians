using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Script.Entity;
using UnityEngine;

public class DamagePopUpHandler : MonoBehaviour
{
   [SerializeField] private GameObject spawnText;
   private const int maxScaleDamage = 8;
   


   public void SpawnText(int Damage)
   {
      SpawnPopUpText(Damage);
   }
   
   private void SpawnPopUpText(int DamageTaken)
   {
      var scaleFromDamage = (float)DamageTaken / maxScaleDamage;
      if (scaleFromDamage>0.5f) { scaleFromDamage = 0.5f; }
      
      
      
      var text=Instantiate(spawnText,transform.position,Quaternion.identity);
     

      foreach (var VARIABLE in  text.GetComponentsInChildren<TextMesh>())
      {
         VARIABLE.text = DamageTaken.ToString();
         
         var newScale = VARIABLE.transform.localScale;
         newScale = new Vector3(newScale.x, newScale.y, newScale.z);
      
         VARIABLE.transform.localScale = newScale*(1+scaleFromDamage);
      }

     
   }
}
