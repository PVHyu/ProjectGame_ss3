using UnityEngine;

public class EnemyHealth : DamageReceiver
{
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