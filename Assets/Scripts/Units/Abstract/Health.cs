using Units.Abstract;
using UnityEngine;

namespace Units.Player
{
    public abstract class Health : MonoBehaviour, IDamagable
    {
        public abstract void Initialize();
        
        public abstract void ApplyDamage(int damage);
        
        public abstract void Death();

    }
}