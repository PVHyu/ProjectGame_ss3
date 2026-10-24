using UnityEngine;

public class PlayerInput : BaseMonoBehaviour
{
    private void Update()
    {
        HandleAttackInput();
        HandleMovementInput();
        HandleJumpInput();
    }

    private void HandleMovementInput()
    {
        if (PlayerCtrl.Instance.PlayerAnimation.IsAttack()) 
        {
            PlayerCtrl.Instance.PlayerMovement.SetMoveInput(0f);
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        PlayerCtrl.Instance.PlayerMovement.SetMoveInput(horizontal);
    }

    private void HandleJumpInput()
    {
        if (PlayerCtrl.Instance.PlayerAnimation.IsAttack()) return;
        if (Input.GetKeyDown(KeyCode.UpArrow)) PlayerCtrl.Instance.PlayerMovement.RequestJump();
    }

    private void HandleAttackInput()
    {
        if (PlayerCtrl.Instance.PlayerMovement.IsJumping) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        PlayerCtrl.Instance.ManagingSkills?.GetSkill("Normal Attack")?.Attack();
    }
}