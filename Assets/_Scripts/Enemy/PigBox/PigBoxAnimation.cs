using UnityEngine;

public class PigBoxAnimation : BaseMonoBehaviour
{
    public bool IsFlipped => PigBoxCtrl.Instance.SpriteRenderer.flipX;

    public bool IsAttack()
    {
        AnimatorStateInfo stateInfo = PigBoxCtrl.Instance.Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("ThrowingBox");
    }

    public void SetPickBox(bool value)
    {
        PigBoxCtrl.Instance.Animator.SetBool("pickBox", value);
    }
    
    public void SetRunning(bool value)
    {
        PigBoxCtrl.Instance.Animator.SetBool("isRunning", value);
    }

    public void SetFacingLeft(bool value)
    {
        PigBoxCtrl.Instance.SpriteRenderer.flipX = !value;
    }

    public void TriggerAttack()
    {
        PigBoxCtrl.Instance.Animator.SetTrigger("isAttack");
    }

    public void TriggerDead()
    {
        Debug.Log(transform.name + ": TriggerDead", gameObject);
        PigBoxCtrl.Instance.Animator.SetTrigger("isDead");
    }

    public void TriggerHit()
    {
        PigBoxCtrl.Instance.Animator.SetTrigger("isHitted");
    }
}