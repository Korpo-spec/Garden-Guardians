using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityDrop : MonoBehaviour
{
   public List<ItemDrop> DropList;


   public void Drop()
   {
      foreach (var item in DropList)
      {
         var random = Random.Range(0, 100);
         if (random<=item.dropChance)
         {
            Vector3 randomVec = new Vector3().RandomVector3(-2, 2, 0, 0, -2, 2);
            Instantiate(item.GameObject, transform.position+randomVec, quaternion.identity);
         }
      }
   }


}

[Serializable]
public struct ItemDrop
{
   public GameObject GameObject;
   [Range(0,100)]
   public int dropChance;
}
