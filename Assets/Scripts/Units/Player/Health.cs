using UI;
using Units.Abstract;
using Units.Enemy;
using UnityEngine;

namespace Units.Player
{
    public class Health : MonoBehaviour, IDamagable, IHeallable
    {
        [Header("Health Stats")]
        [SerializeField] private int maxHealth;
        
        [SerializeField]private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            if(transform.CompareTag("Player"))
                UIEvents.onHealthChanged?.Invoke(_currentHealth);
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Death();
                if (transform.CompareTag("Enemy"))
                    EnemyMovement.CanMove = false;
                if(transform.CompareTag("Player"))
                    UIEvents.onHealthChanged?.Invoke(_currentHealth);

            }
            else
            {
                if(transform.CompareTag("Player"))
                    UIEvents.onHealthChanged?.Invoke(_currentHealth);
            }
        }

        private void Death()
        {
            if (transform.CompareTag("Enemy"))
            {
                UIEvents.onScoreChanged?.Invoke();
                EnemyEvents.onDeath?.Invoke();
            }
            if (transform.TryGetComponent(out Animator animator))
                animator.SetTrigger("Death");
            else
                Destroy(gameObject);
        }

        public void ApplyHeal(int healPoint)
        {
            if ((_currentHealth + healPoint) != maxHealth)
            {
                _currentHealth += healPoint;
            }

            if (_currentHealth + healPoint >= maxHealth)
            {
                healPoint = maxHealth - _currentHealth;
                _currentHealth += healPoint;
            }
            UIEvents.onHealthChanged?.Invoke(_currentHealth);
        }
    }
}