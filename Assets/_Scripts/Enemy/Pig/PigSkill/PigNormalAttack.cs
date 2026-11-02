using System;
using UnityEditor.Build;
using UnityEngine;

public class PigNormalAttack : BaseAttack
{   
    [SerializeField] private EnemySensor enemySensor;
    public EnemySensor EnemySensor => enemySensor;

    void FixedUpdate()
    {
        this.Attack();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents(); 
        this.LoadEnemySensor();
    }

    protected virtual void LoadEnemySensor()
   {
      if (enemySensor != null) return;

      enemySensor = GetComponentInParent<EnemySensor>();
      Debug.Log(transform.name + ": Load EnemySensor", gameObject);
   }

    public override void Attack()
    {
        if (!EnemySensor.IsDetectPlayer()) return;

        Debug.Log("Detected Player");
        SetDamage(PigCtrl.Instance.EnemyData.DamageSkill);
        AttackBox.BeginAttack();
    }
}