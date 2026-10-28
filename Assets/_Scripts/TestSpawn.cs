using UnityEngine;

public class TestSpawn : BaseMonoBehaviour
{
    [SerializeField] private DetonationSpawner detonationSpawner;
    [SerializeField] private Box box;
    public Box Box => box;

    protected override void OnEnable()
    {
        this.CheckDetonationSpawner();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    void CheckDetonationSpawner()
    {
        if(this.detonationSpawner == null) Debug.LogWarning("DetonationSpawner is null"); 
    }

    protected virtual void LoadBox()
    {
        if(this.box != null) return;
        // this.box = this.bulletPrefabs.GetByName(this.bulletName);
    }

    void SpawnBox()
    {
        // Box newBox = DetonationSpawner.Spawn(this., this.transform.position);
    }
}
