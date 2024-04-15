
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Entity
{
    [RequireComponent(typeof(EffectManager))]
    public class EntityHealth : MonoBehaviour
    {
        //[Tooltip("Optional int variable that can be put in, if this is empty it will use the int")] [SerializeField]
        //protected IntVariable intVariable;

        [Tooltip("Health if no int variable was used")] [SerializeField]
        public float health;

        

        [Tooltip("The Scriptable object that contains transforms as keys and health as value. ")] [SerializeField]
        protected TransformHealthDictionary transformHealthDictionary;
        
        [Tooltip("If the gameobject is destroyed on kill")]
        [SerializeField] private bool destroyOnKill;

        [SerializeField] private EntityHealthEvents entityHealthEvents;

        private Rigidbody _rigidbody;
        private Animator _animator;
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        [SerializeField] private GameObject spawnOnDestroy;

        /// <summary>
        /// Used to keep track so it can't die multiple times in the same frame
        /// </summary>
        private bool _isDead = false;

        [HideInInspector] public EffectManager effectManager;


        private void Awake()
        {
            effectManager = GetComponent<EffectManager>();
        }

        void Start()
        {
            /*
            if (intVariable)
            {
                health = intVariable.Value;
            }
            */
            transformHealthDictionary.Add(transform, this);
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            Enable();
        }

        protected virtual void Enable()
        {
        }

        private void OnDestroy()
        {
            //transformHealthDictionary.Remove(transform);
            //TODO: FIX SPAWN ON DESTROY
            //Instantiate(spawnOnDestroy);
        }

        /// <summary>
        /// Decreases the the health by the input amount. This will also decrease the health of the attached intVariable if present.
        /// </summary>
        /// <param name="amount"> The amount of health to lose</param>
        public virtual void DamageUnit(float amount)
        {
            if (health - amount <= 0)
                KillItself();
            else
                entityHealthEvents.takeDamage.Invoke();

            // if (intVariable)
            //     intVariable.Value -= amount;

            health -= amount;
            //_animator.SetTrigger(Hurt);
        }

        protected virtual void KillItself()
        {
            if (_isDead)
                return;

            _isDead = true;
            if (spawnOnDestroy)
            {
                var transform1 = transform;
                Instantiate(spawnOnDestroy, transform1.position, transform1.rotation);
            }
        

            entityHealthEvents.die.Invoke();
            if (destroyOnKill)
            {
                Destroy(gameObject); 
            }
            
        }

        /// <summary>
        /// Increases the the health by the input amount. This will also increase the health of the attached intVariable if present.
        /// </summary>
        /// <param name="amount"> The amount of health to lose</param>
        public virtual void IncreaseHealth(int amount)
        {
            entityHealthEvents.heal.Invoke();
            // if (intVariable)
            //     intVariable.Value += amount;
            health += amount;
        }
        
        
    }

    [System.Serializable]
    public class EntityHealthEvents
    {
        [Header("Events")]
        [SerializeField] public UnityEvent takeDamage;
        [SerializeField] public UnityEvent heal;
        [SerializeField] public UnityEvent die;
    }
}