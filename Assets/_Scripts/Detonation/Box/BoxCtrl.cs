using UnityEngine;

public class BoxCtrl : BaseMonoBehaviour
{
    [SerializeField] private Box box;
    public Box Box => box;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBox();
        this.LoadAnimator();
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
}
