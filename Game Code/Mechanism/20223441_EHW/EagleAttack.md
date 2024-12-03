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

+ 초기 위치로 이동 시 안움직여지는 부분 수정 예정
