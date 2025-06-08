using UnityEngine;

public class RaccoonAttackController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2.0f;
    private Vector2 moveDirection = Vector2.left;
    private bool facingLeft = true;

    [Header("Flip Settings")]
    private bool canFlip = true;
    public float flipCooldown = 0.5f;

    [Header("Activation Settings")]
    public bool useActivationX = false;
    public float activationX = 0f;
    public bool useActivationY = false;   // Y값 조건 사용 여부
    public float activationY = 0f;        // Y 기준값
    private bool isActive = false;
    private Transform playerTransform;

    private Animator animator;

    // 초기 상태 저장용 변수
    private Vector3 initialPosition;
    private bool initialFacingLeft;

    void Start()
    {
        moveDirection = Vector2.left;
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        // 초기 상태 저장
        initialPosition = transform.position;
        initialFacingLeft = facingLeft;

        if (!useActivationX) isActive = true;
    }

    void Update()
    {
        // 조건 만족 시 활성화
        if (!isActive && useActivationX && playerTransform != null)
        {
            bool xCondition = playerTransform.position.x >= activationX;
            bool yCondition = !useActivationY || playerTransform.position.y <= activationY;

            if (xCondition && yCondition)
            {
                isActive = true;
            }
        }

        if (animator != null)
        {
            animator.enabled = isActive;
        }

        if (isActive)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject other)
    {
        if (other.CompareTag("TurnPoint"))
        {
            if (canFlip)
            {
                FlipDirection();
            }
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    private void FlipDirection()
    {
        moveDirection *= -1;
        facingLeft = !facingLeft;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingLeft ? 1 : -1);
        transform.localScale = scale;

        canFlip = false;
        Invoke(nameof(ResetFlip), flipCooldown);
    }

    private void ResetFlip()
    {
        canFlip = true;
    }

    // 복원 함수: 라쿤을 초기 상태로 되돌림
    public void RestoreObstacle()
    {
        transform.position = initialPosition;
        facingLeft = initialFacingLeft;
        moveDirection = facingLeft ? Vector2.left : Vector2.right;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingLeft ? 1 : -1);
        transform.localScale = scale;

        isActive = !useActivationX;
        canFlip = true;

        if (animator != null)
        {
            animator.enabled = isActive;
            animator.Rebind(); // 애니메이션 초기화
        }
    }
}
