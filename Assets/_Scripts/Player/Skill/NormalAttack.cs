using UnityEngine;

public class NormalAttack : Skill
{
    [SerializeField] private PlayerAnimation playerAnimation;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerAnimation();
    }

    protected virtual void LoadPlayerAnimation()
    {
        if (playerAnimation != null) return;

        playerAnimation = transform.root.GetComponentInChildren<PlayerAnimation>();
        Debug.Log(transform.name + ": LoadPlayerAnimation", gameObject);
    }

    public override void Attack()
    {
        playerAnimation.SetAttack(true);
    }
}