using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAirObstacleController : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트
    public GameObject objectToActivate; // 활성화할 오브젝트
    public float activationXPosition = 10f; // x좌표 기준

    void Update()
    {
        // 플레이어의 위치가 지정된 x좌표보다 클 때
        if (player.transform.position.x > activationXPosition)
        {
            if (!objectToActivate.activeInHierarchy) // 오브젝트가 비활성화 상태일 때만 활성화
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
