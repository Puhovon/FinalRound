using System.Collections;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Player
{
    public class PlayerAttack
    {
        public static int maxAmmo = 3;
        private float bulletSpeed;
        private GameObject bullet;
        private ParticleSystem particles;

        public static int currentAmmo;
        public static bool canFire = true;

        private MonoBehaviour _mono;

        public PlayerAttack(MonoBehaviour mono, ParticleSystem gunParticle, int _maxAmmo)
        {
            maxAmmo = _maxAmmo > 0 ? _maxAmmo : 15;
            particles = gunParticle;
            _mono = mono;
            currentAmmo = maxAmmo;
            PlayerEvents.onFired += Fire;
        }

        private IEnumerator Reload()
        {
            for (int i = 0; i < 3; i++)
            {

                yield return new WaitForSeconds(1);
            }

            currentAmmo = maxAmmo;
            UIEvents.onAmmoChanged?.Invoke();
            canFire = true;
        }

        private void Fire()
        {
            if (canFire)
            {
                currentAmmo -= 1;
                UIEvents.onAmmoChanged?.Invoke();
                particles.Play();
                Debug.Log("Fire");
                if (currentAmmo == 0)
                {
                    canFire = false;
                    _mono.StartCoroutine(Reload());
                }
            }
        }
    }
}