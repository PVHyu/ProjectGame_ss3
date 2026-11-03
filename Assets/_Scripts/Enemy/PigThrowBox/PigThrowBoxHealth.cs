using UnityEngine;

public class PigThrowBoxHealth : DamageReceiver
{
    protected override void Start()
    {
        base.Start();
        MaxHP = PigThrowBoxCtrl.Instance.EnemyData.MaxHP;
        CurrentHP = MaxHP;
    }

    protected override void OnDead()
    {
        base.OnDead();
        PigThrowBoxCtrl.Instance.PigThrowBoxAnimation.TriggerHit();
        PigThrowBoxCtrl.Instance.PigThrowBoxAnimation.TriggerDead();
        Debug.Log(transform.name + ": OnDead", gameObject);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        PigThrowBoxCtrl.Instance.PigThrowBoxAnimation.TriggerHit();
        Debug.Log(transform.name + ": OnHurt", gameObject);
    }
}