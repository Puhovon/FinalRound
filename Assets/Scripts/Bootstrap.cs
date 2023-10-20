using System.Collections.Generic;
using UI;
using Units.Enemy;
using Units.Player;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UIGame _UIGame;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorController _playerAnimatorController;
    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private List<EnemyHealth> _healths;
    [SerializeField] private PlayerHealth _playerHealth;
    private PlayerAttack _playerAttack;

    void Start()
    {
        _playerAttack = new PlayerAttack(this,gunParticle, MaxAmmo);
        _playerMovement.Initialize(_playerAttack);
        _playerAnimatorController.Initialize();
        _UIGame.Instantiate(MaxAmmo);
        _playerHealth.Initialize();
        foreach (var health in _healths)
        {
            health.Initialize();
        }
    }
}
