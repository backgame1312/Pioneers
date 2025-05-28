using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ʈ���ſ� ���� ��ü �̸�: " + other.name);

        Transform root = other.transform.root;
        if (root.CompareTag("Player"))
        {
            PlayerController playerController = root.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Die();
                Debug.Log("Die ȣ�� ����");
            }
            else
            {
                Debug.LogWarning("PlayerController ������Ʈ�� ã�� �� ����");
            }
        }
    }
}

