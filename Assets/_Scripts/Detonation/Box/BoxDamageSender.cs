using System.Collections;
using UnityEngine;

public class BoxDamageSender : AttackBox
{
    [SerializeField] private BoxCtrl boxCtrl;
    private DamageReceiver damageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCtrl();
    }

    void LoadBoxCtrl()
    {
        if(this.boxCtrl != null) return;
        this.boxCtrl = transform.parent.GetComponent<BoxCtrl>();
        Debug.Log(transform.name + ": Load BoxCtrl", gameObject);
    }

    public override void BeginAttack()
    {
        if (this.circleCollider2D == null) return;
        this.circleCollider2D.enabled = true;
    }

    public override void EndAttack() {}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) return; 
        boxCtrl.BoxDespawn.DoDespawn();

        // if (!collision.gameObject.CompareTag("Player")) return;

        damageReceiver = collision.collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        this.Send(damageReceiver);
 
        if (PlayerCtrl.Instance.PlayerMovement != null)
            PlayerCtrl.Instance.PlayerMovement.Knockback(transform.position);
    }
}