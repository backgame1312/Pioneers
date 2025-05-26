using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ʈ���ſ� ���� ��ü �̸�: " + other.name);

        if (other.CompareTag("Player"))
        {
            // PlayerController ������Ʈ�� �ݶ��̴��� ���� ��� �θ𿡼� ã��
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController == null)
            {
                playerController = other.GetComponentInParent<PlayerController>();
            }

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
