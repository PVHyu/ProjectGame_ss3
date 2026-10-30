using UnityEngine;
using System.Collections.Generic;

public class BoxForPlayerNormalAttack : AttackBox
{
    private readonly HashSet<PigHealth> hitEnemies = new();

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

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (this.circleCollider2D == null || !this.circleCollider2D.enabled) return;
        if (!hitEnemies.Add(other.GetComponent<PigHealth>())) return;

        base.OnTriggerEnter2D(other);

        if (PigCtrl.Instance.PigMovement != null && other.gameObject.CompareTag("Enemy"))
            PigCtrl.Instance.PigMovement.Knockback(transform.position);
        
    }
}