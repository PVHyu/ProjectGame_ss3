using UnityEngine;

public class TestSpawn : BaseMonoBehaviour
{
    [SerializeField] private DetonationSpawner detonationSpawner;
    [SerializeField] private Detonation detonation;
    public Detonation Detonation => detonation;

    protected override void OnEnable()
    {
        this.CheckDetonationSpawner();
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(SpawnBoxPieces), 3f, 3f);
    }

    void CheckDetonationSpawner()
    {
        if(this.detonationSpawner == null) Debug.LogError("DetonationSpawner is null"); 
    }

    void SpawnBoxPieces()
    {
        Detonation newBox = detonationSpawner.Spawn(this.detonation, this.transform.position);
        newBox.gameObject.SetActive(true);
    }
}
