using System;
using Units.Player;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    
    
    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        _playerInput.Update();
        CheckRunAnim();
        // if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        //     PlayerMovement.canMove = false;
                
    }

    private void OnEnable()
    {
        PlayerEvents.OnJumped += Jump;
        PlayerEvents.OnTurned += TurnBack;
        PlayerEvents.onChangedState += ChangeState;
        PlayerEvents.onFired += Attack;
    }

    private void OnDisable()
    {
        PlayerEvents.OnJumped -= Jump;
        PlayerEvents.OnTurned -= TurnBack;
        PlayerEvents.onChangedState -= ChangeState;
        PlayerEvents.onFired -= Attack;
    }

    private void ChangeState(bool withRifle)
    {
        _animator.SetBool("WithRifle", withRifle);
    }

    private void Attack()
    {
        if(PlayerAttack.canFire)
            _animator.SetTrigger("Attack");
    }
    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    private void CheckRunAnim()
    {
        if(Math.Abs(PlayerMovement.MoveVector.x) > 0.2f)
            _animator.SetBool("Run", true);
        else
            _animator.SetBool("Run", false);
    }

    private void TurnBack()
    {
        _animator.SetTrigger("TurnBack");
    }
}
