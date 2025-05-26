using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.Log("���ÿ� Ʈ���� ���� - �÷��̾� ���");
            player.Die(); // �÷��̾� ��� ó��
        }
    }
}
