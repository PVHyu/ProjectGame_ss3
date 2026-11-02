using System.Collections;
using UnityEngine;

public class ThrowBoxAttack : BaseAttack
{   
    [SerializeField] private AttackArea attackArea;
    public AttackArea AttackArea => attackArea;

    [SerializeField] private ThrowingBox throwingBox;
    public ThrowingBox ThrowingBox => throwingBox;

    [Header("Attack Timing")]
    [SerializeField] private float attackCooldown = 3f;
    private bool isAttacking = false;

    private void FixedUpdate()
    {
        this.Attack();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents(); 
        this.LoadAttackArea();
        this.LoadThrowingBox();
    }

    protected virtual void LoadAttackArea()
    {
        if (this.attackArea != null) return;
        this.attackArea = GetComponentInParent<AttackArea>();
        Debug.Log(transform.name + ": Load AttackArea", gameObject);
    }

    protected virtual void LoadThrowingBox()
    {
        if (this.throwingBox != null) return;
        this.throwingBox = GetComponent<ThrowingBox>();
        Debug.Log(transform.name + ": Load ThrowingBox", gameObject);
    }

    public override void Attack()
    {
        if (!this.attackArea.IsDetectPlayer()) return;

        if (isAttacking) return;
        StartCoroutine(AttackCooldownCoroutine());
      
        if (this.throwingBox != null) this.throwingBox.Throw(this.attackArea.TargetPlayer);
    }

    IEnumerator AttackCooldownCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}