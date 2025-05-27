using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

        // 처음엔 고정 상태로
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3f; // 기본은 1.0, 더 크게 하면 더 빠르게 떨어짐
        }
    }

    public void RestoreObstacle()
    {
        // 위치 초기화
        transform.position = originalPosition;

        // 다시 고정 상태로 바꿈
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
