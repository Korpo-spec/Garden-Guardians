
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

        public bool Attack(Animator animator)
        {
            //Debug.Log(timeSinceLastAttack + comboTimeOutTime + " " + Time.time);
            animator.SetInteger("MaxCombo", weapon.comboLength);
            if (timeSinceLastAttack + comboTimeOutTime < Time.time)
            {
                
                ResetBlend(animator);
                comboState = 0;
            }
            if (timeSinceLastAttack + comboConnectTime > Time.time)
            {
               
                return false;
            }
            
            //animator.SetFloat("Blend", comboState * 0.5f);
            animator.SetTrigger("Combat");
            Debug.Log("Combat Set");
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
            //Debug.Break();
            return true;

        }
        
        public void Attack(Animator animator, Vector3 attackrot)
        {
            
            //Debug.Log(timeSinceLastAttack + comboTimeOutTime + " " + Time.time);
            if (!Attack(animator)) return;
            RotatePlayer(animator.transform, attackrot);
            
        }

        private bool _specialHeld;
        private float _timeSinceLastPress = 0;
        public void SpecialAttack(Animator animator, bool pressed)
        {
            if (!pressed)
            {
                if (_specialHeld)
                {
                    _specialHeld = false;
                    weapon.specialAttack.OnRelease();
                    
                }
                return;
            }
            weapon = animator.GetComponent<EntityAttack>().weapon;
            weapon.specialAttack.OnSpecialSwitch(animator);
            if (!_specialHeld)
            {
                weapon.specialAttack.OnPressed(animator);
                _specialHeld = true;
            }
            else
            {
                weapon.specialAttack.OnHeld();
            }
        }
        
        private void RotatePlayer(Transform transform, Vector3 moveDir)
        {
            if (moveDir.magnitude==0)
            {
                return;
            }
            else
            {
                Vector3 dirVector3 = new Vector3(moveDir.x, 0, moveDir.z);
                transform.forward = dirVector3;
            }
       
        }
        
        
        
        private void ResetBlend(Animator animator)
        {
            for (int i = 0; i < 3; i++)
            {
                animator.SetFloat("Blend", -0.5f);
            }
        }
    }
}