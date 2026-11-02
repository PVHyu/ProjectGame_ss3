using UnityEngine;

public class PigHealth : DamageReceiver
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
        PigThrowBoxCtrl.Instance.PigAnimation.TriggerHit();
        PigThrowBoxCtrl.Instance.PigAnimation.TriggerDead();
        Debug.Log(transform.name + ": OnDead", gameObject);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        PigThrowBoxCtrl.Instance.PigAnimation.TriggerHit();
        Debug.Log(transform.name + ": OnHurt", gameObject);
    }
}