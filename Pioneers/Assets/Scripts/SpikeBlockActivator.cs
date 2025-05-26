using UnityEngine;

public class SpikeBlockActivator : MonoBehaviour
{

    // 플레이어와 충돌하면 가시 활성화
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 감지됨: " + collision.gameObject.name);

        PlayerController player = collision.collider.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.Log("가시 활성화됨");
            ActivateSpikes();
        }
    }

    // 자식 중 "Spike" 태그가 붙은 오브젝트들 활성화
    void ActivateSpikes()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Spike"))
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // 장애물 복원 함수 (가시 비활성화 + 상태 초기화)
    public void RestoreObstacle()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Spike"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
