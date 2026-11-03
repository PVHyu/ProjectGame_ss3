using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PigMovement : EnemyMovement
{
    protected override void SetRunning(bool value)
    {
        PigCtrl.Instance.PigAnimation.SetRunning(value);
    }

    public override void Move()
    {
        if (PigCtrl.Instance.PigHealth.IsDead()) return;
        if (IsWallAhead() || IsEdgeAhead())
        {
            StartCoroutine(TurnAroundAfterPause());
            return;
        }
        PigCtrl.Instance.R2d.linearVelocity = new Vector2(Direction * MoveSpeed, PigCtrl.Instance.R2d.linearVelocity.y);
        SetRunning(true);
    }

}
