using System.Collections;
using UnityEngine;

public class ExplosionDespawn : Despawn<Effect>
{
    public override void DoDespawn()
    {
        this.spawner.Despawn(this.parent);
    }
}