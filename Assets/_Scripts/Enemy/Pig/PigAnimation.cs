using UnityEngine;

public class PigrAnimation : BaseMonoBehaviour
{
    public bool IsFlipped => PlayerCtrl.Instance.SpriteRenderer.flipX;


    public void SetRunning(bool value)
    {
        PlayerCtrl.Instance.Animator.SetBool("isRunning", value);
    }

    public void SetJumping(bool value)
    {
        PlayerCtrl.Instance.Animator.SetBool("isJumping", value);
    }

    public void SetFalling(bool value)
    {
        PlayerCtrl.Instance.Animator.SetBool("isFalling", value);
    }

    public void SetFacingRight(bool value)
    {
        PlayerCtrl.Instance.SpriteRenderer.flipX = !value;
    }

    public void SetAttack(bool value)
    {
        PlayerCtrl.Instance.Animator.SetBool("isAttack", value);
    }

    public void TriggerDead()
    {
       PlayerCtrl.Instance.Animator.SetTrigger("isDead");
    }

    public void TriggerLand()
    {
        PlayerCtrl.Instance.Animator.SetTrigger("Ground");
    }

    public void TriggerHit()
    {
       PlayerCtrl.Instance.Animator.SetTrigger("isHitted");
    }
}