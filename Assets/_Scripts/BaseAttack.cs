using UnityEngine;

public abstract class BaseAttack : BaseMonoBehaviour
{
   [SerializeField] private AttackBox attackBox;
   public AttackBox AttackBox => attackBox;
   

   protected override void LoadComponents()
   {
      base.LoadComponents();
      this.LoadAttackHitbox();
   }

   protected virtual void LoadAttackHitbox()
   {
      if (attackBox != null) return;

      attackBox = GetComponent<AttackBox>();
      Debug.Log(transform.name + ": LoadAttackBox", gameObject);
   }

   public abstract void Attack();

   protected abstract void SetDamage(int damage);
}