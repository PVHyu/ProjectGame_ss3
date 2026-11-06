using UnityEngine;
using System.Collections.Generic;

public class AreaExplostionAttack : AttackBox
{
    private DamageReceiver damageReceiver;
    private readonly HashSet<DamageReceiver> hitPlayer = new();


    void FixedUpdate()
    {
        this.CounterAttackBox();
    }

    public override void BeginAttack()
    {
        hitPlayer.Clear();
        
        if (this.circleCollider2D == null) return;
        this.circleCollider2D.enabled = true;
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PigHealth") return;

        damageReceiver = collision.GetComponent<DamageReceiver>();
        if (damageReceiver == null || !hitPlayer.Add(damageReceiver)) return;
        
        this.Send(damageReceiver);
 
        if (PlayerCtrl.Instance.PlayerMovement != null)
            PlayerCtrl.Instance.PlayerMovement.Knockback(transform.position);        
    }
}