using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class AttackBox : DamageSender
{
    [SerializeField] private float attackDuration = 0.1f;
    [SerializeField] private float attackDelay = 1f;

    private float attackTimer = 0f;
    protected bool waitAttack = false;

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

    protected IEnumerator PauseAttack()
    {
        waitAttack = true;
        yield return new WaitForSeconds(attackDelay);
        waitAttack = false;
    }

    public abstract void BeginAttack();

    public abstract void EndAttack();
}