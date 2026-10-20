using UnityEngine;

public class PlayerSkills : BaseMonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerMovement();
        LoadPlayerAnimation();
    }

    protected virtual void LoadPlayerMovement()
    {
        if (playerMovement != null) return;

        playerMovement = transform.parent.GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + ": LoadPlayerMovement", gameObject);
    }

    protected virtual void LoadPlayerAnimation()
    {
        if (playerAnimation != null) return;

        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
        Debug.Log(transform.name + ": LoadPlayerAnimation", gameObject);
    }

    public void NormalAttack()
    {
        playerAnimation.SetAttack(true);
    }
}