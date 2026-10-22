using System.Collections;
using UnityEngine;

public class ContactDamageBox : DamageSender
{
    private DamageReceiver damageReceiver;

    public void BeginAttack()
    {
        if (this.collider == null) return;
        this.collider.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PigDamageReceiver") return;

        damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        this.Send(damageReceiver);

        if (PlayerCtrl.Instance.PlayerMovement != null)
            PlayerCtrl.Instance.PlayerMovement.Knockback(transform.position);
    }
}