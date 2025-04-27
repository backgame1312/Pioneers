using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // 플레이어
    public float speed = 5.0f;
    public Vector2 offset = new Vector2(-2f, 0f); // 플레이어 위치에 대한 카메라 오프셋

    // 카메라의 X 이동 제한 범위
    public float minX = -10f;  // 최소 X값
    public float maxX = 10f;   // 최대 X값

    // Y축 이동 제한 범위
    public float followThresholdY = 12f; // 플레이어 Y값이 이 값을 넘으면 카메라 Y 이동
    private float fixedY = 5f;    // 기본 Y값 (고정 위치)

    private float currentY; // 카메라의 현재 Y값 (부드러운 이동을 위해)

    void Start()
    {
        currentY = fixedY; // 시작 시 카메라 Y값을 고정 위치로 설정
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 X 위치: 플레이어 위치 + 오프셋
        float targetX = target.position.x + offset.x;

        // X값을 최소/최대 범위 내로 제한
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // Y값은 기본 고정 위치에서, 플레이어 Y가 일정 값 이상일 때만 따라가게 설정
        float targetY = fixedY;
        if (target.position.y > followThresholdY)
        {
            targetY = target.position.y;
        }

        // Y값 부드럽게 보간해서 이동
        currentY = Mathf.Lerp(currentY, targetY, speed * Time.deltaTime);

        // 카메라는 X축과 Y축을 부드럽게 따라가고, Z축은 고정
        Vector3 targetPosition = new Vector3(targetX, currentY, -10f);

        // 카메라는 즉시 목표 위치로 이동
        transform.position = targetPosition;
    }
}
