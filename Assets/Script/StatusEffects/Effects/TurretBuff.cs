using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Effects
{
    [CreateAssetMenu(menuName = "Effects/TurretBuff")]
    public class TurretBuff : Effect
    {
        public float attackSpeedAmp;
        public float damageAmp;
        public float rangeAmp;

        /*
        private List<Tower> turrets;

        public override void Enter(GameObject obj)
        {
            base.Enter(obj);
            
            turrets = new List<Tower>();

            turrets = obj.GetComponentsInChildren<Tower>().ToList();
            for (int i = 0; i < turrets.Count; i++)
            {
                turrets[i].attackSpeed += attackSpeedAmp;
                turrets[i].range += rangeAmp;
                turrets[i].damage += damageAmp;
                turrets[i].UpdateRange();
            }
        }

        public override void OnReapply()
        {
            time = duration;
        }

        public override void Exit()
        {
         
            base.Exit();
            for (int i = 0; i < turrets.Count; i++)
            {
                turrets[i].attackSpeed -= attackSpeedAmp;
                turrets[i].range -= rangeAmp;
                turrets[i].damage -= damageAmp;
                turrets[i].UpdateRange();
            }
        }
        */
    }
}