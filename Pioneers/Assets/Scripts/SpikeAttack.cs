using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.Log("가시에 트리거 감지 - 플레이어 사망");
            player.Die(); // 플레이어 사망 처리
        }
    }
}
