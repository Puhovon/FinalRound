using System;
using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public enum PlayerStates
    {
        IdleWalk, WithRifleWalk
    }

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player Config")]
        [SerializeField] private float _maxSpeed;

        [SerializeField] private float _jumpForce;
        private PlayerAttack _playerAttack;

        private float _currentMaxSpeed;
        private const float ChangeMaxSpeedFactor = 6;
        private bool OnGround = false;
        
        public static Vector3 MoveVector = new Vector3(0,0,0);
        private PlayerInput _playerInput;
        private PlayerStates _playerStates;
        private Rigidbody _playerRb;

        public static bool canMove = true;
        private bool faceRight = true;
        public void Initialize(PlayerAttack playerAttack)
        {
            _playerAttack = playerAttack;
            _playerRb = GetComponent<Rigidbody>();
            _playerInput = new PlayerInput();
            _currentMaxSpeed = _maxSpeed;
        }

        private void Update()
        {
            _playerInput.Update();
            _playerStates = GetPlayerState();
            if(_playerStates == PlayerStates.WithRifleWalk && _playerInput.Fire && PlayerAttack.canFire)
                PlayerEvents.onFired?.Invoke();
            if (_playerInput.Movement.x > 0 && !faceRight)
                Flip();
            if (_playerInput.Movement.x < 0 && faceRight)
                Flip();
        }
        
        private void FixedUpdate()
        {
            if (canMove)
            {
                Move();
                _playerRb.velocity = MoveVector;    
            }
        }

        private void Flip()
        {
            Vector3 rot = _playerRb.rotation.eulerAngles;
            rot = new Vector3(rot.x,rot.y+180,rot.z);
            _playerRb.rotation = Quaternion.Euler(rot);
            faceRight = !faceRight;
        }
        
        private void Move()
        {
            MoveVector = new Vector3(_playerInput.Movement.x * _maxSpeed, _playerRb.velocity.y, 0);
        }
        
        private PlayerStates GetPlayerState()
        {
            if (_playerInput.JumpPressed && _playerStates == PlayerStates.IdleWalk && OnGround)
            {
                PlayerEvents.OnJumped?.Invoke();
                _playerRb.velocity = new Vector3(_playerRb.velocity.x, _jumpForce, 0);
            }
            if (_playerInput.StateWithRifle)
            {
                return PlayerStates.WithRifleWalk;
            }
            return PlayerStates.IdleWalk;
        }

        private void OnCollisionExit(Collision other)
        {
            OnGround = false;
        }

        private void OnCollisionStay(Collision other)
        {
            OnGround = true;
        }
    }
}
