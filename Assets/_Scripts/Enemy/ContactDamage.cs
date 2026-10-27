using UnityEngine;

public class ContactDamage : BaseAttack
{

    protected void FixedUpdate()
    {
        Attack();
    }

    public override void Attack()
    {
        SetDamage(PigCtrl.Instance.EnemyData.DamageContact);
        AttackBox.BeginAttack();
    }

    protected override void SetDamage(int damage)
    {
        this.AttackBox.Damage = damage;
    }
}