using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeObstacleController : MonoBehaviour
{
    public GameObject player;
    public float triggerXPosition = 5.0f;
    public float triggerMaximumXPositon = 10.0f;
    public float snakeSpeed = 20.0f;
    public float snakeDownSpeed = 5.0f;
    public float targetYPosition = 5.0f; // 목표 Y 위치
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
        // 플레이어가 지정된 X좌표를 넘어가면 올라가기 시작
        if (!isRising && player.transform.position.x >= triggerXPosition && player.transform.position.x <= triggerMaximumXPositon)
        {
            isRising = true;
            AudioManager.Instance.PlaySnakeAttack();

        }

        // 올라가는 동작
        if (isRising && transform.position.y < targetYPosition)
        {
            transform.position += Vector3.up * snakeSpeed * Time.deltaTime;
        }

        // 목표 Y에 도달하면 내려가기 시작
        if (transform.position.y >= targetYPosition && isRising)
        {
            isRising = false; // 올라가는 동작이 끝났으므로 isRising을 false로 설정
            StartCoroutine(DescendAfterDelay(3f)); // 3초 후 내려가기 시작
        }
    }

    private IEnumerator DescendAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 3초 대기

        float descentSpeed = snakeDownSpeed * Time.deltaTime; // 속도와 deltaTime을 결합하여 일정하게 유지

        // 내려가는 동작
        while (transform.position.y > initialPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition.y, transform.position.z), descentSpeed);
            yield return null;
        }

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
        }
    }
}
