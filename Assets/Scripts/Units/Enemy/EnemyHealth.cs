using UI;
using Units.Abstract;
using Units.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Enemy
{
    public class EnemyHealth : Health, IDamagable
    {
        [SerializeField] private int maxHealth;

        [SerializeField] private int _currentHealth;
        [SerializeField] private ParticleSystem psBlood;
        public UnityEvent onDeath = new UnityEvent();

        public override void Initialize()
        {
            _currentHealth = maxHealth;
        }

        public override void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            psBlood.Play();
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Death();
            }
        }

        public override void Death()
        {
            Destroy(this);
            UIEvents.onScoreChanged?.Invoke();
            onDeath?.Invoke();
            if (transform.TryGetComponent(out Animator animator))
                animator.SetTrigger("Death");
            else
                Destroy(gameObject);
        }
    }
}