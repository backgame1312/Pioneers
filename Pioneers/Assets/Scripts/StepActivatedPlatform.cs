using UnityEngine;

public class StepActivatedPlatform : MonoBehaviour
{
    [Header("Movement Range (X Axis)")]
    public float minX = -5f;
    public float maxX = 5f;

    [Header("Movement Speed")]
    public float moveSpeed = 2f;

    private bool movingRight = true;
    private bool isPlayerOnPlatform = false;
    private Vector3 initialPosition;  // ���� ��ġ ����

    void Start()
    {
        initialPosition = transform.position;  // ���� ��ġ ����
    }

    void Update()
    {
        if (!isPlayerOnPlatform) return;

        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= maxX)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x <= minX)
                movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            collision.transform.SetParent(transform); // �÷��̾ �÷����� ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            collision.transform.SetParent(null); // �ٽ� ������Ŵ
        }
    }

    // ���� �Լ� �߰�
    public void RestoreObstacle()
    {
        transform.position = initialPosition;
        movingRight = true;
        isPlayerOnPlatform = false;

        Debug.Log("�÷����� �ʱ� ���·� �����Ǿ����ϴ�.");
    }
}
