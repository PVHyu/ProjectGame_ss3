using System.Collections;
using UnityEngine;

public class BoxDamageSender : AttackBox
{
    private DamageReceiver damageReceiver;

    public override void BeginAttack()
    {
        if (this.circleCollider2D == null) return;
        this.circleCollider2D.enabled = true;
    }

    public override void EndAttack() {}

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        damageReceiver = other.collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        this.Send(damageReceiver);
 
        if (PlayerCtrl.Instance.PlayerMovement != null)
            PlayerCtrl.Instance.PlayerMovement.Knockback(transform.position);
    }
}