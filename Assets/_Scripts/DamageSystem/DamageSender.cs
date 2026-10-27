using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]

public class DamageSender : BaseMonoBehaviour
{
    [SerializeField] protected CircleCollider2D collider;
    private int damage = 1;
    public int Damage { get => damage; set => damage = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    protected virtual void Send(DamageReceiver damageReceiver)
    {
        if (damageReceiver == null) Debug.Log("DamageReceiver is null");
        damageReceiver.Deduct(this.damage);
    }

    protected virtual void LoadCollider()
    {
        if (this.collider != null) return;
        this.collider = GetComponent<CircleCollider2D>();
        this.collider.enabled = false;
    }
}