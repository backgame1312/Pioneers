using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    [Header("Block Rise Speed")]
    public float riseSpeed = 2f;

    [Header("Maximum Rise Height (How far up from initial position)")]
    public float maxRiseHeight = 3f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isRising = false;

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
        // 목표 위치 계산 (위로 maxRiseHeight만큼)
        targetPosition = initialPosition + Vector3.up * maxRiseHeight;
    }

    void Update()
    {
        if (isRising)
        {
            // 블럭을 위로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, riseSpeed * Time.deltaTime);

            // 목표 위치 도달하면 상승 멈춤
            if (transform.position == targetPosition)
            {
                isRising = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 블럭에 착지하면 상승 시작
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어가 위에서 떨어져 착지한 경우만 처리
            if (collision.relativeVelocity.y <= 0)
            {
                isRising = true;
            }
        }
    }

    public void RestoreObstacle()
    {
        // 블럭 위치를 초기 위치로 되돌림
        transform.position = initialPosition;

        // 상승 상태 초기화
        isRising = false;
    }
}
