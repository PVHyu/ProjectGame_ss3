using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : BaseMonoBehaviour
{
    [SerializeField] protected CircleCollider2D circleCollider2D;
    [SerializeField] protected BoxCollider2D boxCollider2D;

    private int damage = 1;
    public int Damage { get => damage; set => damage = value; }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        DamageReceiver damageReceiver = collision.collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    protected virtual void Send(DamageReceiver damageReceiver)
    {
        if (damageReceiver == null) Debug.Log("DamageReceiver is null");
        damageReceiver.Deduct(this.damage);
    }
}