using System.Collections;
using UnityEngine;

public class BoxDespawn : Despawn<Detonation>
{
    public override void DoDespawn()
    {
        
        this.spawner.Despawn(this.parent);
    }
}