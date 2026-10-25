
using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PigMovement : BaseMonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Raycast")]
    [SerializeField] private float wallCheckDistance = 0.15f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckOffset = 0.05f;

    [Header("Knockback")]
    [SerializeField] private float knockbackForceX = 2f;
    [SerializeField] private float knockbackForceY = 2f;
    [SerializeField] private float knockbackDuration = 1f;

    private Rigidbody2D rb;
    private Collider2D col;

    // 1: sang phải, -1: sang trái
    private int direction = 1;
    private bool isKnockback;
    private Coroutine knockbackCoroutine;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (col == null)
            col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (isKnockback) return;

        if (IsWallAhead() || IsEdgeAhead()) TurnAround();

        Move();
    }

    private void LoadCollider2D()
    {
        
    }

    private void Move()
    {
        PigCtrl.Instance.Rb2d.linearVelocity = new Vector2(direction * moveSpeed, PigCtrl.Instance.Rb2d.linearVelocity.y);
    }

    private bool IsWallAhead()
    {
        Vector2 origin = new Vector2(direction > 0 ? col.bounds.max.x : col.bounds.min.x, col.bounds.center.y);
        Vector2 rayDirection = direction > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, wallCheckDistance, groundLayer);

        Debug.DrawRay(origin, rayDirection * wallCheckDistance, Color.yellow);

        return hit.collider != null;
    }

    private bool IsEdgeAhead()
    {
        float x = direction > 0 ? col.bounds.max.x + edgeCheckOffset : col.bounds.min.x - edgeCheckOffset;

        Vector2 origin = new Vector2(x, col.bounds.min.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.red);

        return hit.collider == null;
    }

    private void TurnAround()
    {
        direction *= -1;

        // Nếu Pig có SpriteRenderer, lật sprite tại đây.
        // Ví dụ: spriteRenderer.flipX = direction < 0;
    }

    private void FacePlayer(Vector2 playerPosition)
    {
        bool playerIsOnRight = playerPosition.x > transform.position.x;

        PigCtrl.Instance.SpriteRenderer.flipX = !playerIsOnRight;
    }

    public void Knockback(Vector2 hammerPosition)
    {
        StopCoroutine(knockbackCoroutine);
        FacePlayer(hammerPosition);
        knockbackCoroutine = StartCoroutine(KnockbackRoutine(hammerPosition));
    }

    private IEnumerator KnockbackRoutine(Vector2 hammerPosition)
    {
        isKnockback = true;
        float directionX = transform.position.x < hammerPosition.x ? -1f : 1f;

        PigCtrl.Instance.Rb2d.linearVelocity = new Vector2(directionX * knockbackForceX, knockbackForceY);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
        knockbackCoroutine = null;
    }
}
