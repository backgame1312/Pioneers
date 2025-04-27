using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAirObstacleController : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject objectToActivate; // Ȱ��ȭ�� ������Ʈ
    public float activationXPosition = 10f; // x��ǥ ����

    void Update()
    {
        // �÷��̾��� ��ġ�� ������ x��ǥ���� Ŭ ��
        if (player.transform.position.x > activationXPosition)
        {
            if (!objectToActivate.activeInHierarchy) // ������Ʈ�� ��Ȱ��ȭ ������ ���� Ȱ��ȭ
            {
                objectToActivate.SetActive(true);
            }
        }
    }
    public void RestoreObstacle()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }
}
