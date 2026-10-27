using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PigMovement : BaseMonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnPauseDuration = 3f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Raycast")]
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckOffset = 0.05f;

    private Collider2D c2d;
    private EnemyKnockback enemyKnockback;
    private bool isTurning;

    private int direction = -1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider2D();
        this.LoadPigKnockback();
    }

    private void FixedUpdate()
    {
        if (enemyKnockback.IsKnockback || isTurning) return;
        Move();
    }

    protected virtual void LoadPigKnockback()
    {
        if (this.enemyKnockback != null) return;
        this.enemyKnockback = GetComponent<EnemyKnockback>();
        Debug.Log(transform.name + ": Load PigKnockback", gameObject);
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this.c2d != null) return;
        this.c2d = GetComponent<BoxCollider2D>();
        Debug.Log(transform.name + ": Load BoxCollider2D", gameObject);
    }

    private void Move()
    {
        if (PigCtrl.Instance.PigHealth.IsDead()) return;
        if (IsWallAhead() || IsEdgeAhead())
        {
            StartCoroutine(TurnAroundAfterPause());
            return;
        }
        PigCtrl.Instance.R2d.linearVelocity = new Vector2(direction * moveSpeed, PigCtrl.Instance.R2d.linearVelocity.y);
        PigCtrl.Instance.PigAnimation.SetRunning(true);
    }

    private IEnumerator TurnAroundAfterPause()
    {
        isTurning = true;

        PigCtrl.Instance.R2d.linearVelocity = Vector2.zero;
        PigCtrl.Instance.PigAnimation.SetRunning(false);

        yield return new WaitForSeconds(turnPauseDuration);

        direction *= -1;
        PigCtrl.Instance.PigAnimation.SetFacingLeft(direction < 0);

        isTurning = false;
    }

    private bool IsWallAhead()
    {
        Vector2 origin = new Vector2(direction > 0 ? c2d.bounds.max.x : c2d.bounds.min.x, c2d.bounds.center.y);
        Vector2 rayDirection = direction > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, wallCheckDistance, groundLayer);

        Debug.DrawRay(origin, rayDirection * wallCheckDistance, Color.yellow);

        return hit.collider != null;
    }

    private bool IsEdgeAhead()
    {
        float x = direction > 0 ? c2d.bounds.max.x + edgeCheckOffset : c2d.bounds.min.x - edgeCheckOffset;
        Vector2 origin = new Vector2(x, c2d.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.red);
        return hit.collider == null;
    }

    private void TurnAround()
    {
        Debug.Log("TurnAround");
        direction *= -1;
        PigCtrl.Instance.PigAnimation.SetFacingLeft(direction < 0);
    }

    private void FacePlayer(Vector2 playerPosition)
    {
        bool playerIsOnRight = playerPosition.x > transform.position.x;
        if ((direction > 0) != playerIsOnRight) TurnAround();
    }

    public void Knockback(Vector2 hammerPosition)
    {
        FacePlayer(hammerPosition);
        enemyKnockback.ApplyKnockback(hammerPosition);
    }
}
