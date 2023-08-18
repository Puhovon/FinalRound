using UnityEngine;

public class PlayerInput
{
    public Vector2 Movement { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool CrouchPressed { get; private set; }
    
    public void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Movement = new Vector2(horizontal, vertical);
        JumpPressed = Input.GetButtonDown("Jump") ? true : false;
        CrouchPressed = Input.GetKey(KeyCode.LeftControl) ? true : false;
    }
}
