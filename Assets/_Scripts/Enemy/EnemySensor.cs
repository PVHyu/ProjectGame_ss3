using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySensor : BaseMonoBehaviour
{
    [SerializeField] private BoxCollider2D c2d;
    [SerializeField] private float checkDistance = 0.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EnemyMovement enemyMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider2D();
        this.LoadEnemyMovement();
    }

    protected virtual void LoadCollider2D()
    {
        if (this.c2d != null) return;
        this.c2d = GetComponent<BoxCollider2D>();
        Debug.Log(transform.name + ": Load BoxCollider2D", gameObject);
    }

    protected virtual void LoadEnemyMovement()
    {
        if (this.enemyMovement != null) return;
        this.enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        Debug.Log(transform.name + ": Load EnemyMovement", gameObject);
    }

    public bool IsDetectPlayer()
    {
        Vector2 origin = new Vector2(enemyMovement.Direction > 0 ? c2d.bounds.max.x : c2d.bounds.min.x, c2d.bounds.center.y);
        Vector2 rayDirection = enemyMovement.Direction > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, checkDistance, playerLayer);

        Debug.DrawRay(origin, rayDirection * checkDistance, Color.green);

        return hit.collider != null;
    }
}
