# HurdleController
## 기능
이 스크립트는 장애물이 플레이어의 점프를 감지하고, 일정 조건이 만족되면 장애물이 지정된 거리만큼 이동하도록 제어한다. ``Update()``메서드는 매 프레임마다 장애물의 상태를 확인하고 필요한 경우 이동을 처리한다.

## 코드
```C++
public class HurdleController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform player;
    public float hurdleSpeed = 5.0f;
    public float distanceHurdle = 3.0f;

    private bool isPlayerJumping = false;
    private bool shouldMove = false;
    private bool hasMoved = false;
    private float targetXPosition;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (isPlayerJumping && distance <= distanceHurdle && !shouldMove && !hasMoved)
        {
            shouldMove = true;
            targetXPosition = transform.position.x + 3f;
        }

        if (shouldMove)
        {
            if (transform.position.x < targetXPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    new Vector3(targetXPosition, transform.position.y, transform.position.z),
                    hurdleSpeed * Time.deltaTime);
            }
            else
            {
                shouldMove = false;
                hasMoved = true;
            }
        }
    }

    public void SetPlayerJumping(bool isJumping)
    {
        isPlayerJumping = isJumping;
    }
}
```

## 주요 변수
- player : 플레이어의 ``Transform``을 참조하며, 장애물은 이 객체와의 거리 관계를 기준으로 이동 여부 결정
- hurdleSpeed : 장애물이 이동하는 속도 설정
- distanceHurdle : 플레이어와 장애물 간의 거리가 특정 값 이하일 때, 장애물이 이동을 시작하도록 설정
- isPlayerJumping : 플레이어가 점프 중인지 여부를 나타내는 변수로, 플레이어의 점프 상태에 따라 장애물 이동 여부 결정
- shouldMove : 장애물이 이동할 준비가 되었는지 추적하는 플래그 변수
- hasMoved : 장애물이 이미 이동했는지 여부를 추적하여 중복 이동 방지
- targetXPosition : 장애물이 목표로 이동할 X 좌표 저장

## 주요 메서드
1. Update()
   - 매 프레임마다 호출되어, 플레이어와 장애물 간의 거리를 계산하고, 플레이어가 점프 중이고 장애물이 이동할 조건을 만족하면 장애물을 이동시킴
   - 이동은 ``Vector3.MoveTowards()``를 사용하여 목표 위치에 도달할 때까지 진행
   - 목표 위치에 도달하면, 더 이상이동하지 않도록 ``shouldMove``와 ``hasMoved``값을 업데이트
2. SetPlayerJumping(bool isJumping)
   - 외부에서 호출되어 플레이어의 점프상태(``isJumping``)를 설정하는 메서드
   - 이 값에 따라 장애물이 이동할지 말지 정
