using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingPlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformsToActivate; // Ȱ��ȭ�� �÷������� �迭�� ����
    public float[] activationXPositions; // �� �÷����� Ȱ��ȭ�� X ��ǥ �迭

    private GameObject player;
    private bool[] hasActivated; // �� �÷����� �̹� �� �� Ȱ��ȭ�Ǿ����� ���

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾ ã��

        hasActivated = new bool[platformsToActivate.Length];
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            platformsToActivate[i].SetActive(false);
            hasActivated[i] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            // �̹� �� �� Ȱ��ȭ�� �÷����� ����
            if (hasActivated[i]) continue;

            // ���� �� ���� Ȱ��ȭ �� �ư�, ���� �����ϸ� Ȱ��ȭ
            if (player.transform.position.x > activationXPositions[i] &&
                !platformsToActivate[i].activeInHierarchy)
            {
                platformsToActivate[i].SetActive(true);
                hasActivated[i] = true; // Ȱ��ȭ ���
            }
        }
    }

    // �ʿ��� ���, ��� �÷����� �ٽ� ��Ȱ��ȭ�ϴ� �޼���
    public void RestoreObstacle()
    {
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            platformsToActivate[i].SetActive(false);
            hasActivated[i] = false;
        }
    }
}
