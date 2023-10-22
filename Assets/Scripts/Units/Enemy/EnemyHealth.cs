using UI;
using Units.Abstract;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyHealth : Health, IDamagable
    {
        public override void ApplyDamage(int damage)
        {
            CurrentHealth -= damage;
            psBlood.Play();
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
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