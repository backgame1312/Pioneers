using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("트리거에 닿은 객체 이름: " + other.name);

        if (other.CompareTag("Player"))
        {
            // PlayerController 컴포넌트가 콜라이더에 없을 경우 부모에서 찾기
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController == null)
            {
                playerController = other.GetComponentInParent<PlayerController>();
            }

            if (playerController != null)
            {
                playerController.Die();
                Debug.Log("Die 호출 성공");
            }
            else
            {
                Debug.LogWarning("PlayerController 컴포넌트를 찾을 수 없음");
            }
        }
    }

}
