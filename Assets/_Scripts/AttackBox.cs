using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class AttackBox : DamageSender
{
    [SerializeField] private float attackBoxDuration = 0.1f;
    [SerializeField] private float attackDelay = 1f;

    private float attackTimer = 0f;
    protected bool waitAttack = false;

    void FixedUpdate()
    {
        this.CounterAttackBox();
    }
    
    protected virtual void CounterAttackBox()
    {
        if (this.circleCollider2D == null || !this.circleCollider2D.enabled) return;
        this.attackTimer += Time.fixedDeltaTime;
        if (this.attackTimer >= this.attackBoxDuration)
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