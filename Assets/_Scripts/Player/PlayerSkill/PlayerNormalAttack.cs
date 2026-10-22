using UnityEngine;

public class PlayerNormalAttack : PlayerSkill
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private AttackHitbox attackHitbox;
    [SerializeField] private int damage = 10;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAttackHitbox();
    }

    protected virtual void LoadAttackHitbox()
    {
        if (attackHitbox != null) return;

        attackHitbox = GetComponentInParent<AttackHitbox>();
        Debug.Log(transform.name + ": LoadAttackHitbox", gameObject);
    }

    public override void Attack()
    {
        PlayerCtrl.Instance.PlayerAnimation.SetAttack(true);
        SetDamage(this.damage);
        attackHitbox.BeginAttack();
    }

    protected virtual void SetDamage(int damage)
    {
        this.attackHitbox.Damage = damage;
    }
}