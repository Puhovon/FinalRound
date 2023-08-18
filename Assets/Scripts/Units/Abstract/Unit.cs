using UnityEngine;

public abstract class Unit
{
    private int _health;
    private int _maxHealth;
    private int _moveSpeed;

    public Unit(int health, int maxHealth, int moveSpeed)
    {
        _health = health;
        _maxHealth = maxHealth;
        _moveSpeed = moveSpeed;
    }
}
