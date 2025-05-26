using UnityEngine;

public class SpikeBlockActivator : MonoBehaviour
{

    // �÷��̾�� �浹�ϸ� ���� Ȱ��ȭ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�浹 ������: " + collision.gameObject.name);

        PlayerController player = collision.collider.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.Log("���� Ȱ��ȭ��");
            ActivateSpikes();
        }
    }

    // �ڽ� �� "Spike" �±װ� ���� ������Ʈ�� Ȱ��ȭ
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

    // ��ֹ� ���� �Լ� (���� ��Ȱ��ȭ + ���� �ʱ�ȭ)
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
