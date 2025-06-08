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
    private bool hasBeenTriggered = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.simulated = false;
        Xposition = transform.position.x;
        Yposition = transform.position.y;
    }

    void Update()
    {
        if (isFalling)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void CheckAndStartFalling(float playerX)
    {
        if (!hasBeenTriggered && playerX >= triggerX)
        {
            hasBeenTriggered = true;
            StartFalling();
        }
    }

    public void StartFalling()
    {
        isFalling = true;
        rb.simulated = true;
    }

    public void RestoreObstacle()
    {
        isFalling = false;
        hasBeenTriggered = false;
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
        transform.position = new Vector2(Xposition, Yposition);
    }
}
