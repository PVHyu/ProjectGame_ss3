using UnityEngine;
using System.Collections.Generic;

public class BoxForPlayerNormalAttack : AttackBox
{
    private readonly HashSet<DamageReceiver> hitEnemies = new();

    void FixedUpdate()
    {
        this.CounterAttackBox();
    }

    public override void BeginAttack()
    {
        hitEnemies.Clear();
        if (this.circleCollider2D == null) return;

        if (PlayerCtrl.Instance.PlayerAnimation.IsFlipped) {
            this.circleCollider2D.offset = new Vector2(-Mathf.Abs(this.circleCollider2D.offset.x), this.circleCollider2D.offset.y);
            this.circleCollider2D.enabled = true;
        } else {
            this.circleCollider2D.offset = new Vector2(Mathf.Abs(this.circleCollider2D.offset.x), this.circleCollider2D.offset.y);
            this.circleCollider2D.enabled = true;
        }
    }

    public override void EndAttack()
    {
        if (this.circleCollider2D != null) this.circleCollider2D.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (circleCollider2D == null || !circleCollider2D.enabled) return;
        if (!collision.CompareTag("Enemy")) return;

        var damageReceiver = collision.GetComponent<DamageReceiver>();
        if (damageReceiver == null || !hitEnemies.Add(damageReceiver)) return;

        base.OnTriggerEnter2D(collision);

        var enemyMovement = collision.GetComponentInParent<EnemyMovement>();
        enemyMovement?.Knockback(transform.position);
        
    }
}