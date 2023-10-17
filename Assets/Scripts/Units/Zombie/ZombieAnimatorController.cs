using Units.Enemy;
using UnityEngine;

public class ZombieAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator.GetComponent<Animator>();
        EnemyEvents.onZombieAttack.AddListener(Attack);
        // EnemyEvents.onZombieDeatectPlayer.AddListener();
        EnemyEvents.onZombieWalk.AddListener(Walk);
        EnemyEvents.onZombieDeath.AddListener(Death);
        // EnemyEvents.onZombieUnDeatectPlayer.AddListener();
    }

    private void Attack()
    {
        
    }

    private void Walk(bool isWalking)
    {
        
    }

    private void Death()
    {
        
    }
}
