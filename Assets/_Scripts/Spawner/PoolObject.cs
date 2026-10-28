using UnityEngine;
using System.Collections;

public abstract class PoolObject : BaseMonoBehaviour
{
    [SerializeField] protected DespawnBase despawn;
    public DespawnBase Despawn => despawn;
    public abstract string GetName();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
    }

    protected virtual void LoadDespawn()
    {
        if(this.despawn != null) return;
        this.despawn = this.GetComponentInChildren<DespawnBase>();
    }
}