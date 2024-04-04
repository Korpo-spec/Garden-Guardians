using UnityEngine;

namespace Script.PlayerMovement
{
    [System.Serializable]
    public class AttackModule
    {
        public AttackComboSO weapon;
        public float comboConnectTime;

        [SerializeField]public int comboState = 0;
        public float timeSinceLastAttack = 0;

        public void Attack(Animator animator)
        {
            animator.SetTrigger("Combo"+comboState);
            Debug.Log("Combo"+comboState+ " " +!animator.GetCurrentAnimatorStateInfo(0).IsName("Combo"+comboState));
            if (comboState != 0  && !animator.GetCurrentAnimatorStateInfo(0).IsName("Combo"+comboState))
            {
                Debug.Log("Returning");
                return;
            }
            
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