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
    public float targetYPosition = 5.0f; // ��ǥ Y ��ġ
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
        // �÷��̾ ������ X��ǥ�� �Ѿ�� �ö󰡱� ����
        if (!isRising && player.transform.position.x >= triggerXPosition && player.transform.position.x <= triggerMaximumXPositon)
        {
            isRising = true;
            AudioManager.Instance.PlaySnakeAttack();

        }

        // �ö󰡴� ����
        if (isRising && transform.position.y < targetYPosition)
        {
            transform.position += Vector3.up * snakeSpeed * Time.deltaTime;
        }

        // ��ǥ Y�� �����ϸ� �������� ����
        if (transform.position.y >= targetYPosition && isRising)
        {
            isRising = false; // �ö󰡴� ������ �������Ƿ� isRising�� false�� ����
            StartCoroutine(DescendAfterDelay(3f)); // 3�� �� �������� ����
        }
    }

    private IEnumerator DescendAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 3�� ���

        float descentSpeed = snakeDownSpeed * Time.deltaTime; // �ӵ��� deltaTime�� �����Ͽ� �����ϰ� ����

        // �������� ����
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
