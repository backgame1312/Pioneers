using UnityEngine;

public class ClearBlock : MonoBehaviour
{
    public float clearDelay = 1f;
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
        if (isClear)
        {
            gameObject.SetActive(false);
            return;
        }

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
            Invoke(nameof(Disappear), clearDelay); // 1초 후 사라지기
        }
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
    }
}
