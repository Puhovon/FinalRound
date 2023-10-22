using Units.Abstract;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Abstract
{
    public abstract class Health : MonoBehaviour, IDamagable
    {
        [Header("Health Stats")]
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        
        public ParticleSystem psBlood;

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

        public virtual void Initialize()
        {
            currentHealth = maxHealth;
        }
        
        public abstract void ApplyDamage(int damage);
        
        public abstract void Death();

    }
}