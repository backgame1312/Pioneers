using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

        // ó���� ���� ���·�
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �������� ���·� ����
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void RestoreObstacle()
    {
        // ��ġ �ʱ�ȭ
        transform.position = originalPosition;

        // �ٽ� ���� ���·� �ٲ�
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
