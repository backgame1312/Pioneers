using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // �÷��̾�
    public float speed = 5.0f;
    public Vector2 offset = new Vector2(-2f, 0f); // �÷��̾� ��ġ�� ���� ī�޶� ������

    // ī�޶��� X �̵� ���� ����
    public float minX = -10f;  // �ּ� X��
    public float maxX = 10f;   // �ִ� X��

    // Y�� �̵� ���� ����
    public float followThresholdY = 12f; // �÷��̾� Y���� �� ���� ������ ī�޶� Y �̵�
    private float fixedY = 5f;    // �⺻ Y�� (���� ��ġ)

    private float currentY; // ī�޶��� ���� Y�� (�ε巯�� �̵��� ����)

    void Start()
    {
        currentY = fixedY; // ���� �� ī�޶� Y���� ���� ��ġ�� ����
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ��ǥ X ��ġ: �÷��̾� ��ġ + ������
        float targetX = target.position.x + offset.x;

        // X���� �ּ�/�ִ� ���� ���� ����
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // Y���� �⺻ ���� ��ġ����, �÷��̾� Y�� ���� �� �̻��� ���� ���󰡰� ����
        float targetY = fixedY;
        if (target.position.y > followThresholdY)
        {
            targetY = target.position.y;
        }

        // Y�� �ε巴�� �����ؼ� �̵�
        currentY = Mathf.Lerp(currentY, targetY, speed * Time.deltaTime);

        // ī�޶�� X��� Y���� �ε巴�� ���󰡰�, Z���� ����
        Vector3 targetPosition = new Vector3(targetX, currentY, -10f);

        // ī�޶�� ��� ��ǥ ��ġ�� �̵�
        transform.position = targetPosition;
    }
}
