using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // �÷��̾�
    public float speed = 5.0f; // ī�޶� �̵� �ӵ�
    public Vector2 offset = new Vector2(-2f, 0f); // �÷��̾ ���� ī�޶� ������

    [Header("X-axis Movement Range")]
    public float minX = -10f; // �ּ� X��
    public float maxX = 10f;  // �ִ� X��

    [Header("Y-axis Movement Settings")]
    public float followThresholdY = 12f; // �÷��̾� Y���� �� ���� ������ ī�޶� Y���� ����
    private float minY = 10; // ������ Y��
    private float maxY = 22;

    private float currentY; // ī�޶��� ���� Y�� (�ε巯�� �̵��� ���� ����)

    void Start()
    {
        // ī�޶� Y���� ���� ��ġ�� �ʱ�ȭ
        currentY = minY;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ��ǥ ��ġ ���
        Vector3 targetPosition = GetTargetPosition();

        // ī�޶� �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // ��ǥ ��ġ�� ����ϴ� �޼���
    private Vector3 GetTargetPosition()
    {
        // ��ǥ X ��ġ: �÷��̾� ��ġ + ������, �ּ�/�ִ� X������ ����
        float targetX = Mathf.Clamp(target.position.x + offset.x, minX, maxX);

        // Y ���� ������ �� �Ǵ� �÷��̾� Y���� ���� �� �̻��� �� ���󰡵��� ����
        float targetY = (target.position.y > followThresholdY) ? target.position.y : minY;

        // �ִ� Y�� ���� �߰�
        targetY = Mathf.Clamp(targetY, float.MinValue, maxY);

        // Y���� �ε巴�� ����
        currentY = Mathf.Lerp(currentY, targetY, speed * Time.deltaTime);

        // ���� ��ǥ ��ġ ��ȯ (Z���� ����)
        return new Vector3(targetX, currentY, -10f);
    }
}
