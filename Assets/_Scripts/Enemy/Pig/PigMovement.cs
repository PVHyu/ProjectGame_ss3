using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMovement : BaseMonoBehaviour
{
    [SerializeField] private float knockbackForceX = 2f;
    [SerializeField] private float knockbackForceY = 2f;
    [SerializeField] private float knockbackDuration = 1f;

    // private bool isKnockback;


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        
    }

    public void Knockback(Vector2 hammerPosition)
    {
        Debug.Log("Calling Knockback");
        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(hammerPosition));
    }

    private IEnumerator KnockbackRoutine(Vector2 hammerPosition)
    {
        // isKnockback = true;
        float directionX = transform.position.x < hammerPosition.x ? -1f : 1f;

        PigCtrl.Instance.Rb2d.linearVelocity = new Vector2(
            directionX * knockbackForceX,
            knockbackForceY
        );

        yield return new WaitForSeconds(knockbackDuration);

        // isKnockback = false;
    }
}