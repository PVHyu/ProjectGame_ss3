using System;
using UnityEditor.Build;
using UnityEngine;

public class ExplosionAttack : BaseAttack
{   
    [SerializeField] private int normalDamage = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Attack();
    }

    public override void Attack()
    {
        SetDamage(normalDamage);
        AttackBox.BeginAttack();
    }

    protected override void SetDamage(int normalDamage)
    {
        this.AttackBox.Damage = normalDamage;
    }
}