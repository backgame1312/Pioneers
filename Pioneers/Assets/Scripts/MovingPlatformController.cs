using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement Range (X Axis)")]
    public float minX = -5f;
    public float maxX = 5f;

    [Header("Movement Speed")]
    public float moveSpeed = 2f;

    [Header("Balloon Hint")]
    public GameObject balloonHint; // 말풍선 연결용
    private bool balloonHidden = false;

    private bool movingRight = true;

    void Start()
    {
        if (balloonHint == null)
        {
            balloonHint = GetComponentInChildren<SpeechBubble>(true)?.gameObject;
        }
    }
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= maxX)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x <= minX)
                movingRight = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (balloonHint != null && !balloonHidden)
        {
            balloonHint.SetActive(false);
            balloonHidden = true;
        }
    }
}
