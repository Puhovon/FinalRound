using Units.Player;
using UnityEngine;

public class PlayerInput
{
    
    public Vector2 Movement { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool CrouchPressed { get; private set; }
    public bool Fire { get; private set; }
    public bool StateWithRifle { get; private set; } = false;
    
    
    public void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Movement = new Vector2(horizontal, 0);
        JumpPressed = Input.GetButtonDown("Jump") ? true : false;
        if (Input.GetButtonDown("Fire2"))
        {
            StateWithRifle = !StateWithRifle;
            PlayerEvents.onChangedState?.Invoke(StateWithRifle);
        }
        Fire = Input.GetButtonDown("Fire1") ? true : false;

    }
}
