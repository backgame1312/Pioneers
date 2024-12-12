# 돌진 닭 이벤트
## 기능
- 플레이어가 특정 위치에 도달하면 돌진 닭이 목표 지점을 향해 돌진한다.
- 돌진 닭은 목표 지점에 도달하거나 플레이어와 충돌하면 사라진다

## 코드
`PlayerController` 스크립트에서 돌진 닭 이벤트와 관련된 코드 추출
```C++
public EnemyController enemyController;

if (transform.position.x >= 130 && enemyController != null && !enemyController.isEnemyMoving)
{
    enemyController.TriggerRush();
}

private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ResetPlayer();
        }
    }
```
`EnemyController` 스크립트
```C++
private Rigidbody enemyRb;

[Header("Set in Inspector")]
public float rushSpeed = 10f;
public Vector3 targetPosition = new Vector3(100, 0, 0);

public bool isEnemyMoving = false;

void Start()
{
    enemyRb = GetComponent<Rigidbody>();
}

private void FixedUpdate()
{
    if (isEnemyMoving)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        enemyRb.MovePosition(transform.position + direction * rushSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= 1f)
        {
            Destroy(gameObject);
        }
    }
}

public void TriggerRush()
{
    Debug.Log("Enemy is rushing!");
    isEnemyMoving = true;
}
```

## 주요 변수
- *rushSpeed* : 적이 돌진할 때의 속도
- *targetPosition* : 적이 돌진하는 목표 지점
- *isEnemyMoving* : 적이 돌진 중인지 확인하는 플래그

## 주요 로직
### EnemyController
1. 적의 이동
   - `isEnemyMoving`가 `true`일 때, 목표 지점을 향해 적이 이동
   - `Vector3.Distance`를 사용해 목표 지점과의 거리 계산
   - 목표 지점에 도착하면 적 삭제
2. 돌진 시작 트릭
   - `TriggerRush()` 메서드를 호출해 `isEnemyMoving`을 `true`로 설정
   - 디버그 메시지로 돌진 시작을 알림

### PlayerController
1. 적 돌진 조건
   - 플레이어의 X 좌표가 130 이상이고, 적이 아직 돌진 중이 아니면 `TriggerRush()` 호출
2. 적과 충돌 처리
   - 플레이어가 적과 출돌하면 `ResetPlayer()` 메서드 호출로 플레이어 호출
