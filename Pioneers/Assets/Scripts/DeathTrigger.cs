using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("트리거에 닿은 객체 이름: " + other.name);

        Transform root = other.transform.root;
        if (root.CompareTag("Player"))
        {
            PlayerController playerController = root.GetComponent<PlayerController>();
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

