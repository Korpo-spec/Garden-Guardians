using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Effects/chainEffect")]
public class LightChainEffect : Effect
{
   [SerializeField]
   private float EffectRadius;
   private float originalSpeed;
   private EntityMovement _entityMovement;
   private EntityHealth _entityHealth;

   private List<Transform> neighbours=new List<Transform>();

   [SerializeField] private float Damage;
   [SerializeField] private GameObject ChainSource;
    private LineRenderer _lineRenderer;
   
   


   
   public override void Enter(EffectManager obj)
   {
      base.Enter(obj);
      _entityMovement = obj.GetComponent<EntityMovement>();
      findNeighbours();
      
      foreach (var neighbour in neighbours)
      {
         neighbour.GetComponent<EntityHealth>().DamageUnit(Damage);
      }

      Instantiate(ChainSource);
      _lineRenderer = ChainSource.GetComponent<LineRenderer>();
      _lineRenderer.positionCount = neighbours.Count+1;
      _lineRenderer.SetPosition(0,_entityMovement.transform.position);
      for (int i = 0; i < neighbours.Count; i++)
      {
         _lineRenderer.SetPosition(i+1,neighbours[i].position);
      }
     
   }

   private void findNeighbours()
   {
      foreach (var collider in Physics.OverlapSphere(_entityMovement.transform.position, EffectRadius))
      {
         if (collider.TryGetComponent<EntityFaction>(out EntityFaction Neighbours)&&Neighbours.faction == Faction.Enemy)
         {
            neighbours.Add(Neighbours.transform);
         }
      }

      
   }

   public override void UpdateEffect()
   {
      
   }

   public override void OnReapply()
   {
      time = duration;
   }

   public override void Exit()
   {
      base.Exit();
      neighbours.Clear();
      
      
   }
}
