using System.Collections;
using UnityEngine;

public class ThrowBombAttack : BaseAttack
{   
    [SerializeField] private AttackArea attackArea;
    public AttackArea AttackArea => attackArea;
    [SerializeField] private ThrowingBomb throwingBomb;
    public ThrowingBomb ThrowingBomb => throwingBomb;

    [Header("Attack Timing")]
    [SerializeField] private float attackCooldown = 3f;
    private bool isAttacking = false;

    private void Update()
    {
        // if (!PigBoxCtrl.Instance.PigThrowBoxHealth.IsDead()) this.Attack();
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
        if (this.throwingBomb != null) return;
        this.throwingBomb = GetComponent<ThrowingBomb>();
        Debug.Log(transform.name + ": Load ThrowingBomb", gameObject);
    }

    public override void Attack()
    {
        if (!this.attackArea.IsDetectPlayer() || isAttacking) return;
        Debug.Log("Attack is " + (isAttacking ? "in progress" : "ready"));
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PigBoxCtrl.Instance.PigBoxAnimation.TriggerAttack();

        yield return new WaitUntil(() => PigBoxCtrl.Instance.PigBoxAnimation.IsAttack());
        yield return new WaitUntil(() => !PigBoxCtrl.Instance.PigBoxAnimation.IsAttack());
        
        this.throwingBomb.Throw(this.attackArea.TargetPlayer);

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}