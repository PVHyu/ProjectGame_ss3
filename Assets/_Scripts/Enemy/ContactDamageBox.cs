using System.Collections;
using UnityEngine;

public class ContactDamageBox : DamageSender
{
    private DamageReceiver damageReceiver;
    private bool isOnCooldown;

    public void BeginAttack()
    {
        if (this.collider == null) return;
        if (isOnCooldown) return;

        this.collider.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PigDamageReceiver") return;
        if (isOnCooldown) return;

        damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        this.Send(damageReceiver);

        if (PlayerCtrl.Instance.PlayerMovement != null)
            PlayerCtrl.Instance.PlayerMovement.Knockback(transform.position);

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        isOnCooldown = true;
        this.collider.enabled = false;

        yield return new WaitForSeconds(1f);

        isOnCooldown = false;
    }
}