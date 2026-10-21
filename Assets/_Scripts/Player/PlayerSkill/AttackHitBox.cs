using UnityEngine;
using System.Collections.Generic;

public class AttackHitbox : DamageSender
{
    private readonly HashSet<EnemyHealth> hitEnemies = new();
    [SerializeField] private float attackDuration = 0.1f;
    private float attackTimer = 0f;

    void FixedUpdate()
    {
        this.CounterAttack();
    }
    
    protected virtual void CounterAttack()
    {
        if (this.collider == null || !this.collider.enabled) return;
        this.attackTimer += Time.fixedDeltaTime;
        if (this.attackTimer >= this.attackDuration)
        {
            this.EndAttack();
            this.attackTimer = 0f;
        }
    }

    public void BeginAttack()
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

    public void EndAttack()
    {
        if (this.collider != null) this.collider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (this.collider == null || !this.collider.enabled) return;
        if (!hitEnemies.Add(other.GetComponent<EnemyHealth>())) return;
        Debug.Log("Hit Enemy: " + other.name);
        base.OnTriggerEnter2D(other);
    }
}