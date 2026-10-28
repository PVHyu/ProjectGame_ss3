using UnityEngine;

public class BoxCtrl : BaseMonoBehaviour
{
    [SerializeField] protected Box box;
    public Box Box => box;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBox();
    }

    protected virtual void LoadBox()
    {
        if(this.box != null) return;
        this.box = GetComponent<Box>();
        Debug.Log(transform.name + ": Load Box", gameObject);
    }
}
