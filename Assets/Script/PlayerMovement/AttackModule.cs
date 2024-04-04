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
                Debug.Log("Resetting");
                ResetTriggers(animator);
                comboState = 0;
            }

            if (timeSinceLastAttack + comboConnectTime > Time.time)
            {
                Debug.Log("Click too close");
                return;
            }
            Debug.Log(comboState);
            if (comboState != 0&&!animator.GetCurrentAnimatorStateInfo(1).IsName("Combo"+(comboState-1)))
            {
                Debug.Log("Has not swtitched to next combo yet"+ comboState);
                return;
            }
            Debug.Log(comboState);
            animator.SetTrigger("ResetToEntry");
            animator.SetTrigger("Combo"+comboState);
            timeSinceLastAttack = Time.time;
            
            
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
            for (int i = 0; i < weapon.ComboLength; i++)
            {
                animator.ResetTrigger("Combo"+i);
            }
        }
    }
}