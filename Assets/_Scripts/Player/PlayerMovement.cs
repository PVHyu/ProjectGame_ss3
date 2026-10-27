using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float knockbackForceX = 2f;
    [SerializeField] private float knockbackForceY = 2f;
    [SerializeField] private float knockbackDuration = 1f;

    private float moveInput;
    private bool isKnockback;

    public bool IsGrounded { get; private set; }
    public bool IsJumping { get; private set; }

    void FixedUpdate()
    {
        Move();
        RequestFall();
    }

    public void OnGroundEnter()
    {
        IsGrounded = true;
        IsJumping = false;
        PlayerCtrl.Instance.PlayerAnimation.SetJumping(false);
        PlayerCtrl.Instance.PlayerAnimation.SetFalling(false);
    }

    public void OnGroundExit()
    {
        IsGrounded = false;
    }

    public void SetMoveInput(float value)
    {
        moveInput = value;
    }

    public void RequestJump()
    {
        if (!IsGrounded || PlayerCtrl.Instance.PlayerHealth.IsDead()) return;

        IsGrounded = false;
        IsJumping = true;

        PlayerCtrl.Instance.Rb2d.linearVelocity = new Vector2(PlayerCtrl.Instance.Rb2d.linearVelocity.x, 0f);
        PlayerCtrl.Instance.Rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        PlayerCtrl.Instance.PlayerAnimation.SetJumping(true);
        PlayerCtrl.Instance.PlayerAnimation.SetFalling(false);
    }

    private void RequestFall()
    {
        if (IsGrounded)
        {
            if (PlayerCtrl.Instance.Rb2d.linearVelocity.y <= 0.1f)
            {
                PlayerCtrl.Instance.PlayerAnimation.SetJumping(false);
                PlayerCtrl.Instance.PlayerAnimation.SetFalling(false);
            }
            return;
        }

        float velY = PlayerCtrl.Instance.Rb2d.linearVelocity.y;

        if (velY > 0.1f)
        {
            PlayerCtrl.Instance.PlayerAnimation.SetJumping(true);
            PlayerCtrl.Instance.PlayerAnimation.SetFalling(false);
        }
        else if (velY < -0.1f)
        {
            PlayerCtrl.Instance.PlayerAnimation.SetJumping(false);
            PlayerCtrl.Instance.PlayerAnimation.SetFalling(true);
        }
    }

    private void Move()
    {
        if(PlayerCtrl.Instance.PlayerHealth.IsDead()) return;
        
        if(isKnockback) return;

        PlayerCtrl.Instance.Rb2d.linearVelocity = new Vector2(moveInput * moveSpeed, PlayerCtrl.Instance.Rb2d.linearVelocity.y);

        PlayerCtrl.Instance.PlayerAnimation.SetRunning(moveInput != 0f);

        if (moveInput > 0f)
            PlayerCtrl.Instance.PlayerAnimation.SetFacingRight(true);
        else if (moveInput < 0f)
            PlayerCtrl.Instance.PlayerAnimation.SetFacingRight(false);
    }

    public void Knockback(Vector2 enemyPosition)
    {
        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(enemyPosition));
    }

    private IEnumerator KnockbackRoutine(Vector2 enemyPosition)
    {
        isKnockback = true;
        float directionX = transform.position.x < enemyPosition.x ? -1f : 1f;

        PlayerCtrl.Instance.Rb2d.linearVelocity = new Vector2(
            directionX * knockbackForceX,
            knockbackForceY
        );

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }
}