using UI;
using Units.Abstract;
using Units.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Player
{
    public class Health : MonoBehaviour, IDamagable, IHeallable
    {
        [Header("Health Stats")]
        [SerializeField] private int maxHealth;
        
        [SerializeField] private int _currentHealth;
        [SerializeField] private ParticleSystem psBlood;
        public UnityEvent onDeath = new UnityEvent();

        public void Initialize()
        {
            _currentHealth = maxHealth;
            if(transform.CompareTag("Player"))
                UIEvents.onHealthChanged?.Invoke(_currentHealth);
        }

       
        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            psBlood.Play();
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
                onDeath?.Invoke();
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