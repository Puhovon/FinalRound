using Units.Player;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private EnemyHealth _health;
        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();
            _animator = GetComponent<Animator>();
            EnemyEvents.onPlayerDetect.AddListener(Detected);
            EnemyEvents.onPlayerUndetect.AddListener(Undetected);
            EnemyEvents.onWalked.AddListener(Walk);
            _health.onDeath.AddListener(Death);
        }

        private void AfterDeath()
        {
            Destroy(gameObject);
        }
        private void Death()
        {
            _animator.SetTrigger("Death");
        }
        private void Walk(bool walk)
        {
            _animator.SetBool("Walk", walk);
        }
        private void Detected()
        {
            print("Detected");
            _animator.SetBool("Aiming", true);
        }

        private void Undetected()
        {
            _animator.SetBool("Aiming", false);
        }
    }
}