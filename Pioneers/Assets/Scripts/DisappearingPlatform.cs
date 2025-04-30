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
            // ��ֹ��� �÷��̾�� �浹�ϸ� ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }

    // �� ��ֹ��� �����ϴ� �޼���
    public void RestoreObstacle()
    {
        // ��ֹ� ���¸� ���� (Ȱ��ȭ)
        gameObject.SetActive(true);
    }
}
