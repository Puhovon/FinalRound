using Units.Abstract;
using UnityEngine;

namespace Units.Universal
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float force = 5f;
        [SerializeField] private int damage;
        [SerializeField] private Rigidbody rb;
        private void Awake()
        {
            rb.AddForce(transform.parent.forward * force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            try
            {
                if (other.transform.parent.TryGetComponent(out IDamagable damagable))
                {
                    damagable.ApplyDamage(damage);
                    Destroy(gameObject);
                }
                
            }
            catch
            {
                print("Cant confirm damage");
            }
        }
    }
}