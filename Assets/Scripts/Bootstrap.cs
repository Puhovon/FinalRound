using System.Collections.Generic;
using UI;
using Units.Abstract;
using Units.Player;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UIGame uIGame;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private int maxAmmo;
    [SerializeField] private List<Health> healths;
    [SerializeField] private PlayerHealthComponent playerHealthComponent;
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _playerAttack = new PlayerAttack(this, gunParticle, maxAmmo);
        playerMovement.Initialize(_playerAttack);
        playerAnimatorController.Initialize();
        uIGame.Instantiate(maxAmmo);
        foreach (var health in healths) health.Initialize();
        playerHealthComponent.Initialize();
    }
}