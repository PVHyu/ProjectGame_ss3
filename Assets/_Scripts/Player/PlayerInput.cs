using UnityEngine;

public class PlayerInput : BaseMonoBehaviour
{
    private PlayerMovement movement;
    private ManagingSkills playerSkills;
    
    private void Update()
    {
        HandleAttackInput();

    if (!PlayerCtrl.Instance.PlayerAnimation.IsAttack())
    {
        HandleMovementInput();
        HandleJumpInput();
    }
    else
    {
        movement.SetMoveInput(0f);
    }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
        LoadSkills();
    }

    protected virtual void LoadMovement()
    {
        if (movement != null) return;

        movement = transform.parent.GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + ": LoadMovement", gameObject);
    }

    protected virtual void LoadSkills()
    {
        if (playerSkills != null) return;

        playerSkills = transform.parent.GetComponentInChildren<ManagingSkills>();
        Debug.Log(transform.name + ": LoadSkills", gameObject);
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

    private void HandleAttackInput()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !PlayerCtrl.Instance.PlayerAnimation.IsIdle()) return;
        if (playerSkills == null) return;
        PlayerCtrl.Instance.PlayerMovement.StopMovement();
        BaseAttack skill = playerSkills?.GetSkill("Normal Attack");
        skill?.Attack();
    }
}