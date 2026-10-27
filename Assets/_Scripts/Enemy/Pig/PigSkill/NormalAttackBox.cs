using System.Collections;
using UnityEngine;

public class NormalAttackBox : AttackBox
{
    private DamageReceiver damageReceiver;

    public override void BeginAttack()
    {
        if (this.collider == null) return;
        this.collider.enabled = true;
        if (!waitAttack)
        {
            PigCtrl.Instance.PigAnimation.TriggerAttack();
            StartCoroutine(PauseAttack());
            this.EndAttack();
        } 
    }

    public override void EndAttack() 
    {
        if (this.collider == null) return;
        this.collider.enabled = false;
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