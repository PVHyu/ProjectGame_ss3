using UnityEngine;

public class NormalAttack : Skill
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private AttackHitbox attackHitbox;
    [SerializeField] private int damage = 10;
    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerAnimation();
        LoadAttackHitbox();
    }

    protected virtual void LoadAttackHitbox()
    {
        if (attackHitbox != null) return;

        attackHitbox = GetComponentInParent<AttackHitbox>();
        Debug.Log(transform.name + ": LoadAttackHitbox", gameObject);
    }

    protected virtual void LoadPlayerAnimation()
    {
        if (playerAnimation != null) return;

        playerAnimation = transform.root.GetComponentInChildren<PlayerAnimation>();
        Debug.Log(transform.name + ": LoadPlayerAnimation", gameObject);
    }

    public override void Attack()
    {
        playerAnimation.SetAttack(true);
        SetDamage(this.damage);
        attackHitbox.BeginAttack();
    }

    protected virtual void SetDamage(int damage)
    {
        this.attackHitbox.Damage = damage;
    }
}