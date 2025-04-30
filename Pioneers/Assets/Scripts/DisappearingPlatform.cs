using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private bool isActive = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chicken"))
        {
            // 장애물이 플레이어와 충돌하면 비활성화
            gameObject.SetActive(false);
        }
    }

    // ▼ 장애물을 복구하는 메서드
    public void RestoreObstacle()
    {
        // 장애물 상태를 복구 (활성화)
        gameObject.SetActive(true);
    }
}
