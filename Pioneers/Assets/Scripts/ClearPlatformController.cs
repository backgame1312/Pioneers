using UnityEngine;

public class ClearBlock : MonoBehaviour
{
    public float clearDelay = 1f; // 블럭이 사라지는 지연 시간
    private bool isClear = false;

    public GameObject balloonHint;

    void Start()
    {
        if (balloonHint == null) return;

        if (gameObject.activeSelf)
        {
            balloonHint.SetActive(false);
        }
        else
        {
            balloonHint.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (balloonHint != null)
        {
            balloonHint.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (balloonHint != null)
        {
            balloonHint.SetActive(true);
        }
    }

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
