using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Abstract
{
    public class Health : MonoBehaviour, IDamagable
    {
        [Header("Health Stats")]
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        
        [SerializeField] private ParticleSystem psBlood;

        public int MaxHealth
        {
            get => maxHealth;
        }

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }
        public UnityEvent onDeath = new UnityEvent();
        public UnityEvent<int> onApplyDamage = new UnityEvent<int>();
        
        
        public void Initialize()
        {
            currentHealth = maxHealth;
        }


        public void ApplyDamage(int damage)
        {
            psBlood.Play();
            if (currentHealth - damage > 0)
            {
                currentHealth -= damage;
                onApplyDamage?.Invoke(currentHealth);                
            }
            else
            {
                if (TryGetComponent(out IDeadable dead))
                {
                    dead.Die();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}