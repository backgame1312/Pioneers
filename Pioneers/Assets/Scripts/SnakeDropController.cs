using System.Collections;
using UnityEngine;

public class SnakeDropController : MonoBehaviour
{
    [Header("Snake Settings")]
    public GameObject player;
    public float triggerXPosition = 5.0f;
    public float triggerYPosition = 0f;
    public float triggerMaximumXPosition = 10.0f;
    public float snakeFallSpeed = 20.0f;
    public float snakeRiseSpeed = 5.0f;
    public float targetYPosition = 2.0f;
    public float resetTime = 5.0f;
    public GameObject speechBubble;

    private PlayerController playerController;
    private Vector3 initialPosition;
    private bool isFalling = false;
    private bool hasActivated = false; // 중복 방지용

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        initialPosition = transform.position;
    }

    void Update()
    {
        // 트리거 조건 + hasActivated로 중복 작동 방지
        if (!isFalling && !hasActivated &&
            player.transform.position.x >= triggerXPosition &&
            player.transform.position.x <= triggerMaximumXPosition &&
            player.transform.position.y >= triggerYPosition)
        {
            isFalling = true;
            hasActivated = true;

            AudioManager.Instance.PlaySnakeAttack();

            if (speechBubble != null) speechBubble.SetActive(false);
        }

        // 떨어지는 동작
        if (isFalling && transform.position.y > targetYPosition)
        {
            transform.position += Vector3.down * snakeFallSpeed * Time.deltaTime;
        }

        // 도달하면 올라가기
        if (transform.position.y <= targetYPosition && isFalling)
        {
            isFalling = false;
            StartCoroutine(RiseAfterDelay(3f));
        }
    }

    private IEnumerator RiseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float riseSpeed = snakeRiseSpeed * Time.deltaTime;

        while (transform.position.y < initialPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition.y, transform.position.z), riseSpeed);
            yield return null;
        }

        if (speechBubble != null)
            speechBubble.SetActive(true);

        gameObject.SetActive(false);
        Invoke("RestoreObstacle", 3f);
    }

    private void RestoreObstacle()
    {
        gameObject.SetActive(true);
        hasActivated = false; // 다시 발동 가능하도록 초기화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.Die();

            if (speechBubble != null)
                speechBubble.SetActive(true);
        }
    }
}
