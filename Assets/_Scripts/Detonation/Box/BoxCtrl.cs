using UnityEngine;

public class BoxCtrl : BaseMonoBehaviour
{
    [SerializeField] private Box box;
    public Box Box => box;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private BoxDespawn boxDespawn;
    public BoxDespawn BoxDespawn => boxDespawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBox();
        this.LoadAnimator();
        this.LoadDespawn();
    }

    protected virtual void LoadBox()
    {
        if(this.box != null) return;
        this.box = GetComponent<Box>();
        Debug.Log(transform.name + ": Load Box", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if(this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": Load Animator", gameObject);
    }

    protected virtual void LoadDespawn()
    {
        if(this.boxDespawn != null) return;
        this.boxDespawn = GetComponentInChildren<BoxDespawn>();
        Debug.Log(transform.name + ": Load Animator", gameObject);
    }
}
