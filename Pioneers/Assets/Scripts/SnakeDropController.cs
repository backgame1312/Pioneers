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
    public float targetYPosition = 2.0f; // 뱀이 내려올 y위치
    public float resetTime = 5.0f;
    public GameObject speechBubble;

    private PlayerController playerController;
    private Vector3 initialPosition;
    private bool isFalling = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        initialPosition = transform.position;  // 시작 위치 (위에 있음)
    }

    void Update()
    {
        // 플레이어가 특정 X 구간에 들어오면 떨어지기 시작
        if (!isFalling &&
            player.transform.position.x >= triggerXPosition &&
            player.transform.position.x <= triggerMaximumXPosition &&
            player.transform.position.y >= triggerYPosition)
        {
            isFalling = true;
            AudioManager.Instance.PlaySnakeAttack();

            if (speechBubble != null) speechBubble.SetActive(false);
        }

        // 떨어지는 동작
        if (isFalling && transform.position.y > targetYPosition)
        {
            transform.position += Vector3.down * snakeFallSpeed * Time.deltaTime;
        }

        // 목표 지점에 도달하면 올라가기 시작
        if (transform.position.y <= targetYPosition && isFalling)
        {
            isFalling = false;
            StartCoroutine(RiseAfterDelay(3f)); // 3초 대기 후 올라가기
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
