using System.Collections;
using UnityEngine;

public class BoxPiecesDespawn : Despawn<Effect>
{
    public override void DoDespawn()
    {
        
        this.spawner.Despawn(this.parent);
    }
}