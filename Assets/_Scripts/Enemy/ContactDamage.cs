using UnityEngine;

public class ContactDamage : BaseAttack
{
    [SerializeField] private int damageContact = 1;

    protected void FixedUpdate()
    {
        Attack();
    }

    public override void Attack()
    {
        SetDamage(this.damageContact);
        AttackBox.BeginAttack();
    }

    protected override void SetDamage(int damage)
    {
        this.AttackBox.Damage = damage;
    }
}