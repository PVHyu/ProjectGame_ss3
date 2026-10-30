using System.Collections;
using UnityEngine;

public class NormalAttackBox : AttackBox
{
    private DamageReceiver damageReceiver;

    public override void BeginAttack()
    {
        if (this.circleCollider2D == null) return;

        if (PigCtrl.Instance.PigAnimation.IsFlipped) 
        {
            this.circleCollider2D.offset = new Vector2(Mathf.Abs(this.circleCollider2D.offset.x), this.circleCollider2D.offset.y);
            this.circleCollider2D.enabled = true;
        } else {
            this.circleCollider2D.offset = new Vector2(-Mathf.Abs(this.circleCollider2D.offset.x), this.circleCollider2D.offset.y);
            this.circleCollider2D.enabled = true;
        }

        if (!waitAttack)
        {
            PigCtrl.Instance.PigAnimation.TriggerAttack();
            StartCoroutine(PauseAttack());
            this.EndAttack();
        } 
    }

    public override void EndAttack() 
    {
        if (this.circleCollider2D == null) return;
        this.circleCollider2D.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PigHealth") return;

        damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        this.Send(damageReceiver);
 
        if (PlayerCtrl.Instance.PlayerMovement != null)
            PlayerCtrl.Instance.PlayerMovement.Knockback(transform.position);
    }
}