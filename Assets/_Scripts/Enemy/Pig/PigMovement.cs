
using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PigMovement : BaseMonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Raycast")]
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckOffset = 0.05f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForceX = 2f;
    [SerializeField] private float knockbackForceY = 2f;
    [SerializeField] private float knockbackDuration = 1f;

    private Collider2D c2d;

    private int direction = 1;
    private bool isKnockback;
    private Coroutine knockbackCoroutine;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider2D();
    }

    private void FixedUpdate()
    {
        if (isKnockback) return;
        Move();
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
        if (IsWallAhead() || IsEdgeAhead()) TurnAround();
        PigCtrl.Instance.R2d.linearVelocity = new Vector2(direction * moveSpeed, PigCtrl.Instance.R2d.linearVelocity.y);
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
    }

    private void FacePlayer(Vector2 playerPosition)
    {
        bool playerIsOnRight = playerPosition.x > transform.position.x;

        PigCtrl.Instance.SpriteRenderer.flipX = playerIsOnRight;
    }

    public void Knockback(Vector2 hammerPosition)
    {
        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
        FacePlayer(hammerPosition);
        knockbackCoroutine = StartCoroutine(KnockbackRoutine(hammerPosition));
    }

    private IEnumerator KnockbackRoutine(Vector2 hammerPosition)
    {
        isKnockback = true;
        float directionX = transform.position.x < hammerPosition.x ? -1f : 1f;

        PigCtrl.Instance.R2d.linearVelocity = new Vector2(directionX * knockbackForceX, knockbackForceY);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
        knockbackCoroutine = null;
    }
}
