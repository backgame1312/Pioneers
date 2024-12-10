# Earthworm
## 기능
플레이어가 지렁이 아이템을 먹으면 일정 시간 동안 이동 속도가 증가한다.

## 코드
```C++
public class Earthworm : MonoBehaviour
{
    public float speedBoost = 3f; // 속도 증가량
    public float boostDuration = 3f; // 버프 지속 시간
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                StartCoroutine(SpeedBoost());
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator SpeedBoost()
    {
        playerController.moveSpeed += speedBoost; // 속도 증가
        yield return new WaitForSeconds(boostDuration); // 지속 시간
        playerController.moveSpeed -= speedBoost; // 원래 속도로 복구
    }
}
```

## 주요 변수
- *speedBoost* : 플레이어 이동 속도 증가량.
- *boostDuration* : 이동 속도 증가 효과가 지속되는 시간이다.

## 주요 메서드
1. OnTriggerEnter2D(Collider2D collision)
    - 지렁이 아이템이 플레이어와 충돌하면 PlayerController를 참조한다.
    - 이동 속도 증가 코루틴(SpeedBoost())을 시작한 후, 아이템 오브젝트를 제거한다.
2. SpeedBoost()
    - 플레이어 이동 속도를 speedBoost만큼 증가시킨 뒤, boostDuration 동안 효과를 유지한다.
    - 시간이 지나면 원래 이동 속도로 복구된다.
