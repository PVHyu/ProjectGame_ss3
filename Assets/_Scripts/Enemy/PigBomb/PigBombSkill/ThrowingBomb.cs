using UnityEngine;

public class ThrowingBomb : BaseMonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private DetonationSpawner detonationSpawner;
    [SerializeField] private Detonation detonation;

    public Detonation Detonation => detonation;

    [Header("Trajectory")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float angle = 45f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float trajectoryStep = 0.1f;
    [SerializeField] private int trajectoryPoints = 100;

    private float initialSpeed;
    private float angleRad;
    private float throwDirection = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Throw(endPoint);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CheckDetonationSpawner();
    }

    private void CheckDetonationSpawner()
    {
        if (detonationSpawner == null)
            Debug.LogError("DetonationSpawner is null", gameObject);

        if (detonation == null)
            Debug.LogError("Detonation is null", gameObject);

        if (startPoint == null)
            Debug.LogError("StartPoint is null", gameObject);
        
        if (endPoint == null)
            Debug.LogError("EndPoint is null", gameObject);
    }

    public void Throw(Transform target)
    {
        if (target != null) endPoint = target;
        SpawnBoxPieces();
    }

    private void SpawnBoxPieces()
    {
        CalculateInitialSpeed();

        if (initialSpeed <= 0f) return;

        Detonation newBox = detonationSpawner.Spawn(detonation, startPoint.position);

        if (newBox == null) return;
        newBox.gameObject.SetActive(true);

        Rigidbody2D rb = newBox.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        Vector2 velocity = new Vector2(initialSpeed * Mathf.Cos(angleRad) * throwDirection, initialSpeed * Mathf.Sin(angleRad));

        rb.linearVelocity = velocity;
    }

    private void CalculateInitialSpeed()
    {
        if (startPoint == null || endPoint == null)
            return;

        float x = endPoint.position.x - startPoint.position.x;
        float y = endPoint.position.y - startPoint.position.y;

        float absX = Mathf.Abs(x);
        if (absX < 0.001f)
        {
            Debug.LogWarning("StartPoint and EndPoint have nearly the same X position.");
            initialSpeed = 0f;
            return;
        }

        throwDirection = Mathf.Sign(x);
        angleRad = Mathf.Abs(angle) * Mathf.Deg2Rad;

        float cosAngle = Mathf.Cos(angleRad);
        float tanAngle = Mathf.Tan(angleRad);

        float denominator = 2f * cosAngle * cosAngle * (absX * tanAngle - y);

        if (denominator <= 0.001f)
        {
            initialSpeed = 0f;
            return;
        }

        float speedSquared = gravity * absX * absX / denominator;

        if (speedSquared <= 0f)
        {
            Debug.LogWarning("Cannot calculate a valid initial speed. Check the angle and target position.");
            initialSpeed = 0f;
            return;
        }

        initialSpeed = Mathf.Sqrt(speedSquared);
    }

    private Vector2 CalculateTrajectoryPosition(float time)
    {
        float x = initialSpeed * Mathf.Cos(angleRad) * throwDirection * time;
        float y = initialSpeed * Mathf.Sin(angleRad) * time - 0.5f * gravity * time * time;

        return (Vector2)startPoint.position + new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
        if (startPoint == null || endPoint == null)
            return;

        CalculateInitialSpeed();

        Gizmos.color = Color.red;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time1 = i * trajectoryStep;
            float time2 = (i + 1) * trajectoryStep;

            Vector2 pos1 = CalculateTrajectoryPosition(time1);
            Vector2 pos2 = CalculateTrajectoryPosition(time2);

            Gizmos.DrawLine(pos1, pos2);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint.position, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(endPoint.position, 0.1f);
    }
}