using System;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform bulletInstantiate;
        [SerializeField] private ParticleSystem firePatricles;

        private void Awake()
        {
            EnemyEvents.onFired.AddListener(Fire);
        }

        private void Fire()
        {
            Instantiate(bullet, bulletInstantiate);
            firePatricles.Play();
        }
        
    }
}