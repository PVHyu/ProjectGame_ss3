using System.Collections;
using UnityEngine;

public class ContactDamageBox : AttackBox
{
    private DamageReceiver damageReceiver;

    public override void BeginAttack()
    {
        if (this.collider == null) return;
        this.collider.enabled = true;
    }

    public override void EndAttack() {}

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