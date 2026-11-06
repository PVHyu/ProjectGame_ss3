using UnityEngine;

public class PigBoxHealth : DamageReceiver
{
    protected override void Start()
    {
        base.Start();
        MaxHP = PigBoxCtrl.Instance.EnemyData.MaxHP;
        CurrentHP = MaxHP;
    }

    protected override void OnDead()
    {
        base.OnDead();
        PigBoxCtrl.Instance.PigBoxAnimation.TriggerDead();
        Debug.Log(transform.name + ": OnDead", gameObject);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        PigBoxCtrl.Instance.PigBoxAnimation.TriggerHit();
        Debug.Log(transform.name + ": OnHurt", gameObject);
    }
}