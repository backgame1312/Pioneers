using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    [Header("Block Rise Speed")]
    public float riseSpeed = 2f;

    [Header("Maximum Rise Height (How far up from initial position)")]
    public float maxRiseHeight = 3f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isRising = false;

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
        // ��ǥ ��ġ ��� (���� maxRiseHeight��ŭ)
        targetPosition = initialPosition + Vector3.up * maxRiseHeight;
    }

    void Update()
    {
        if (isRising)
        {
            // ���� ���� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, riseSpeed * Time.deltaTime);

            // ��ǥ ��ġ �����ϸ� ��� ����
            if (transform.position == targetPosition)
            {
                isRising = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾ ���� �����ϸ� ��� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ ������ ������ ������ ��츸 ó��
            if (collision.relativeVelocity.y <= 0)
            {
                isRising = true;
            }
        }
    }

    public void RestoreObstacle()
    {
        // �� ��ġ�� �ʱ� ��ġ�� �ǵ���
        transform.position = initialPosition;

        // ��� ���� �ʱ�ȭ
        isRising = false;
    }
}
