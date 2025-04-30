using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // ▼ 플레이어 리스폰 관련
    [Header("Respawn Settings")]
    public float deathY = -10.0f;                      // 이 Y값보다 아래로 떨어지면 죽음 처리
    public Vector2 respawnPosition = new Vector2(0, 0); // 죽었을 때 되돌아갈 위치
    private int deathCount = 0;                        // 죽은 횟수 카운트

    // ▼ 이동 속도 관련
    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;                    // 기본 이동 속도
    public float speedUPAmount = 10.0f;                // 속도 증가 아이템 사용 시 증가량

    // ▼ 점프 및 중력 관련
    [Header("Jump Settings")]
    public float jumpForce = 5.0f;                     // 점프 힘
    public float defaultJumpGravity = 2.0f;            // 상승 중 중력
    public float fallGravity = 4.0f;                   // 하강 중 중력

    // ▼ 아이템 오브젝트들
    [Header("Items")]
    public GameObject speedUPItem;                     // 속도 증가 아이템
    public GameObject doubleJumpItem;                  // 더블 점프 아이템
    public GameObject speedDownItem;                   // 속도 감소 아이템

    // ▼ 시작 위치
    [Header("Start Position")]
    public Vector2 startPosition = new Vector2(0, 0);  // 씬 시작 시 플레이어 위치

    // ▼ 내부 상태 변수들
    private Rigidbody2D rb;                            // 플레이어의 Rigidbody2D
    private bool isGrounded = false;                   // 땅에 닿아 있는지 여부
    private bool canDoubleJump = false;                // 더블 점프 가능 여부
    private bool isDoubleJump = false;                 // 더블 점프 기능 활성화 여부
    private bool isSpeedUP = false;                    // 현재 속도 증가 상태인지

    // ▼ 장애물 제어용 스크립트 참조
    private DisappearingPlatform disappearingPlatform;
    private AppearingPlatformManager appearingPlatformManager;
    private EagleAttackController eagleAttackController;

    void Start()
    {
        // 시작 시 위치 설정 및 컴포넌트 찾기
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;

        disappearingPlatform = FindAnyObjectByType<DisappearingPlatform>();
        appearingPlatformManager = FindAnyObjectByType<AppearingPlatformManager>();
        eagleAttackController = FindAnyObjectByType<EagleAttackController>();
    }

    void Update()
    {
        // 매 프레임 이동, 점프, 중력, 낙사 체크 처리
        HandleMovement();
        HandleJump();
        AdjustGravity();
        CheckIfFallen();
    }

    // ▼ 좌우 이동 처리
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // ▼ 점프 및 더블 점프 처리
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                isGrounded = false;
            }
            else if (isDoubleJump && canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
                isDoubleJump = false;
            }
        }
    }

    // ▼ 점프 동작 실행
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        AudioManager.Instance.PlayJump();
    }

    // ▼ 중력 조절 (상승/하강 시 다른 중력 적용)
    private void AdjustGravity()
    {
        rb.gravityScale = rb.linearVelocity.y < 0 ? fallGravity : defaultJumpGravity;
    }

    // ▼ 낙사 체크
    private void CheckIfFallen()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    // ▼ 플레이어가 죽었을 때 처리
    public void Die()
    {
        deathCount++;
        Debug.Log($"플레이어가 죽었습니다. 죽은 횟수: {deathCount}");

        // 위치 초기화 및 UI 갱신
        transform.position = respawnPosition;
        rb.linearVelocity = Vector2.zero;
        UIManager.Instance.UpdateDeathCount(deathCount);
        UIManager.Instance.OnPlayerDeath();

        // 장애물 복구 및 아이템 리셋
        RestoreObstacles();
        ResetItems();
        ResetAbilities();
    }

    // ▼ 장애물 상태 초기화
    private void RestoreObstacles()
    {
        disappearingPlatform?.RestoreObstacle();
        appearingPlatformManager?.RestoreObstacle();
        eagleAttackController?.RestoreObstacle();
    }

    // ▼ 아이템 오브젝트들 다시 활성화
    private void ResetItems()
    {
        speedUPItem.SetActive(true);
        doubleJumpItem.SetActive(true);
        speedDownItem.SetActive(true);
    }

    // ▼ 점프 능력 초기화
    private void ResetAbilities()
    {
        isDoubleJump = false;
        canDoubleJump = false;
    }

    // ▼ 땅과 닿았는지 체크
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = true;
        }
    }

    // ▼ 아이템, 목표지점 충돌 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Earthwarm":
                HandleSpeedUP();
                break;
            case "Goal":
                StartCoroutine(LoadNextSceneWithDelay());
                break;
            case "Peanut":
                EnableDoubleJump();
                break;
            case "Larva":
                StartCoroutine(ReduceSpeedTemporarily());
                break;
        }

        // 공통 처리: 아이템 비활성화 + 사운드
        if (other.CompareTag("Earthwarm") || other.CompareTag("Peanut") || other.CompareTag("Larva"))
        {
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
        }
    }

    // ▼ 속도 증가 아이템 효과
    private void HandleSpeedUP()
    {
        isSpeedUP = true;
        moveSpeed += speedUPAmount;
        StartCoroutine(SpeedUpCoroutine());
    }

    // ▼ 일정 시간 후 속도 원상 복구
    private IEnumerator SpeedUpCoroutine()
    {
        yield return new WaitForSeconds(5f);
        moveSpeed -= speedUPAmount;
        isSpeedUP = false;
    }

    // ▼ 더블 점프 기능 활성화
    private void EnableDoubleJump()
    {
        isDoubleJump = true;
    }

    // ▼ 일정 시간 동안 속도 감소
    private IEnumerator ReduceSpeedTemporarily()
    {
        float originalSpeed = moveSpeed;
        moveSpeed /= 2;
        yield return new WaitForSeconds(3f);
        moveSpeed = originalSpeed;
    }

    // ▼ 3초 후 다음 씬으로 이동
    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("1-2");
    }
}
