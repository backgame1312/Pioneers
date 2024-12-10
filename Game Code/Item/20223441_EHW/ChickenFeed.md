# ChickenFeed
## 기능
플레이어가 닭모이 아이템을 먹으면 닭 3마리가 등장하여 플레이어를 방해한다.

## 코드
```C++
public class ChickenFeed : MonoBehaviour
{
    public GameObject[] chickens; // 닭 배열
    public Transform player;      // 플레이어 좌표

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와 충돌
        {
            ActivateChickens(); // 닭 활성화
            Destroy(gameObject); // 아이템 제거
        }
    }

    private void ActivateChickens()
    {
        foreach (GameObject chicken in chickens)
        {
            chicken.SetActive(true); // 비활성화된 닭 활성화
            chicken.GetComponent<ChickenFollowPlayer>().player = player; // 플레이어 추적
        }
    }
}
```

# ChickenFollowPlayer
## 기능
생성된 닭들이 플레이어를 따라다니며 지정된 시간 후 사라진다.

## 코드
```C++
public class ChickenFollowPlayer : MonoBehaviour
{
    public Transform player;          // 플레이어의 좌표
    public float followSpeed = 2f;    // 추적 속도
    public float lifeTime = 5f;       // 닭이 사라지기 전 유지되는 시간

    private void OnEnable()
    {
        // 닭 활성화 되고 lifeTime 후 삭제
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (player != null)
        {
            // 플레이어를 향해 이동
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }
}
```

# PushForce
## 닭이 플레이어와 충돌했을 때 밀어내는 힘을 적용한다.

## 코드
```C++
public class PushForce : MonoBehaviour
{
    public float pushForce = 5f; // 미는힘

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와 충돌
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // 밀어내는 힘을 적용
                Vector2 pushDirection = collision.transform.position - transform.position;
                pushDirection = pushDirection.normalized;
                playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
```

## 주요 변수
- *chickens* : 닭 오브젝트 배열.
- *player* : 닭이 추적할 플레이어의 좌표이다.

- *player* : 닭이 따라다닐 대상인 플레이어의 좌표이다.
- *followSpeed* : 닭이 플레이어를 추적하는 속도이다.
- *lifeTime* : 닭이 활성화된 뒤 일정 시간이 지나면 사라지도록 설정한다.

- *pushForce* : 닭이 플레이어와 충돌했을 때 플레이어를 밀어내는 힘의 크기이다.

## 주요 메서드
1. ActivateChickens()
    - 닭 오브젝트를 활성화(SetActive(true))하고, 플레이어를 추적하도록 한다.
    - 호출 시 모든 닭이 플레이어를 따라 움직인다.

2. Update()
    - Vector3.MoveTowards()를 사용해 닭이 지속적으로 플레이어를 추적하도록 한다.
3. OnEnable()
    - 닭 오브젝트가 활성화될 때 실행한다.
    - Destroy(gameObject, lifeTime)으로 지정된 시간이 지나면 닭을 제거한다.

4. OnCollisionEnter2D(Collision2D collision)
    - 닭과 플레이어가 충돌했을 때 실행한다.
    - 닭이 플레이어를 밀어내는 방향(pushDirection)을 계산한다.
    - Rigidbody2D.AddForce()로 힘을 적용하여 플레이어를 밀어낸다.
