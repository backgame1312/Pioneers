using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Respawn Settings")]
    public float deathY = -10.0f;
    private Vector2 respawnPosition = new Vector2(-5f, -2f);
    private int deathCount = 0;

    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    public float speedUPAmount = 10.0f;
    private float moveInput;

    [Header("Jump Tuning")]
    public float jumpForce = 5.0f;
    public float defaultjump = 2f;
    public float jumpSpeed = 4.0f;
    public float weakenedJumpPower = 0.5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isSpeedUP = false;
    private bool isDoubleJumpEnabled = false;
    private bool canDoubleJump = false;
    private bool isWeakenedJump = false;

    [Header("Items")]
    public GameObject speedUPItem;
    public List<GameObject> doubleJumpItems;
    public GameObject speedDownItem;
    public GameObject weakenedJumpItem;

    private DisappearingPlatform disappearingPlatform;
    private AppearingPlatformManager appearingPlatformManager;
    private EagleAttackController eagleAttackController;

    private Animator animator;

    public Vector2 startingPosition = new Vector2(-5f, -2f);

    void Start()
    {
        transform.position = startingPosition;

        rb = GetComponent<Rigidbody2D>();

        disappearingPlatform = FindAnyObjectByType<DisappearingPlatform>();
        appearingPlatformManager = FindAnyObjectByType<AppearingPlatformManager>();
        eagleAttackController = FindAnyObjectByType<EagleAttackController>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); 
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        MovementController();
        JumpGravity();
        CheckFallDeath();

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("IsGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void MovementController()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                float jumpPower = jumpForce;
                
                if (isWeakenedJump)
                {
                    jumpPower *= weakenedJumpPower;
                    isWeakenedJump = false;
                }

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                isGrounded = false;
                AudioManager.Instance.PlayJump();
            }
            else if (isDoubleJumpEnabled && canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isDoubleJumpEnabled = false;
                canDoubleJump = false;
            }
        }
    }

    private void JumpGravity()
    {
        if (rb.linearVelocity.y < 0)
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
        Debug.Log("플레이어가 죽었습니다. 죽은 횟수: " + deathCount);

        transform.position = respawnPosition;

        rb.linearVelocity = Vector2.zero;

        if (UIManager.Instance != null) // UIManager가 null이 아닐 때만 호출
        {
            UIManager.Instance.UpdateDeathCount(deathCount);
            UIManager.Instance.OnPlayerDeath();
        }
        else
        {
            Debug.LogWarning("UIManager가 초기화되지 않았습니다.");
        }

        if (speedUPItem != null) // speedUPItem이 null이 아닐 때만 호출
        {
            speedUPItem.SetActive(true);
        }
        else
        {
            Debug.LogWarning("speedUPItem이 할당되지 않았습니다.");
        }

        // 각 플랫폼 관리자가 null이 아니면 복원 호출
        if (disappearingPlatform != null)
        {
            disappearingPlatform.RestoreObstacle();
        }
        else
        {
            Debug.LogWarning("DisappearingPlatform이 할당되지 않았습니다.");
        }

        if (appearingPlatformManager != null)
        {
            appearingPlatformManager.RestoreObstacle();
        }
        else
        {
            Debug.LogWarning("AppearingPlatformManager가 할당되지 않았습니다.");
        }

        if (eagleAttackController != null)
        {
            eagleAttackController.RestoreObstacle();
        }
        else
        {
            Debug.LogWarning("EagleAttackController가 할당되지 않았습니다.");
        }

        isDoubleJumpEnabled = false;
        canDoubleJump = false;

        if (doubleJumpItems != null && doubleJumpItems.Count > 0)
        {
            foreach (GameObject peanut in doubleJumpItems)
            {
                if (peanut != null)
                    peanut.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("doubleJumpItems 리스트가 비어있거나 null입니다.");
        }

        if (speedDownItem != null) // speedDownItem이 null이 아닐 때만 호출
        {
            speedDownItem.SetActive(true);
        }
        else
        {
            Debug.LogWarning("SpeedDownItem이 할당되지 않았습니다.");
        }

        if (weakenedJumpItem != null) // weakenedJumpItem이 null이 아닐 때만 호출
        {
            weakenedJumpItem.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WeakenedJumpItem이 할당되지 않았습니다.");
        }
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
        if (other.CompareTag("Earthwarm"))
        {
            HandleSpeedUP();
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
        }

        if (other.CompareTag("Goal"))
        {
            StartCoroutine(LoadNextSceneWithDelay());
        }

        if (other.CompareTag("Peanut"))
        {
            isDoubleJumpEnabled = true;
            other.gameObject.SetActive(false); // Peanut 오브젝트 비활성화
            AudioManager.Instance.PlayItemGet();
        }

        if (other.CompareTag("Larva"))
        {
            StartCoroutine(ReduceSpeedTemporarily()); // Larva 태그와 충돌 시 속도 감소
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
        }

        if (other.CompareTag("Mushroom"))
        {
            isWeakenedJump = true;
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
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

    private IEnumerator ReduceSpeedTemporarily()
    {
        float originalSpeed = moveSpeed;
        moveSpeed /= 2; // 속도 절반으로 줄이기
        yield return new WaitForSeconds(3f); // 3초 후
        moveSpeed = originalSpeed; // 원래 속도로 복원
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(3f); // 3초 기다리고
        SceneManager.LoadScene("1-2");        // 3초 후에 씬 이동
    }
}
