using UnityEngine;
using System.Collections.Generic;

public class BoxForPlayerNormalAttack : AttackBox
{
    private readonly HashSet<PigHealth> hitEnemies = new();

    void FixedUpdate()
    {
        this.CounterAttack();
    }

    public override void BeginAttack()
    {
        hitEnemies.Clear();
        if (this.collider == null) return;

        if (PlayerCtrl.Instance.PlayerAnimation.IsFlipped) {
            this.collider.offset = new Vector2(-Mathf.Abs(this.collider.offset.x), this.collider.offset.y);
            this.collider.enabled = true;
        } else {
            this.collider.offset = new Vector2(Mathf.Abs(this.collider.offset.x), this.collider.offset.y);
            this.collider.enabled = true;
        }
    }

    public override void EndAttack()
    {
        if (this.collider != null) this.collider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (this.collider == null || !this.collider.enabled) return;
        if (!hitEnemies.Add(other.GetComponent<PigHealth>())) return;
        Debug.Log("Hit Enemy: " + other.name);
        base.OnTriggerEnter2D(other);
    }
}