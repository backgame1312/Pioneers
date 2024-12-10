# CheckPoint
## 기능
플레이어가 체크포인트에 도달했을 때 해당 위치를 저장하여, 리스폰 시 저장된 위치로 이동할 수 있도록 한다.

## 코드
```C++
public class CheckPoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition; // 마지막 체크포인트 위치 저장
    public static bool isCheckpoint = false;   // 체크포인트 설정 여부

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 체크포인트에 닿으면
        {
            lastCheckpointPosition = transform.position; // 현재 위치를 체크포인트로 설정
            isCheckpoint = true; // 체크포인트 설정
            // Debug.Log("체크포인트 위치: " + lastCheckpointPosition);
        }
    }
}
```

# PlayerController
## 기능
플레이어가 리스폰할 때 마지막 체크포인트로 이동하며, 체크포인트가 설정되지 않았으면 초기 위치로 리스폰한다.

## 코드
```C++
public class PlayerController : MonoBehaviour
{
    private void Respawn()
    {
        if (CheckPoint.isCheckpointSet) // 체크포인트가 설정된 경우
        {
            transform.position = CheckPoint.lastCheckpointPosition;
        }
        else // 체크포인트가 설정되지 않은 경우
        {
            transform.position = Vector3.zero; // 초기 위치로 리스폰
        }
    }
}
```

## 주요 변수
- *lastCheckpointPosition* : 마지막으로 활성화된 체크포인트의 위치를 저장한다.
- *isCheckpoint* : 체크포인트가 설정되었는지 여부를 확인한다.

## 주요 메서드
1. OnTriggerEnter2D(Collider2D other)
      - 플레이어가 체크포인트에 닿으면 lastCheckpointPosition에 현재 체크포인트 위치를 저장하고, isCheckpoint을 true로 설정한다.
2. Respawn()
      - 체크포인트가 설정되어 있다면 lastCheckpointPosition으로 이동한다.
      - 체크포인트가 설정되지 않았다면 초기 위치로 리스폰한다.
