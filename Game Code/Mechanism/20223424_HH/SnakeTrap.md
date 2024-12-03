# SnakeTrap
## 기능
이 스크립트는 플레이어가 일정 범위에 들어오면 뱀이 점프하는 트랩을 구현한 것이다. 뱀은 특정 거리 이내에서 활성화되고, 점프하면서 플레이어와 충돌하면 플레이어를 초기 위치로 되돌린다.

## 코드
```C++
public class SnakeTrap : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform player;
    public Vector3 snakeStartPoint;
    public Vector3 playerStartPoint;
    public float triggerDistance = 5.0f;
    public float snakeJumpHeight = 10.0f;
    public float snakeSpeed = 5.0f;

    private bool isTriggered = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance && !isTriggered)
        {
            isTriggered = true;
            targetPosition = new Vector3(transform.position.x, 
                initialPosition.y + snakeJumpHeight, transform.position.z);
        }

        if (isTriggered && transform.position.y < targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position,
              targetPosition, snakeSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.position = playerStartPoint;

            ResetSnake();
        }
    }

    private void ResetSnake()
    {
        isTriggered = false;
        transform.position = initialPosition;
    }
}
```

## 주요 변수
- *player* : 뱀 트랩과 상호작용할 플레이어의 `Transform`
- *snakeStartPoint* : 뱀의 초기 위치로, 플레이어와의 충돌이나 트리거 후 뱀의 위치를 리셋할 때 사용
- *playerStartPoint* : 플레이어의 초기 위치로, 플레이어가 뱀과 충돌할 경우, 플레이어를 이 위치로 되돌림
- *triggerDistance* : 플레이어가 뱀 트랩의 활성화 범위에 들어가면 뱀이 점프하도록 하는 거리
- *snakeJumpHeight* : 뱀이 점프하는 높이
- *snakeSpeed* : 뱀이 점프할 때 이동하는 속도

## 주요 메서드
1. Start()
   - 뱀의 초기 위치를 저장함
2. Update()
   - 매 프레임마다 실행되며, 플레이어와 뱀 간의 거리를 계산하여, 거리가 `triggerDistance` 이하가 되면 뱀이 점프하도록 트리거함
   - 뱀이 점프 중일 때, `snakeSpeed`에 따라 점프를 완료할 때까지 뱀을 위로 이동시킴
3. OnCollisionEnter(Collision collision)
   - 플레이어와 충돌하면, 플레이어를 초기 위치로 되돌리고 뱀의 상태를 초기화하며, 뱀의 위치도 초기화되어 점프 상태 리셋됨
4. ResetSnake()
   - 뱀의 상태를 초기화하여 점프 상태를 리셋하고, 뱀의 위치를 초기 위치로 되돌림
