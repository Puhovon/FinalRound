using System;
using Units.Abstract;
using Units.Enemy;
using Units.Player;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] private Transform instantiateBullet;
    [SerializeField] private GameObject bullet;    
    private Ray ray;
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        PlayerEvents.onFired += Attack;
        
    }
    
    private void OnDestroy()
    {
        PlayerEvents.onFired -= Attack;
    }

    private void Attack()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
         if (Input.GetMouseButtonDown(0) && hit.collider.TryGetComponent(out EnemyHealth damagable))
                damagable.ApplyDamage(1);
        
    }

    // private void OnDrawGizmos()
    // {
    //     
    // }
}
