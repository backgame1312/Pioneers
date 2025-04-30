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
        // 플레이어가 특정 위치를 지나면 독수리가 추적을 시작
        if (!isChasing && player.transform.position.x >= triggerXPosition)
        {
            isChasing = true;
            AudioManager.Instance.PlayEagleCatch();
        }

        // 추적 상태일 때 독수리가 목표 위치로 이동
        if (isChasing)
        {
            // 첫 번째 목표 위치(targetPosition)로 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, eagleSpeed * Time.deltaTime);

            // 첫 번째 목표에 도달하면 두 번째 목표(destroyPosition)로 이동
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = destroyPosition; // 두 번째 목표로 변경
            }

            // 두 번째 목표에 도달하면 독수리를 비활성화
            if (Vector2.Distance(transform.position, destroyPosition) < 0.1f)
            {
                gameObject.SetActive(false); // 독수리 비활성화
            }
        }
    }

    // 플레이어와 충돌 시 독수리 비활성화 및 플레이어 사망 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chicken"))
        {
            gameObject.SetActive(false); // 독수리 비활성화
            playerController.Die(); // 플레이어 사망 처리
        }
    }

    // 독수리 복구 메서드 (초기 위치로 되돌리기)
    public void RestoreObstacle()
    {
        transform.position = new Vector2(Xposition, Yposition); // 초기 위치로 복원
        gameObject.SetActive(true); // 독수리 활성화
        isChasing = false; // 추적 상태 초기화
        targetPosition = new Vector2(100, 1); // 첫 번째 목표 위치로 설정
    }
}
