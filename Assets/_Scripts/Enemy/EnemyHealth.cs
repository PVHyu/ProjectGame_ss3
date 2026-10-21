using UnityEngine;

public class EnemyHealth : DamageReceiver
{
    [SerializeField] private int enemyMaxHP = 50;
    protected override void Start()
    {
        base.Start();
        MaxHP = enemyMaxHP;
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