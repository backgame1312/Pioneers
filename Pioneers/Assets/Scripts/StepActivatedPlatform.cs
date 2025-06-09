using UnityEngine;

public class StepActivatedPlatform : MonoBehaviour
{
    [Header("Movement Range (X Axis)")]
    public float minX = -5f;
    public float maxX = 5f;

    [Header("Movement Speed")]
    public float moveSpeed = 2f;

    private bool movingRight = true;
    private bool isPlayerOnPlatform = false;
    private Vector3 initialPosition;  // 시작 위치 저장

    void Start()
    {
        initialPosition = transform.position;  // 시작 위치 저장
    }

    void Update()
    {
        if (!isPlayerOnPlatform) return;

        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= maxX)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x <= minX)
                movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            collision.transform.SetParent(transform); // 플레이어를 플랫폼에 붙임
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            collision.transform.SetParent(null); // 다시 독립시킴
        }
    }

    // 복원 함수 추가
    public void RestoreObstacle()
    {
        transform.position = initialPosition;
        movingRight = true;
        isPlayerOnPlatform = false;

        Debug.Log("플랫폼이 초기 상태로 복원되었습니다.");
    }
}
