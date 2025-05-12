using UnityEngine;

public class ClearBlock : MonoBehaviour
{
    public float clearDelay = 1f; // ���� ������� ���� �ð�
    private bool isClear = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isClear)
        {
            isClear = true;
            Invoke("Disappear", clearDelay); // ���� �ð� �ڿ� ������� ����
        }
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
    }
}
