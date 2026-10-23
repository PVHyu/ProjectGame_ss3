using UnityEngine;

public class PigHealth : DamageReceiver
{
    protected override void Start()
    {
        base.Start();
        MaxHP = PigCtrl.Instance.EnemyData.MaxHP;
        CurrentHP = MaxHP;
    }

    protected override void OnDead()
    {
        base.OnDead();
        PigCtrl.Instance.PigAnimation.TriggerHit();
        PigCtrl.Instance.PigAnimation.TriggerDead();
        Debug.Log(transform.name + ": OnDead", gameObject);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        PigCtrl.Instance.PigAnimation.TriggerHit();
        Debug.Log(transform.name + ": OnHurt", gameObject);
    }
}