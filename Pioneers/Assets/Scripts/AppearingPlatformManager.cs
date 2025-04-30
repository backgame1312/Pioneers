using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingPlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformsToActivate; // 활성화할 플랫폼들을 배열로 관리
    public float[] activationXPositions; // 각 플랫폼을 활성화할 X 좌표 배열

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Chicken"); // 플레이어를 찾기

        // 시작 시 모든 플랫폼을 비활성화
        foreach (var platform in platformsToActivate)
        {
            platform.SetActive(false);
        }
    }

    void Update()
    {
        // 각 플랫폼의 X 좌표를 기준으로 활성화 여부 체크
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            if (player.transform.position.x > activationXPositions[i] && !platformsToActivate[i].activeInHierarchy)
            {
                platformsToActivate[i].SetActive(true); // 해당 X좌표를 넘으면 플랫폼 활성화
            }
        }
    }

    // 필요한 경우, 모든 플랫폼을 다시 비활성화하는 메서드
    public void RestoreObstacle()
    {
        foreach (var platform in platformsToActivate)
        {
            platform.SetActive(false); // 모든 플랫폼 비활성화
        }
    }
}
