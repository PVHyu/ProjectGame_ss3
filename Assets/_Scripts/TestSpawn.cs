using UnityEngine;

public class TestSpawn : BaseMonoBehaviour
{
    [SerializeField] private EffectSpawner effectSpawner;
    [SerializeField] private EffectPrefabs effectPrefabs;
    [SerializeField] private Effect effect;
    public Effect Effect => effect;

    protected override void OnEnable()
    {
        this.CheckDetonationSpawner();
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(SpawnBoxPieces), 3f, 3f);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    void CheckDetonationSpawner()
    {
        if(this.effectSpawner == null) Debug.LogWarning("EffectSpawner is null"); 
    }

    void SpawnBoxPieces()
    {
        Effect newBoxPieces = effectSpawner.Spawn(this.Effect, this.transform.position);
        newBoxPieces.gameObject.SetActive(true);
    }
}
