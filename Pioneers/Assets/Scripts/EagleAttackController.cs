using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttackController : MonoBehaviour
{
    [Header("Eagle Settings")]
    public GameObject player; // 플레이어
    public float triggerXPosition = 100.0f; // 독수리가 추적을 시작하는 X값
    public float eagleSpeed = 50.0f; // 독수리 속도

    [Header("Initial Position")]
    public float Xposition = 121.0f; // 독수리의 초기 X값
    public float Yposition = 20.5f; // 독수리의 초기 Y값

    [Header("Movement Target Positions")]
    public Vector2 targetPosition = new Vector2(100, 1); // 독수리가 이동할 첫 번째 목표 위치
    public Vector2 destroyPosition = new Vector2(80, 20.5f); // 독수리가 이동할 두 번째 목표 위치

    private bool isChasing = false; // 추적 상태 확인

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
        Debug.Log("Eagle 복원됨");
        isChasing = false;
        targetPosition = new Vector2(70, 2);
        transform.position = new Vector2(Xposition, Yposition);
        gameObject.SetActive(true);
    }
}
