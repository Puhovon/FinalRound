using System;
using Units.Abstract;
using Units.Player;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private int healPoint;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.transform.parent.TryGetComponent(out PlayerHealth heallable))
            {
                print(typeof(PlayerHealth));
                heallable.ApplyHeal(healPoint);
                _animator.SetTrigger("Taked");
                
            }
        }
        catch (Exception e)
        {
            print($"EROOR: {e}\nCollider: {other.transform.parent.name}");
        }
        
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
