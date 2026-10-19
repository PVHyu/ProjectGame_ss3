using UnityEngine;

public class PlayerMovement : BaseMonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void FixedUpdate()
    {
        Movement();
    }

    void Movement() 
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0f).normalized;

        transform.parent.position += direction * moveSpeed * Time.fixedDeltaTime;
    }
}