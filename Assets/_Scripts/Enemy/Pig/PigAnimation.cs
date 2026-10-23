using UnityEngine;

public class PigAnimation : BaseMonoBehaviour
{
    public bool IsFlipped => PigCtrl.Instance.SpriteRenderer.flipX;


    public void SetRunning(bool value)
    {
        PigCtrl.Instance.Animator.SetBool("isRunning", value);
    }

    public void SetJumping(bool value)
    {
        PigCtrl.Instance.Animator.SetBool("isJumping", value);
    }

    public void SetFalling(bool value)
    {
        PigCtrl.Instance.Animator.SetBool("isFalling", value);
    }

    public void SetFacingRight(bool value)
    {
        PigCtrl.Instance.SpriteRenderer.flipX = !value;
    }

    public void SetAttack(bool value)
    {
        PigCtrl.Instance.Animator.SetBool("isAttack", value);
    }

    public void TriggerDead()
    {
       PigCtrl.Instance.Animator.SetTrigger("isDead");
    }

    public void TriggerLand()
    {
        PigCtrl.Instance.Animator.SetTrigger("Ground");
    }

    public void TriggerHit()
    {
       PigCtrl.Instance.Animator.SetTrigger("isHitted");
    }
}