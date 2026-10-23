using UnityEngine;
using System.Collections.Generic;

public abstract class AttackBox : DamageSender
{
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

    public abstract void BeginAttack();

    public abstract void EndAttack();
}