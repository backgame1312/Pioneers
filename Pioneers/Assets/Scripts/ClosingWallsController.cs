using UnityEngine;

public class ClosingWallController : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Trigger X Position")]
    public float triggerX = 10f;

    [Header("Target X Position")]
    public float targetX = 0f;

    [Header("Move Speed")]
    public float moveSpeed = 3f;

    [Header("Pause Range (X Min/Max)")]
    public float pauseRangeMinX = 12f;
    public float pauseRangeMaxX = 18f;

    [Header("Pause if Player Y >")]
    public float pauseAboveY = 5f;

    private Vector3 originalPosition;
    private bool isMoving = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        if (!isMoving && player.transform.position.x >= triggerX)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            if (player.transform.position.x >= pauseRangeMinX &&
                player.transform.position.x <= pauseRangeMaxX &&
                player.transform.position.y > pauseAboveY)
            {
                return;
            }

            Vector3 pos = transform.position;
            pos.x = Mathf.MoveTowards(pos.x, targetX, moveSpeed * Time.deltaTime);
            transform.position = pos;

            // 오차 범위로 타겟 도달 판정
            if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
            {
                isMoving = false;
                transform.position = new Vector3(targetX, transform.position.y, transform.position.z); // 정확히 맞춰줌
            }
        }
    }

    public void RestoreObstacle()
    {
        transform.position = originalPosition;
        isMoving = false;
        Debug.Log("즉시 복원 완료");
    }
}
