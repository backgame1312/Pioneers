using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Respawn Settings")]
    public float deathY = -10.0f;
    private Vector2 respawnPosition = new Vector2(-6f, -1f);
    private int deathCount = 0;


    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    public float speedUPAmount = 10.0f;


    [Header("Jump Tuning")]
    public float jumpForce = 5.0f;
    public float defaultjump = 2f;
    public float jumpSpeed = 4.0f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isSpeedUP = false;
    private bool isDoubleJumpEnabled = false;
    private bool canDoubleJump = false;

    [Header("Items")]
    public GameObject speedUPItem;
    public GameObject doubleJump;
    public GameObject speedDownItem;

    private GroundObstacleController groundObstacleController;
    private TheAirObstacleController theAirObstacleController;
    private EagleController eagleController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        groundObstacleController = FindObjectOfType<GroundObstacleController>();
        theAirObstacleController = FindObjectOfType<TheAirObstacleController>();
        eagleController = FindObjectOfType<EagleController>();
    }

    void Update()
    {
        MovementController();
        JumpGravity();
        CheckFallDeath();
    }

    private void MovementController()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
            }
            else if (isDoubleJumpEnabled && canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isDoubleJumpEnabled = false;
                canDoubleJump = false;
            }
        }

    }

    private void JumpGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = jumpSpeed;
        }
        else
        {
            rb.gravityScale = defaultjump;
        }
    }

    private void CheckFallDeath()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    public void Die()
    {
        deathCount++;
        Debug.Log("�÷��̾ �׾����ϴ�. ���� Ƚ��: " + deathCount);

        transform.position = respawnPosition;

        rb.velocity = Vector2.zero;

        UIManager.Instance.UpdateDeathCount(deathCount);

        speedUPItem.SetActive(true);

        if (groundObstacleController != null)
        {
            groundObstacleController.RestoreObstacle();
        }

        if (theAirObstacleController != null)
        {
            theAirObstacleController.RestoreObstacle();
        }

        if (eagleController != null)
        {
            eagleController.RestoreObstacle();
        }

        isDoubleJumpEnabled = false;
        canDoubleJump = false;
        doubleJump.SetActive(true);

        speedDownItem.SetActive(true);

        UIManager.Instance.OnPlayerDeath();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedUP"))
        {
            HandleSpeedUP();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Goal"))
        {
            StartCoroutine(LoadNextSceneWithDelay());
        }

        if (other.CompareTag("Peanut"))
        {
            isDoubleJumpEnabled = true;
            other.gameObject.SetActive(false); // Peanut ������Ʈ ��Ȱ��ȭ
        }

        if (other.CompareTag("Larva"))
        {
            StartCoroutine(ReduceSpeedTemporarily()); // Larva �±׿� �浹 �� �ӵ� ����
            other.gameObject.SetActive(false);
        }
    }

    private void HandleSpeedUP()
    {
        isSpeedUP = true;
        moveSpeed += speedUPAmount;
        StartCoroutine(SpeedUpCoroutine());
    }

    private IEnumerator SpeedUpCoroutine()
    {
        yield return new WaitForSeconds(5f);
        moveSpeed -= speedUPAmount;
        isSpeedUP = false;
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(3f); // 2�� ��ٸ���
        SceneManager.LoadScene("1-2");        // 2�� �Ŀ� �� �̵�
    }

    private IEnumerator ReduceSpeedTemporarily()
    {
        float originalSpeed = moveSpeed;
        moveSpeed /= 2; // �ӵ� �������� ���̱�
        yield return new WaitForSeconds(3f); // 3�� ��
        moveSpeed = originalSpeed; // ���� �ӵ��� ����
    }
}
