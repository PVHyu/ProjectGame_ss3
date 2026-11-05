using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AttackArea : BaseMonoBehaviour
{
    [SerializeField] private CircleCollider2D c2d;
    [SerializeField] private LayerMask obstacleLayer; 
    [SerializeField] private string playerLayer = "Player";

    private Transform targetPlayer;
    public Transform TargetPlayer => targetPlayer;
    private PigBoxCtrl pigThrowBoxCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider2D();
        this.LoadPigThrowBoxCtrl();
    }

     protected virtual void LoadPigThrowBoxCtrl()
    {
        if (this.pigThrowBoxCtrl != null) return;
        this.pigThrowBoxCtrl = GetComponentInParent<PigBoxCtrl>();
        Debug.Log(transform.name + ": Load PigThrowBoxCtrl", gameObject);
    }

    protected virtual void LoadCollider2D()
    {
        if (this.c2d != null) return;
        this.c2d = GetComponent<CircleCollider2D>();
        this.c2d.isTrigger = true; 
        Debug.Log(transform.name + ": Load CircleCollider2D", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayer)) 
            targetPlayer = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == targetPlayer)
            targetPlayer = null;
    }

    public bool IsDetectPlayer()
    {
        Debug.Log("Target Player: " + (targetPlayer != null ? targetPlayer.name : "None"));
        if (targetPlayer == null) return false;

        Vector2 rayOrigin = pigThrowBoxCtrl.transform.position;
        Vector2 targetPos = targetPlayer.position;
        Debug.Log("Checking if player is detectable...");
        RaycastHit2D hitObstacle = Physics2D.Linecast(rayOrigin, targetPos, obstacleLayer);

        bool canSeePlayer = (hitObstacle.collider == null);

        Debug.DrawLine(rayOrigin, targetPos, canSeePlayer ? Color.green : Color.red);

        return canSeePlayer;
    }
}