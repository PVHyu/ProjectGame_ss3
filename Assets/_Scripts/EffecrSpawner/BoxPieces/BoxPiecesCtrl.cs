using UnityEngine;

public class BoxPiecesCtrl : BaseMonoBehaviour
{
    [SerializeField] protected BoxPieces boxPieces;
    public BoxPieces BoxPieces => boxPieces;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBox();
    }

    protected virtual void LoadBox()
    {
        if(this.boxPieces != null) return;
        this.boxPieces = GetComponent<BoxPieces>();
        Debug.Log(transform.name + ": Load BoxPieces", gameObject);
    }
}
