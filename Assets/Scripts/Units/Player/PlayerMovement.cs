using System;
using UnityEngine;

public enum PlayerStates
{
    Jump,
    Stay,
    Walk,
    Run,
    Crouch
}
public class PlayerMovement
{
    public static Action onJumped;
    public static Action<bool> onCrouched;

    
    private PlayerStates _playerStates;
    private PlayerInput _playerInput;
    
    public static Vector3 _moveVector = new Vector3(0, 0, 0);
    private Rigidbody _rb;
    private Transform _transform;
    
    private float _jumpMaxSpeed;
    private float _speed;
    private float _walkMaxSpeed;
    private float _jumpForce;
    private float _inJumpMaxSpeed;
    private float _runMaxSpeed;
    private float _crouchMaxSpeed;

    private float acceleration = 5;
    private float deacceleration = 10;
    
    private const float ChangeMaxSpeedFactor = 6;

    private float _currentMaxSpeed;

    public bool OnGround = false;

    
    public PlayerMovement(Rigidbody rb, float walkMaxSpeed, float jumpForce, float jumpMaxSpeed, float runMaxSpeed, float crouchMaxSpeed)
    {
        _rb = rb;
        _walkMaxSpeed = walkMaxSpeed;
        _jumpForce = jumpForce;
        _jumpMaxSpeed = jumpMaxSpeed;
        _runMaxSpeed = runMaxSpeed;
        _crouchMaxSpeed = crouchMaxSpeed;
        _playerInput = new PlayerInput();
    }
    
    public void Update()
    {
        _playerInput.Update();
        _playerStates = GetPlayerState();
    }

    private PlayerStates GetPlayerState()
    {
        if (_playerInput.JumpPressed)
        {
            onJumped?.Invoke();
            _moveVector.y = _jumpForce;
            return PlayerStates.Jump;
        }

        if (_playerInput.CrouchPressed)
        {
            onCrouched?.Invoke(true);
            return PlayerStates.Crouch;
        }
        onCrouched?.Invoke(false);
        return PlayerStates.Walk;
    }

    private void CalculateMaxSpeed()
    {
        float targetMaxSpeed = _playerStates switch
        {
            PlayerStates.Run => _runMaxSpeed,
            PlayerStates.Jump => _inJumpMaxSpeed,
            PlayerStates.Crouch => _crouchMaxSpeed,
            _ => _walkMaxSpeed,
        };
        _currentMaxSpeed = Mathf.Lerp(_currentMaxSpeed, targetMaxSpeed, Time.fixedDeltaTime * ChangeMaxSpeedFactor);
    }
    
    public void FixedUpdate()
    {
        Move();
        CalculateVelocityY();
        CalculateMaxSpeed();
        _rb.velocity = _moveVector;
    }

    private void Move()
    {
        if (_playerInput.Movement != Vector2.zero)
        {
            _moveVector = new Vector3(_playerInput.Movement.x * _currentMaxSpeed, _moveVector.y, 0);
                        
        }
    }

    private void CalculateVelocityY()
    {
        if (OnGround)
        {
            _moveVector.y = Mathf.Max(_moveVector.y, 0);
            return;
        }
        _moveVector.y += Physics.gravity.y * Time.fixedDeltaTime;
    }
}
