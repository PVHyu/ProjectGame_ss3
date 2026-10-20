using UnityEngine;

public class PlayerAnimation : BaseMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadAnimator();
        LoadSpriteRenderer();
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;

        animator = transform.parent.GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;

        spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
        Debug.Log(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    public void SetRunning(bool value)
    {
        animator.SetBool("isRunning", value);
    }

    public void SetJumping(bool value)
    {
        animator.SetBool("isJumping", value);
    }

    public void SetFalling(bool value)
    {
        animator.SetBool("isFalling", value);
    }

    public void SetFacingRight(bool value)
    {
        spriteRenderer.flipX = !value;
    }

    public void SetAttack(bool value)
    {
        animator.SetBool("isAttack", value);
    }

    public void TriggerLand()
    {
        animator.SetTrigger("Ground");
    }
}