using System.Collections.Generic;
using UnityEngine;

public class BoxPieces : Effect
{
    [Header("References")]
    [SerializeField] private Transform model;
    [SerializeField] private List<Rigidbody2D> pieces = new List<Rigidbody2D>();

    [Header("Explosion Settings")]
    [SerializeField] private float forceXMin = -3f;
    [SerializeField] private float forceXMax = 3f;
    [SerializeField] private float forceYMin = 2.5f;
    [SerializeField] private float forceYMax = 5f;
    [SerializeField] private float torqueRange = 5f; 

    private List<Vector3> initialLocalPositions = new List<Vector3>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadPieces();
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadPieces()
    {
        if (this.pieces.Count > 0) return;
        if (this.model == null) return;

        foreach (Transform child in this.model)
        {
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null) this.pieces.Add(rb);
        }
        Debug.Log(transform.name + ": LoadPieces", gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        this.CacheInitialPositions();
    }

    private void CacheInitialPositions()
    {
        this.initialLocalPositions.Clear();
        foreach (Rigidbody2D piece in this.pieces)
        {
            this.initialLocalPositions.Add(piece.transform.localPosition);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.ExplodePieces();
    }

    public void ExplodePieces()
    {
        // Phòng hờ trường hợp list vị trí gốc chưa kịp lưu
        if (this.initialLocalPositions.Count != this.pieces.Count) this.CacheInitialPositions();

        for (int i = 0; i < this.pieces.Count; i++)
        {
            Rigidbody2D rb = this.pieces[i];
            if (rb == null) continue;

            rb.transform.localPosition = this.initialLocalPositions[i];
            rb.transform.localRotation = Quaternion.identity;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            float randomX = Random.Range(this.forceXMin, this.forceXMax);
            float randomY = Random.Range(this.forceYMin, this.forceYMax);
            Vector2 force = new Vector2(randomX, randomY);

            rb.AddForce(force, ForceMode2D.Impulse);

            float randomTorque = Random.Range(-this.torqueRange, this.torqueRange);
            rb.AddTorque(randomTorque, ForceMode2D.Impulse);
        }
    }

    public override string GetName()
    {
        return "BoxPieces";
    }
}