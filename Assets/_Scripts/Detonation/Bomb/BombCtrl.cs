using UnityEngine;

public class BombCtrl : BaseMonoBehaviour
{
    [SerializeField] private Bomb bomb;
    public Bomb Bomb => bomb;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private BombDespawn bombDespawn;
    public BombDespawn BombDespawn => bombDespawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBox();
        this.LoadAnimator();
        this.LoadDespawn();
    }

    protected virtual void LoadBox()
    {
        if(this.bomb != null) return;
        this.bomb = GetComponent<Bomb>();
        Debug.Log(transform.name + ": Load Bomb", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if(this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": Load Animator", gameObject);
    }

    protected virtual void LoadDespawn()
    {
        if(this.bombDespawn != null) return;
        this.bombDespawn = GetComponentInChildren<BombDespawn>();
        Debug.Log(transform.name + ": Load Animator", gameObject);
    }
}
