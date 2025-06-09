using System.Collections;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Snake Settings")]
    public GameObject player;
    public float triggerXPosition = 5.0f;
    public float triggerMaximumXPosition = 10.0f;
    public float snakeSpeed = 20.0f;
    public float snakeDownSpeed = 5.0f;
    public float targetYPosition = 5.0f;
    public float resetTime = 5.0f;
    public GameObject speechBubble;

    private PlayerController playerController;
    private Vector3 initialPosition;
    private bool isRising = false;
    private bool hasActivated = false; // 중복 방지용 변수

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        initialPosition = transform.position;
    }

    void Update()
    {
        // 플레이어가 지정된 X좌표를 넘어가면 올라가기 시작 (한 번만 작동)
        if (!isRising && !hasActivated && player.transform.position.x >= triggerXPosition && player.transform.position.x <= triggerMaximumXPosition)
        {
            isRising = true;
            hasActivated = true;

            AudioManager.Instance.PlaySnakeAttack();

            if (speechBubble != null)
                speechBubble.SetActive(false);
        }

        // 올라가는 동작
        if (isRising && transform.position.y < targetYPosition)
        {
            transform.position += Vector3.up * snakeSpeed * Time.deltaTime;
        }

        // 목표 Y에 도달하면 내려가기 시작
        if (transform.position.y >= targetYPosition && isRising)
        {
            isRising = false;
            StartCoroutine(DescendAfterDelay(3f));
        }
    }

    private IEnumerator DescendAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float descentSpeed = snakeDownSpeed * Time.deltaTime;

        // 내려가는 동작
        while (transform.position.y > initialPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition.y, transform.position.z), descentSpeed);
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
        hasActivated = false; // 리셋 시 다시 발동 가능하게
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
