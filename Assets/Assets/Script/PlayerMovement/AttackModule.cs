
using UnityEngine;

namespace Script.PlayerMovement
{
    [System.Serializable]
    public class AttackModule
    {
        public AttackComboSO weapon;
        public float comboConnectTime;
        public float comboTimeOutTime;

        [SerializeField]public int comboState = 0;
        public float timeSinceLastAttack = 0;

        public void Attack(Animator animator)
        {
            //Debug.Log(timeSinceLastAttack + comboTimeOutTime + " " + Time.time);
            if (timeSinceLastAttack + comboTimeOutTime < Time.time)
            {
                ResetTriggers(animator);
                comboState = 0;
            }
            if (timeSinceLastAttack + comboConnectTime > Time.time)
            {
               
                return;
            }
            if (comboState != 0&&!animator.GetCurrentAnimatorStateInfo(1).IsName("Combo"+(comboState-1)))
            {
                
                return;
            }
            animator.SetTrigger("Combo"+comboState);
            timeSinceLastAttack = Time.time;
            weapon = animator.GetComponent<EntityAttack>().weapon;
            
            if (comboState < weapon.ComboLength-1)
            {
                comboState++;
            }
            else
            {
                comboState = 0;
            }
            
        }
        
        
        
        private void ResetTriggers(Animator animator)
        {
            for (int i = 0; i < 3; i++)
            {
                animator.ResetTrigger("Combo"+i);
            }
        }
    }
}