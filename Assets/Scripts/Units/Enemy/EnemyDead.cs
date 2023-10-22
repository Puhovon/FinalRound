using UI;
using Units.Abstract;
using UnityEngine;

namespace Units.Enemy
{
    [RequireComponent(typeof(Health))]
    public class EnemyDead : MonoBehaviour, IDeadable
    {
        private Health _health;
        
        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public void Die()
        {
            Destroy(this);
            UIEvents.onScoreChanged?.Invoke();
            _health.onDeath?.Invoke();
            if (transform.TryGetComponent(out Animator animator))
                animator.SetTrigger("Death");
            else
                Destroy(gameObject);
        }
    }
}