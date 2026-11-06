using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PigBoxMovement : EnemyMovement
{
    [SerializeField] private Collider2D attackArea;
    private bool waitingAfterAttack;


    protected override void SetRunning(bool value)
    {
        PigBoxCtrl.Instance.PigBoxAnimation.SetRunning(value);
    }

    public override void Move()
    {
        if (PigBoxCtrl.Instance.PigBoxHealth.IsDead()) return;

        if (PigBoxCtrl.Instance.PigBoxAnimation.IsAttack())
        {
            PigBoxCtrl.Instance.R2d.linearVelocity = new Vector2(0f, PigBoxCtrl.Instance.R2d.linearVelocity.y);
            SetRunning(false);
            if (!waitingAfterAttack) StartCoroutine(ResumeMoveAfterAttack());
            return;
        }

        if (waitingAfterAttack) return;

        if (IsWallAhead() || IsEdgeAhead())
        {
            StartCoroutine(TurnAroundAfterPause());
            return;
        }

        FlipAttackArea();
        PigBoxCtrl.Instance.R2d.linearVelocity = new Vector2(Direction * MoveSpeed, PigBoxCtrl.Instance.R2d.linearVelocity.y);
        SetRunning(true);
    }

    private IEnumerator ResumeMoveAfterAttack()
    {
        waitingAfterAttack = true;
        yield return new WaitUntil(() => !PigBoxCtrl.Instance.PigBoxAnimation.IsAttack());
        yield return new WaitForSeconds(3f);
        waitingAfterAttack = false;
    }

    void FlipAttackArea()
    {
        if (PigBoxCtrl.Instance.PigBoxAnimation.IsFlipped) 
        {
            this.attackArea.offset = new Vector2(Mathf.Abs(this.attackArea.offset.x), this.attackArea.offset.y);
        } else {
            this.attackArea.offset = new Vector2(-Mathf.Abs(this.attackArea.offset.x), this.attackArea.offset.y);
        }
    }
}
