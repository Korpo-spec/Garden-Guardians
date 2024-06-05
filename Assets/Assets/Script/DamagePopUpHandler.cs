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
   


   public void SpawnText(DamageEventArg damageEventArg)
   {
      SpawnPopUpText(damageEventArg);
   }
   
   private void SpawnPopUpText(DamageEventArg damageEventArg)
   {
      var scaleFromDamage = (float)damageEventArg.damage / maxScaleDamage;
      if (scaleFromDamage>0.5f) { scaleFromDamage = 0.5f; }
      
      
      
      var text=Instantiate(spawnText,transform.position,Quaternion.identity);
     

      foreach (var VARIABLE in  text.GetComponentsInChildren<TextMesh>())
      {
         VARIABLE.text = damageEventArg.damage.ToString();
         
         var newScale = VARIABLE.transform.localScale;
         if (damageEventArg.isCrit)
         {
            newScale = new Vector3(newScale.x, newScale.y, newScale.z)*2;
            VARIABLE.text += "!";
         }
         else
         {
            newScale = new Vector3(newScale.x, newScale.y, newScale.z);
         }
         
      
         VARIABLE.transform.localScale = newScale*(1+scaleFromDamage);
      }

     
   }

   public void SetHealthBarActive()
   {
      
   }
}
