using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Respawn Settings")]
    public float deathY = -10.0f;
    private int deathCount = 0;

    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    public float speedUPAmount = 200.0f;
    private float moveInput;

    [Header("Jump Tuning")]
    public float jumpForce = 5.0f;
    public float defaultjump = 2f;
    public float jumpSpeed = 4.0f;
    public float weakenedJumpPower = 0.5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isDoubleJumpEnabled = false;
    private bool canDoubleJump = false;
    private bool isWeakenedJump = false;

    [Header("Items")]
    public List<GameObject> speedUPItems;
    public List<GameObject> doubleJumpItems;
    public List<GameObject> speedDownItems;
    public List<GameObject> weakenedJumpItems;

    private DisappearingPlatformManager disappearingPlatformManager;
    private AppearingPlatformManager appearingPlatformManager;
    private FallingAttackManager fallingAttackManager;
    private JumpObstacleManager jumpObstacleManager;
    private FallingPlatform[] fallingPlatforms;
    private SpikeBlockActivator[] spikeBlockActivators;
    private RisingPlatform[] risingPlatforms;
    private EagleAttackController[] eagleAttackControllers;
    private MoleAttackController[] moleAttackControllers;

    private Animator animator;

    public CharacterFaceManager faceManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        disappearingPlatformManager = FindAnyObjectByType<DisappearingPlatformManager>();
        appearingPlatformManager = FindAnyObjectByType<AppearingPlatformManager>();
        fallingAttackManager = FindAnyObjectByType<FallingAttackManager>();
        jumpObstacleManager = FindAnyObjectByType<JumpObstacleManager>();
        fallingPlatforms = FindObjectsByType<FallingPlatform>(FindObjectsSortMode.None);
        spikeBlockActivators = FindObjectsByType<SpikeBlockActivator>(FindObjectsSortMode.None);
        risingPlatforms = FindObjectsByType<RisingPlatform>(FindObjectsSortMode.None);
        eagleAttackControllers = FindObjectsByType<EagleAttackController>(FindObjectsSortMode.None);
        moleAttackControllers = FindObjectsByType<MoleAttackController>(FindObjectsSortMode.None);

        animator = GetComponent<Animator>();

        if (faceManager != null)
        {
            faceManager.ShowBasic();
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGrounded", isGrounded);

        MovementController();
        JumpGravity();
        CheckFallDeath();

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

        if (UIManager.Instance != null) // UIManager가 null이 아닐 때만 호출
        {
            UIManager.Instance.UpdateDeathCount(deathCount);
            UIManager.Instance.OnPlayerDeath();
        }
        else
        {
            Debug.LogWarning("UIManager가 초기화되지 않았습니다.");
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.HandlePlayerDeath(transform.position);
        }

        if (faceManager != null)
        {
            faceManager.ShowFall();
        }
    }

    public void RestoreAllObstacles()
    { 

        if (fallingAttackManager != null)
        {
            fallingAttackManager.RestoreObstacles();
        }

        if (disappearingPlatformManager != null)
        {
            disappearingPlatformManager.RestoreObstacle();
        }

        if (appearingPlatformManager != null)
        {
            appearingPlatformManager.RestoreObstacle();
        }

        if (jumpObstacleManager != null)
        {
            jumpObstacleManager.RestoreObstacle();
        }

        if (fallingPlatforms != null)
        {
            foreach (var platform in fallingPlatforms)
            {
                if (platform != null)
                    platform.RestoreObstacle();
            }
        }

        if (spikeBlockActivators != null)
        {
            foreach (var spikePlatform in spikeBlockActivators)
            {
                if (spikePlatform != null)
                    spikePlatform.RestoreObstacle();
            }
        }

        if (risingPlatforms != null)
        {
            foreach (var risingPlatform in risingPlatforms)
            {
                if (risingPlatform != null)
                    risingPlatform.RestoreObstacle();
            }
        }

        if (eagleAttackControllers != null)
        {
            foreach (var eagle in eagleAttackControllers)
            {
                if (eagle != null)
                    eagle.RestoreObstacle();
            }
        }

        if (moleAttackControllers != null)
        {
            foreach (var mole in moleAttackControllers)
            {
                if (mole != null)
                    mole.RestoreObstacle();
            }
        }

        // 아이템들도 포함
        RestoreItemList(speedUPItems);
        RestoreItemList(speedDownItems);
        RestoreItemList(weakenedJumpItems);
        RestoreItemList(doubleJumpItems);

        isDoubleJumpEnabled = false;
        canDoubleJump = false;

        Debug.Log("플레이어 장애물 복원 완료");
    }
    private void RestoreItemList(List<GameObject> itemList)
    {
        if (itemList == null) return;

        foreach (GameObject item in itemList)
        {
            if (item != null)
                item.SetActive(true);
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

        if (other.CompareTag("Egg"))
        {
            Destroy(other.gameObject);  // 태그가 Egg인 오브젝트 파괴
        }
    }

    private void HandleSpeedUP()
    {
        moveSpeed += speedUPAmount;
        StartCoroutine(SpeedUpCoroutine());
    }

    private IEnumerator SpeedUpCoroutine()
    {
        yield return new WaitForSeconds(5f);
        moveSpeed -= speedUPAmount;
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
        SceneManager.LoadScene("Game");        // 3초 후에 씬 이동
    }
}
