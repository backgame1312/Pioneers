using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingPlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformsToActivate; // Ȱ��ȭ�� �÷������� �迭�� ����
    public float[] activationXPositions; // �� �÷����� Ȱ��ȭ�� X ��ǥ �迭

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Chicken"); // �÷��̾ ã��

        // ���� �� ��� �÷����� ��Ȱ��ȭ
        foreach (var platform in platformsToActivate)
        {
            platform.SetActive(false);
        }
    }

    void Update()
    {
        // �� �÷����� X ��ǥ�� �������� Ȱ��ȭ ���� üũ
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            if (player.transform.position.x > activationXPositions[i] && !platformsToActivate[i].activeInHierarchy)
            {
                platformsToActivate[i].SetActive(true); // �ش� X��ǥ�� ������ �÷��� Ȱ��ȭ
            }
        }
    }

    // �ʿ��� ���, ��� �÷����� �ٽ� ��Ȱ��ȭ�ϴ� �޼���
    public void RestoreObstacle()
    {
        foreach (var platform in platformsToActivate)
        {
            platform.SetActive(false); // ��� �÷��� ��Ȱ��ȭ
        }
    }
}
