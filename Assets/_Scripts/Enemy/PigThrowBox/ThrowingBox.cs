using UnityEngine;

public class TestSpawnCopy : BaseMonoBehaviour
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

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 3f;

    private float initialSpeed;
    private float angleRad;

    protected override void OnEnable()
    {
        base.OnEnable();
        CheckDetonationSpawner();
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(SpawnBoxPieces), 3f, spawnInterval);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CancelInvoke(nameof(SpawnBoxPieces));
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

    private void SpawnBoxPieces()
    {
        if (detonationSpawner == null || detonation == null)
            return;

        CalculateInitialSpeed();

        Detonation newBox = detonationSpawner.Spawn(
            detonation,
            startPoint.position
        );

        if (newBox == null)
            return;

        newBox.gameObject.SetActive(true);

        Rigidbody2D rb = newBox.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Detonation does not have Rigidbody2D", newBox);
            return;
        }

        Vector2 velocity = new Vector2(
            initialSpeed * Mathf.Cos(angleRad),
            initialSpeed * Mathf.Sin(angleRad)
        );

        rb.linearVelocity = velocity;
    }

    private void CalculateInitialSpeed()
    {
        if (startPoint == null || endPoint == null)
            return;

        float x = endPoint.position.x - startPoint.position.x;
        float y = endPoint.position.y - startPoint.position.y;

        if (Mathf.Abs(x) < 0.001f)
        {
            Debug.LogWarning("StartPoint and EndPoint have nearly the same X position.");
            initialSpeed = 0f;
            return;
        }

        float direction = Mathf.Sign(x);
        angleRad = Mathf.Abs(angle) * Mathf.Deg2Rad * direction;

        float cosAngle = Mathf.Cos(angleRad);
        float tanAngle = Mathf.Tan(angleRad);

        float denominator = 2f * cosAngle * cosAngle * (x * tanAngle - y);

        if (Mathf.Abs(denominator) < 0.001f)
        {
            initialSpeed = 0f;
            return;
        }

        float speedSquared = gravity * x * x / denominator;

        if (speedSquared <= 0f)
        {
            Debug.LogWarning(
                "Cannot calculate a valid initial speed. Check the angle and target position."
            );

            initialSpeed = 0f;
            return;
        }

        initialSpeed = Mathf.Sqrt(speedSquared);
    }

    private Vector2 CalculateTrajectoryPosition(float time)
    {
        float direction = Mathf.Sign(endPoint.position.x - startPoint.position.x);

        float x = initialSpeed * Mathf.Cos(angleRad) * time;
        float y = initialSpeed * Mathf.Sin(angleRad) * time
                  - 0.5f * gravity * time * time;

        return (Vector2)startPoint.position
               + new Vector2(x, y);
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