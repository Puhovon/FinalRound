using Units.Abstract;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamagable
{
    public abstract int _health { get; protected set; }
    public abstract int _maxHealth { get; protected set; }


    public abstract void ApplyDamage(int damage);
}
