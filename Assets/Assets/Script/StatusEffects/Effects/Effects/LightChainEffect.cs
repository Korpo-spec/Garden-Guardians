using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Effects/chainEffect")]
public class LightChainEffect : Effect
{
   [SerializeField]
   private float EffectRadius;
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
         neighbour.GetComponent<EntityHealth>().DamageUnit(Damage,false,null);
      }

      _lineRenderer = ChainSource.GetComponent<LineRenderer>();
      var neighpos = new List<Vector3>();
      foreach (var neigh in neighbours)
      {
         neighpos.Add(neigh.position);
      }

      _lineRenderer.positionCount = neighpos.Count;
      _lineRenderer.SetPositions(neighpos.ToArray());
      
      Instantiate(ChainSource,_entityMovement.transform);
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
