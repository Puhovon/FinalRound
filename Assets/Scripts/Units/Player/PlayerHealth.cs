using UI;
using Units.Abstract;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Player
{
    public class PlayerHealth : Health
    {
        [Header("Health Stats")] 
        [SerializeField] private int maxHealth;

        [SerializeField] private int _currentHealth;
        [SerializeField] private ParticleSystem psBlood;
        public UnityEvent onDeath = new UnityEvent();

        public void ApplyHeal(int healPoint)
        {
            if (_currentHealth + healPoint != maxHealth) _currentHealth += healPoint;

            if (_currentHealth + healPoint >= maxHealth)
            {
                healPoint = maxHealth - _currentHealth;
                _currentHealth += healPoint;
            }

            UIEvents.onHealthChanged?.Invoke(_currentHealth);
        }

        public override void Initialize()
        {
            _currentHealth = maxHealth;
            UIEvents.onHealthChanged?.Invoke(_currentHealth);
        }


        public override void ApplyDamage(int damage)
        {
            psBlood.Play();
            if (_currentHealth - damage > 0)
                _currentHealth -= damage;
            else
                Death();
            UIEvents.onHealthChanged?.Invoke(_currentHealth);
        }

        public override void Death()
        {
            onDeath?.Invoke();
        }
    }
}