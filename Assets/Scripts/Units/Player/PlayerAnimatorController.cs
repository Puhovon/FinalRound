using System;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        _playerInput.Update();
        CheckRunAnim();
    }

    private void OnEnable()
    {
        PlayerMovement.onJumped += Jump;
        PlayerMovement.onCrouched += Crouch;
    }

    private void OnDisable()
    {
        PlayerMovement.onJumped -= Jump;
        PlayerMovement.onCrouched -= Crouch;
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    private void Crouch()
    {
        _animator.SetBool("Crouch", true);        
    }
    
    private void CheckRunAnim()
    {
        if(Math.Abs(PlayerMovement._moveVector.x) > 0.1f)
            _animator.SetBool("Run", true);
        else
            _animator.SetBool("Run", false);
    }    
}
