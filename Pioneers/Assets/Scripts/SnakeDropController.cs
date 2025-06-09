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
    public float targetYPosition = 2.0f; // ���� ������ y��ġ
    public float resetTime = 5.0f;
    public GameObject speechBubble;

    private PlayerController playerController;
    private Vector3 initialPosition;
    private bool isFalling = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        initialPosition = transform.position;  // ���� ��ġ (���� ����)
    }

    void Update()
    {
        // �÷��̾ Ư�� X ������ ������ �������� ����
        if (!isFalling &&
            player.transform.position.x >= triggerXPosition &&
            player.transform.position.x <= triggerMaximumXPosition &&
            player.transform.position.y >= triggerYPosition)
        {
            isFalling = true;
            AudioManager.Instance.PlaySnakeAttack();

            if (speechBubble != null) speechBubble.SetActive(false);
        }

        // �������� ����
        if (isFalling && transform.position.y > targetYPosition)
        {
            transform.position += Vector3.down * snakeFallSpeed * Time.deltaTime;
        }

        // ��ǥ ������ �����ϸ� �ö󰡱� ����
        if (transform.position.y <= targetYPosition && isFalling)
        {
            isFalling = false;
            StartCoroutine(RiseAfterDelay(3f)); // 3�� ��� �� �ö󰡱�
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
