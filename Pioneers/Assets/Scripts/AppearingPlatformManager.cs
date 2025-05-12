using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingPlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformsToActivate; // 활성화할 플랫폼들을 배열로 관리
    public float[] activationXPositions; // 각 플랫폼을 활성화할 X 좌표 배열

    private GameObject player;
    private bool[] hasActivated; // 각 플랫폼이 이미 한 번 활성화되었는지 기록

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어를 찾기

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
            // 이미 한 번 활성화한 플랫폼은 무시
            if (hasActivated[i]) continue;

            // 아직 한 번도 활성화 안 됐고, 조건 충족하면 활성화
            if (player.transform.position.x > activationXPositions[i] &&
                !platformsToActivate[i].activeInHierarchy)
            {
                platformsToActivate[i].SetActive(true);
                hasActivated[i] = true; // 활성화 기록
            }
        }
    }

    // 필요한 경우, 모든 플랫폼을 다시 비활성화하는 메서드
    public void RestoreObstacle()
    {
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            platformsToActivate[i].SetActive(false);
            hasActivated[i] = false;
        }
    }
}
