using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
#region SerializeFields

    [SerializeField] private float _walkMaxSpeed = 3;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _inJumpMaxSpeed = 7;
    [SerializeField] private float _runMaxSpeed = 7;
    [SerializeField] private float _crouchMaxSpeed = 1;
    
#endregion

    private PlayerAnimatorController _animatorController;
    private PlayerMovement _playerMovement;

    public static bool onGround = false;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerMovement =
            new PlayerMovement(rb, _walkMaxSpeed, _jumpForce, _inJumpMaxSpeed, _runMaxSpeed, _crouchMaxSpeed);
    }

    void Update()
    {
        _playerMovement.Update();
        print(_playerMovement.OnGround);
    }
    
    private void FixedUpdate()
    {
        _playerMovement.FixedUpdate();
    }

    private void OnCollisionStay(Collision other)
    {
        _playerMovement.OnGround = true;
        onGround = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _playerMovement.OnGround = false;
        onGround = false;
    }
}
