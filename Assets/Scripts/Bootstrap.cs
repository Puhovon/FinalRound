using System.Collections.Generic;
using UI;
using Units.Player;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UIGame _UIGame;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorController _playerAnimatorController;
    private PlayerAttack _playerAttack;
    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private List<Health> _healths;
    void Start()
    {
        _playerAttack = new PlayerAttack(this,gunParticle, MaxAmmo);
        _playerMovement.Initialize(_playerAttack);
        _playerAnimatorController.Initialize();
        _UIGame.Instantiate(MaxAmmo);
        foreach (var health in _healths)
        {
            health.Initialize();
        }
    }
}
