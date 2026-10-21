using UnityEngine;

public class PlayerHealth : DamageReceiver
{
    [SerializeField] private int playerMaxHP = 100;
    protected override void Start()
    {
        base.Start();
        MaxHP = playerMaxHP;
        CurrentHP = MaxHP;
    }
    protected override void OnDead()
    {
        base.OnDead();
        Debug.Log(transform.name + ": OnDead", gameObject);
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        Debug.Log(transform.name + ": OnHurt", gameObject);
    }
}