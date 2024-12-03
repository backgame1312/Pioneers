# EagleAttack
## 기능
일정 확률로 독수리가 플레이어를 낚아채 플레이어를 초기 위치로 되돌린다.
```C++
public class EagleAttack : MonoBehaviour
{
    public float attackPercent = 0.5f; // 독수리가 플레이어를 잡을 확률
    public float delayTime = 5f; // 게임 시작 후 플레이어를 잡기까지 기다리는 시간
    public Transform player;
    public Vector2 initialPosition;
    private bool canAttack = false;

    private Rigidbody2D playerRb;

    private void Start()
    {
        initialPosition = player.position; // 초기 위치 설정
        playerRb = player.GetComponent<Rigidbody2D>();
        Invoke("EnableAttack", delayTime); // 일정 시간 후 공격 가능
    }

    private void Update()
    {
        if (canAttack && Random.value < attackPercent)
        {
            AttackPlayer();
        }
    }

    void EnableAttack()
    {
        canAttack = true;
    }

    void AttackPlayer()
    {
        playerRb.velocity = Vector2.zero; // 플레이어의 속도를 0으로 초기화 (기존 이동을 멈추게 함)
        player.position = initialPosition; // 플레이어를 초기 위치로 되돌림
    }
}
```

## 주요 변수
- *attackPercent* : 독수리가 플레이어를 낚아챌 확률을 조절한다.
- *delayTime* : 게임 시작 후 독수리가 공격 가능해지기까지의 대기 시간을 설정한다.
- *initialPosition* : 플레이어의 초기 위치를 저장한다.
- *canAttack* : 독수리가 공격 가능한 상태인지 나타내는 bool 변수이다.
  
## 주요 메서드
1. Start():
    - 초기 위치를 설정하고 delayTime 후 독수리가 공격 가능하도록 설정한다.
2. Update():
    - canAttack가 true일 때, attackPercent에 따라 독수리가 플레이어를 낚아챈다.
3. EnableAttack():
    - canAttack를 true로 설정한다.
4. AttackPlayer():
    - 플레이어의 속도를 초기화하고, 플레이어를 초기 위치로 이동시킨다.

+ 초기 위치로 이동 시 안움직여지는 부분 수정 예정
