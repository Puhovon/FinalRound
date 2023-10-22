using UI;
using Units.Abstract;
using UnityEngine;

namespace Units.Player
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealthComponent : MonoBehaviour, IHeallable
    {
        private Health _health;
        
        public void Initialize()
        {
            _health = GetComponent<Health>();
            UIEvents.onHealthChanged?.Invoke(_health.CurrentHealth);
            _health.onApplyDamage.AddListener(OnApplyDamage);
            _health.onDeath.AddListener(Die);
        }

        private void OnApplyDamage(int currentHealth)
        {
            UIEvents.onHealthChanged?.Invoke(currentHealth);
        }

        private void Die()
        {
            Destroy(_health);
        }
        
        public void ApplyHeal(int healPoint)
        {
            if (_health.CurrentHealth + healPoint != _health.CurrentHealth) _health.CurrentHealth += healPoint;

            if (_health.CurrentHealth + healPoint >= _health.MaxHealth)
            {
                healPoint =  _health.MaxHealth - _health.CurrentHealth;
                _health.CurrentHealth += healPoint;
            }

            UIEvents.onHealthChanged?.Invoke(_health.CurrentHealth);
        }
    }
}