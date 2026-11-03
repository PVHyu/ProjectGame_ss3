using System.Collections;
using UnityEngine;

public class EnemyKnockback : BaseMonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] private float knockbackForceX = 2f;
    [SerializeField] private float knockbackForceY = 2f;
    [SerializeField] private float knockbackDuration = 1f;

    private Coroutine knockbackCoroutine;
    private Rigidbody2D r2d;

    public bool IsKnockback { get; private set; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (r2d == null) r2d = GetComponentInParent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 sourcePosition)
    {
        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
        knockbackCoroutine = StartCoroutine(KnockbackRoutine(sourcePosition));
    }

    private IEnumerator KnockbackRoutine(Vector2 sourcePosition)
    {
        IsKnockback = true;
        float directionX = transform.position.x < sourcePosition.x ? -1f : 1f;

        if (r2d == null)
        {
            Debug.LogError("Rigidbody2D not found on this enemy or its parents.", gameObject);
            IsKnockback = false;
            yield break;
        }

        r2d.linearVelocity = new Vector2(
            directionX * knockbackForceX,
            knockbackForceY
        );

        yield return new WaitForSeconds(knockbackDuration);

        IsKnockback = false;
        knockbackCoroutine = null;
    }
}
