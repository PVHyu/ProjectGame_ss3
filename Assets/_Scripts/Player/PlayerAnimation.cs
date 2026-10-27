using UnityEngine;

public class PlayerAnimation : BaseMonoBehaviour
{
    public bool IsFlipped => PlayerCtrl.Instance.SpriteRenderer.flipX;

    public bool IsIdle()
    {
        AnimatorStateInfo stateInfo = PlayerCtrl.Instance.Animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.IsName("Idle");
    }

    public bool IsAttack()
    {
        AnimatorStateInfo stateInfo = PlayerCtrl.Instance.Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Attack");
    }

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

    public void TriggerAttack()
    {
        PlayerCtrl.Instance.Animator.SetTrigger("isAttack");
    }

    public void TriggerDead()
    {
       PlayerCtrl.Instance.Animator.SetTrigger("isDead");
    }

    public void TriggerHit()
    {
       PlayerCtrl.Instance.Animator.SetTrigger("isHitted");
    }
}