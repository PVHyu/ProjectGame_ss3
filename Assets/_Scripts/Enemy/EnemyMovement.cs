using System.Collections;
using UnityEngine;

public abstract class EnemyMovement : BaseMonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] private float turnPauseDuration = 3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D r2d;
    [SerializeField] private Collider2D c2d;

    [Header("Raycast")]
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckOffset = 0.05f;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    
    private EnemyKnockback enemyKnockback;
    private bool isTurning;
    private int direction = -1;
    public int Direction => direction;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider2D();
        this.LoadEnemyKnockback();
        this.LoadRigidbody2D();
        this.LoadSpriteRenderer();
    }

    private void FixedUpdate()
    {
        if (enemyKnockback.IsKnockback || isTurning) return;
        Move();
    }

    protected virtual void LoadEnemyKnockback()
    {
        if (this.enemyKnockback != null) return;
        this.enemyKnockback = GetComponent<EnemyKnockback>();
        Debug.Log(transform.name + ": Load EnemyKnockback", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.r2d != null) return;
        this.r2d = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + ": Load Rigidbody2D", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = transform.root.GetComponentInChildren<SpriteRenderer>();
        Debug.Log(transform.name + ": Load SpriteRenderer", gameObject);
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this.c2d != null) return;
        this.c2d = GetComponent<BoxCollider2D>();
        Debug.Log(transform.name + ": Load BoxCollider2D", gameObject);
    }

    protected IEnumerator TurnAroundAfterPause()
    {
        isTurning = true;

        if (this.r2d != null) this.r2d.linearVelocity = Vector2.zero;
        SetRunning(false);

        yield return new WaitForSeconds(turnPauseDuration);

        direction *= -1;
        SetFacingLeft(direction < 0);

        isTurning = false;
    }

    protected bool IsWallAhead()
    {
        Vector2 rayOrigin = new Vector2(direction > 0 ? this.c2d.bounds.max.x : this.c2d.bounds.min.x, this.c2d.bounds.center.y);
        Vector2 rayDirection = direction > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, this.wallCheckDistance, this.groundLayer);

        Debug.DrawRay(rayOrigin, rayDirection * this.wallCheckDistance, Color.yellow);

        return hit.collider != null;
    }

    protected bool IsEdgeAhead()
    {
        float x = direction > 0 ? this.c2d.bounds.max.x + this.edgeCheckOffset : this.c2d.bounds.min.x - this.edgeCheckOffset;
        Vector2 rayOrigin = new Vector2(x, this.c2d.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, this.groundCheckDistance, this.groundLayer);

        Debug.DrawRay(rayOrigin, Vector2.down * this.groundCheckDistance, Color.red);
        return hit.collider == null;
    }

    private void TurnAround()
    {
        Debug.Log("TurnAround");
        direction *= -1;
        SetFacingLeft(direction < 0);
    }

    protected virtual void SetRunning(bool value) { }

    protected virtual void SetFacingLeft(bool value)
    {
        if (this.spriteRenderer != null) this.spriteRenderer.flipX = !value;
    }

    private void FacePlayer(Vector2 playerPosition)
    {
        bool playerIsOnRight = playerPosition.x > transform.position.x;
        if ((direction > 0) != playerIsOnRight) TurnAround();
    }

    public virtual void Knockback(Vector2 hammerPosition)
    {
        FacePlayer(hammerPosition);
        enemyKnockback.ApplyKnockback(hammerPosition);
    }

    public abstract void Move();
}
