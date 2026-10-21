using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class DamageSender : BaseMonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigidbody;
    [SerializeField] protected CircleCollider2D collider;
    [SerializeField] protected int damage = 10;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollider();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if(damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    protected virtual void Send(DamageReceiver damageReceiver)
    {
        if(damageReceiver == null) Debug.Log("DamageReceiver is null");
        damageReceiver.Deduct(this.damage);
    }

    protected virtual void LoadRigidbody()
    {
        if(this.rigidbody != null) return;
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.rigidbody.bodyType = RigidbodyType2D.Kinematic;  
    }

    protected virtual void LoadCollider()
    {
        if(this.collider != null) return;
        this.collider = GetComponent<CircleCollider2D>();
        this.collider.enabled = false;
    }
}