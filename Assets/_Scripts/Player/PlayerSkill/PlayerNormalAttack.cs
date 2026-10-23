using System;
using UnityEditor.Build;
using UnityEngine;

public class PlayerNormalAttack : BaseAttack
{   
    [SerializeField] private int normalDamage = 1;

    public override void Attack()
    {
        PlayerCtrl.Instance.PlayerAnimation.SetAttack(true);
        SetDamage(normalDamage);
        AttackBox.BeginAttack();
    }

    protected override void SetDamage(int normalDamage)
    {
      this.AttackBox.Damage = normalDamage;
    }
}