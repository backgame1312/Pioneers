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

    private PlayerController playerController;
    private Vector3 initialPosition;
    private bool isRising = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (IsPlayerInTriggerRange() && !isRising)
        {
            StartRising();
        }

        if (isRising)
        {
            RiseTowardsTargetY();
        }

        if (HasReachedTargetY() && isRising)
        {
            StartDescendingAfterDelay();
        }
    }

    private bool IsPlayerInTriggerRange()
    {
        return player.transform.position.x >= triggerXPosition && player.transform.position.x <= triggerMaximumXPosition;
    }

    private void StartRising()
    {
        isRising = true;
        AudioManager.Instance.PlaySnakeAttack();
    }

    private void RiseTowardsTargetY()
    {
        if (transform.position.y < targetYPosition)
        {
            transform.position += Vector3.up * snakeSpeed * Time.deltaTime;
        }
    }

    private bool HasReachedTargetY()
    {
        return transform.position.y >= targetYPosition;
    }

    private void StartDescendingAfterDelay()
    {
        isRising = false;
        StartCoroutine(DescendAfterDelay(3f));
    }

    private IEnumerator DescendAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 내려가기
        while (transform.position.y > initialPosition.y)
        {
            float descentSpeed = snakeDownSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition.y, transform.position.z), descentSpeed);
            yield return null;
        }

        DeactivateSnake();
    }

    private void DeactivateSnake()
    {
        gameObject.SetActive(false);
        Invoke(nameof(RestoreObstacle), resetTime);
    }

    private void RestoreObstacle()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chicken"))
        {
            playerController.Die();
        }
    }
}
