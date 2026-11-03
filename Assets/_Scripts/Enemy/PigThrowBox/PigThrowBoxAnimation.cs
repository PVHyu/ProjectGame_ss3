using UnityEngine;

public class PigThrowBoxAnimation : BaseMonoBehaviour
{
    public bool IsFlipped => PigThrowBoxCtrl.Instance.SpriteRenderer.flipX;

    public bool IsAttack()
    {
        AnimatorStateInfo stateInfo = PigThrowBoxCtrl.Instance.Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("ThrowingBox");
    }

    public void SetPickBox(bool value)
    {
        PigThrowBoxCtrl.Instance.Animator.SetBool("pickBox", value);
    }

    public void SetFacingLeft(bool value)
    {
        PigThrowBoxCtrl.Instance.SpriteRenderer.flipX = !value;
    }

    public void TriggerAttack()
    {
        PigThrowBoxCtrl.Instance.Animator.SetTrigger("isAttack");
    }

    public void TriggerDead()
    {
       PigThrowBoxCtrl.Instance.Animator.SetTrigger("isDead");
    }

    public void TriggerHit()
    {
       PigThrowBoxCtrl.Instance.Animator.SetTrigger("isHitted");
    }
}