using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Effects/ColdZone")]
public class ColdZoneEffect : Effect
{
   [SerializeField] private float EffectRadius;
   
   private EntityMovement _entityMovement;
   
   
   [SerializeField] private float SlowAmount;
   [SerializeField] private GameObject SlowZone;

   
   public override void Enter(EffectManager obj)
   {
         base.Enter(obj);
         if (obj.TryGetComponent(out _entityMovement))
         {
            _entityMovement = obj.GetComponent<EntityMovement>();

            SlowZone.transform.localScale=new Vector3(EffectRadius,1,EffectRadius);
            SlowZone.GetComponent<ColdZoneBehaviour>().destroyTime = duration;
            Instantiate(SlowZone,_entityMovement.transform.position,quaternion.identity);
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

      }
}
