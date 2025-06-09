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
    public float speedChangeAmount = 10.0f;
    private float moveInput;
    private float defaultMoveSpeed;
    public float normalGravity = 8f;

    [Header("Jump Tuning")]
    public float jumpForce = 5.0f;
    public float defaultjump = 2f;
    public float jumpSpeed = 4.0f;
    public float weakenedJumpPower = 0.5f;
    private float defaultJumpForce;

    [Header("Spawn Point")]
    public Vector3 lastCheckpointPosition { get; private set; }
    public bool hasLastNest = false;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isDoubleJumpEnabled = false;
    private bool canDoubleJump = false;
    private bool isWeakenedJump = false;
    private bool isSpeedUpActive = false;
    private bool isDead = false;

    private Animator animator;
    public CharacterFaceManager faceManager;

    [Header("Gimmick Manager")]
    public ObstacleRestoreManager obstacleRestoreManager;

    private int eggCount = 0;
    public int EggCount => eggCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;

        if (faceManager != null)
            faceManager.ShowBasic();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                float jumpPower = isWeakenedJump ? jumpForce * weakenedJumpPower : jumpForce;
                isWeakenedJump = false;

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
        rb.gravityScale = rb.linearVelocity.y < 0 ? jumpSpeed : defaultjump;
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
        if (isDead) return;
        isDead = true;

        deathCount++;
        Debug.Log("플레이어가 죽었습니다. 죽은 횟수: " + deathCount);

        UIManager.Instance?.UpdateDeathCount(deathCount);
        UIManager.Instance?.OnPlayerDeath();
        GameManager.Instance?.HandlePlayerDeath(transform.position);
        AudioManager.Instance.PlayDeath();
        faceManager?.ShowFall();

        ResetToDefaultStats();
    }

    public void ResetDeathState()
    {
        isDead = false;
    }

    private void ResetToDefaultStats()
    {
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        isWeakenedJump = false;
        isSpeedUpActive = false;
        isDoubleJumpEnabled = false;
        canDoubleJump = false;
        Debug.Log("스탯 초기화 완료");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("TurnPoint"))
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
            faceManager?.ShowBuff();
        }

        if (other.CompareTag("Peanut"))
        {
            isDoubleJumpEnabled = true;
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
            faceManager?.ShowBuff();
        }

        if (other.CompareTag("Larva"))
        {
            HandleSpeedDown();
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
            faceManager?.ShowDebuff();
        }

        if (other.CompareTag("Mushroom"))
        {
            isWeakenedJump = true;
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayItemGet();
            faceManager?.ShowDebuff();
        }

        if (other.CompareTag("Egg"))
        {
            AudioManager.Instance.PlayEggGet();
            eggCount++;
            faceManager?.ShowBuff();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Egg_Obstacle")) other.gameObject.SetActive(false);

        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpointPosition = other.transform.position;
            hasLastNest = true;
        }

        if (other.CompareTag("Goal"))
        {
            StartCoroutine(LoadNextSceneWithDelay());
        }

        if (other.CompareTag("MainGoal"))
        {
            Time.timeScale = 0f;
            float time = UIManager.Instance.ElapsedTime;
            int deaths = UIManager.Instance.DeathCount;
            int eggs = eggCount;

            UIManager.Instance.ShowResult(time, deaths, eggs);
        }
    }

    private void HandleSpeedUP()
    {
        if (!isSpeedUpActive)
        {
            moveSpeed += speedChangeAmount;
            StartCoroutine(SpeedUpCoroutine());
        }
    }

    private IEnumerator SpeedUpCoroutine()
    {
        isSpeedUpActive = true;
        yield return new WaitForSeconds(5f);
        moveSpeed = defaultMoveSpeed;
        isSpeedUpActive = false;
    }

    private void HandleSpeedDown()
    {
        moveSpeed -= speedChangeAmount;
        StartCoroutine(SpeedDownCoroutine());
    }

    private IEnumerator SpeedDownCoroutine()
    {
        yield return new WaitForSeconds(5f);
        moveSpeed = defaultMoveSpeed;
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}