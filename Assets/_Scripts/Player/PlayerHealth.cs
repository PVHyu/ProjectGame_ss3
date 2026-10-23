using UnityEngine;

public class PlayerHealth : DamageReceiver
{
    [SerializeField] private int playerMaxHP = 3;
    protected override void Start()
    {
        base.Start();
        MaxHP = playerMaxHP;
        CurrentHP = MaxHP;
    }
    protected override void OnDead()
    {
        base.OnDead();
        PlayerCtrl.Instance.PlayerAnimation.TriggerHit();
        PlayerCtrl.Instance.PlayerAnimation.TriggerDead();
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        PlayerCtrl.Instance.PlayerAnimation.TriggerHit();
        Debug.Log(transform.name + ": OnHurt", gameObject);
    }
}