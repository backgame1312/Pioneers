using UnityEngine;

public class FallingAttack : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallSpeed = 10f;

    [Header("Initial Position")]
    public float Xposition = 0.0f;
    public float Yposition = 0.0f;

    [Header("Trigger X Position")]
    public float triggerX = 0.0f;

    private Rigidbody2D rb;
    private bool isFalling = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 초기엔 리지드바디 비활성화
        rb.simulated = false;
        transform.position = new Vector2(Xposition, Yposition);
    }

    void Update()
    {
        if (isFalling)
        {
            // Rigidbody2D에 직접 속도 부여
            rb.linearVelocity = new Vector2(0, -fallSpeed);
        }
        else
        {
            // 떨어지는 중이 아니면 속도 0
            rb.linearVelocity = Vector2.zero;
        }
    }
    public void CheckAndStartFalling(float playerX)
    {
        if (!isFalling && playerX >= triggerX)
        {
            StartFalling();
        }
    }

    public void StartFalling()
    {
        isFalling = true;
        rb.simulated = true; // 물리 시뮬레이션 켜기
    }

    public void RestoreObstacle()
    {
        isFalling = false;
        rb.simulated = false;  // 물리 시뮬레이션 끄기
        rb.linearVelocity = Vector2.zero;
        transform.position = new Vector2(Xposition, Yposition);
    }
}
