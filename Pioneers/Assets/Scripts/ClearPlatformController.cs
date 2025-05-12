using UnityEngine;

public class ClearBlock : MonoBehaviour
{
    public float clearDelay = 1f; // 블럭이 사라지는 지연 시간
    private bool isClear = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isClear)
        {
            isClear = true;
            Invoke("Disappear", clearDelay); // 일정 시간 뒤에 사라지게 예약
        }
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
    }
}
