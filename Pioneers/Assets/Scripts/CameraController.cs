using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // 플레이어
    public float speed = 5.0f; // 카메라 이동 속도
    public Vector2 offset = new Vector2(-2f, 0f); // 플레이어에 대한 카메라 오프셋

    [Header("X-axis Movement Range")]
    public float minX = -10f; // 최소 X값
    public float maxX = 10f;  // 최대 X값

    [Header("Y-axis Movement Settings")]
    public float followThresholdY = 12f; // 플레이어 Y값이 이 값을 넘으면 카메라가 Y축을 따라감
    private float minY = 10; // 고정된 Y값
    private float maxY = 22;

    private float currentY; // 카메라의 현재 Y값 (부드러운 이동을 위한 변수)

    void Start()
    {
        // 카메라 Y값을 고정 위치로 초기화
        currentY = minY;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 계산
        Vector3 targetPosition = GetTargetPosition();

        // 카메라를 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // 목표 위치를 계산하는 메서드
    private Vector3 GetTargetPosition()
    {
        // 목표 X 위치: 플레이어 위치 + 오프셋, 최소/최대 X값으로 제한
        float targetX = Mathf.Clamp(target.position.x + offset.x, minX, maxX);

        // Y 값은 고정된 값 또는 플레이어 Y값이 일정 값 이상일 때 따라가도록 설정
        float targetY = (target.position.y > followThresholdY) ? target.position.y : minY;

        // 최대 Y값 제한 추가
        targetY = Mathf.Clamp(targetY, float.MinValue, maxY);

        // Y값을 부드럽게 보간
        currentY = Mathf.Lerp(currentY, targetY, speed * Time.deltaTime);

        // 최종 목표 위치 반환 (Z값은 고정)
        return new Vector3(targetX, currentY, -10f);
    }
}
