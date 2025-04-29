using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    public GameObject player;
    public float triggerXPosition = 100.0f;
    public float eagleSpeed = 50.0f;

    public float Xposition = 121.0f;
    public float Yposition = 20.5f;

    public Vector2 targetPosition = new Vector2(100, 1);
    public Vector2 destroyPosition = new Vector2(80, 20.5f);

    private bool isChasing = false;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing && player.transform.position.x >= triggerXPosition)
        {
            isChasing = true;
            AudioManager.Instance.PlayEagleCatch();
        }

        if (isChasing)
        {
            // 1번 좌표(targetPosition)로 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, eagleSpeed * Time.deltaTime);

            // 1번 좌표에 도달하면 2번 좌표(destroyPosition)로 이동
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = destroyPosition; // 2번 좌표로 변경
            }

            // 2번 좌표에 도달하면 독수리 비활성화
            if (Vector2.Distance(transform.position, destroyPosition) < 0.1f)
            {
                gameObject.SetActive(false); // 독수리 비활성화
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            playerController.Die();
        }
    }

    public void RestoreObstacle()
    {
        transform.position = new Vector2(Xposition, Yposition);
        gameObject.SetActive(true);
        isChasing = false;
        targetPosition = new Vector2(100, 1);
    }
}
