using UnityEngine;

public class ContactDamage : BaseMonoBehaviour,IEnemySkill
{
    [SerializeField] private ContactDamageBox contactDamageBox;
    [SerializeField] private int damage = 1;

    protected void FixedUpdate()
    {
        Attack();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContactDamageBox();
    }

    protected virtual void LoadContactDamageBox()
    {
        if (this.contactDamageBox != null) return;
        this.contactDamageBox = GetComponent<ContactDamageBox>();
        Debug.Log(transform.name + ": Load Contact Damage Box", gameObject);
    }

    public void Attack()
    {
        SetDamage(this.damage);
        contactDamageBox.BeginAttack();
    }

    protected virtual void SetDamage(int damage)
    {
        this.contactDamageBox.Damage = damage;
    }
}