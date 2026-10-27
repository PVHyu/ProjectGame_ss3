using UnityEngine;

public class PigAnimation : BaseMonoBehaviour
{
    public bool IsFlipped => PigCtrl.Instance.SpriteRenderer.flipX;


    public void SetRunning(bool value)
    {
        PigCtrl.Instance.Animator.SetBool("isRunning", value);
    }

    public void SetFacingLeft(bool value)
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

    public void TriggerHit()
    {
       PigCtrl.Instance.Animator.SetTrigger("isHitted");
    }
}