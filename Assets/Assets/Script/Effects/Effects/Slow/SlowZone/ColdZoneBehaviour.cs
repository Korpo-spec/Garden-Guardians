using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Entity;
using UnityEngine;
using UnityEngine.Serialization;

public class ColdZoneBehaviour : MonoBehaviour
{

   [SerializeField] private Effect _effect;
   [SerializeField] private bool ShouldBedestroyed;
   [SerializeField] private bool isBlightGround;
   
   [SerializeField]
   private TransformHealthDictionary _healthDictionary;

   [HideInInspector]
   public float destroyTime;


   private void Start()
   {
      if (ShouldBedestroyed)
      {
         StartCoroutine(DestroyObject(destroyTime));
      }
      
   }

   private void OnTriggerEnter(Collider other)
   {
      if (_healthDictionary.Contains(other.transform)&&!other.gameObject.CompareTag("Player")&&!isBlightGround)
      {
         other.GetComponent<EffectManager>().AddEffect(_effect);
      } 
      if (_healthDictionary.Contains(other.transform)&&other.gameObject.CompareTag("Player")&&isBlightGround)
      {
         other.GetComponent<EffectManager>().AddEffect(_effect);
      }
      
   }
   


   private IEnumerator DestroyObject(float time)
   {
      yield return new WaitForSeconds(time);
      Destroy(gameObject);
        
   }
   
}
