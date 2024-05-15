using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Entity;
using UnityEngine;

public class ColdZoneBehaviour : MonoBehaviour
{

   [SerializeField] private SlowEffect _slowEffect;
   
   [SerializeField]
   private TransformHealthDictionary _healthDictionary;

   [HideInInspector]
   public float destroyTime;


   private void Start()
   {
      StartCoroutine(DestroyObject(destroyTime));
   }

   private void OnTriggerEnter(Collider other)
   {
      if (_healthDictionary.Contains(other.transform)&&!other.gameObject.CompareTag("Player"))
      {
         other.GetComponent<EffectManager>().AddEffect(_slowEffect);
      }
   }

   private void OnTriggerStay(Collider other)
   {
      throw new NotImplementedException();
   }

   private IEnumerator DestroyObject(float time)
   {
      yield return new WaitForSeconds(time);
      Destroy(gameObject);
        
   }
   
}
