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
    public bool useActivationY = false;   // Y�� ���� ��� ����
    public float activationY = 0f;        // Y ���ذ�
    private bool isActive = false;
    private Transform playerTransform;

    private Animator animator;

    // �ʱ� ���� ����� ����
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

        // �ʱ� ���� ����
        initialPosition = transform.position;
        initialFacingLeft = facingLeft;

        if (!useActivationX) isActive = true;
    }

    void Update()
    {
        // ���� ���� �� Ȱ��ȭ
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

    // ���� �Լ�: ������ �ʱ� ���·� �ǵ���
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
            animator.Rebind(); // �ִϸ��̼� �ʱ�ȭ
        }
    }
}
