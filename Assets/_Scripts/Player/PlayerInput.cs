using UnityEngine;

public class PlayerInput : BaseMonoBehaviour
{
    private PlayerMovement movement;
    
    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
    }

    protected virtual void LoadMovement()
    {
        if (movement != null) return;

        movement = transform.parent.GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + ": LoadMovement", gameObject);
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        movement.SetMoveInput(horizontal);
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.RequestJump();
        }
    }
}