# Mushroom
## 기능
플레이어가 버섯 아이템을 먹으면 일정 시간 동안 점프력이 증가한다.

## 코드
```C++
public class Mushroom : MonoBehaviour
{
    public float JumpBoost = 2f; // 점프 강화 배율
    public float boostDuration = 1f; // 버프 지속 시간
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                StartCoroutine(ApplyJumpBoost());
                Destroy(gameObject); // 아이템 제거
            }
        }
    }

    private IEnumerator ApplyJumpBoost()
    {
        playerController.jumpForce *= JumpBoost; // 점프 강화
        yield return new WaitForSeconds(boostDuration); // 지속 시간
        playerController.jumpForce /= JumpBoost; // 원래 값 복구
    }
}
```

## 주요 변수
- *JumpBoost* : 점프력을 강화시키는 배율.
- *boostDuration* : 점프력 증가 효과가 지속되는 시간이다.

## 주요 메서드
1. OnTriggerEnter2D(Collider2D collision)
    - 버섯 아이템이 플레이어와 충돌하면 PlayerController를 참조한다.
    - 점프력 증가 코루틴(ApplyJumpBoost())을 실행한 뒤, 아이템 오브젝트를 제거한다.
2. ApplyJumpBoost()
    - 플레이어의 jumpForce를 JumpBoost만큼 곱해 점프력을 증가시킨다.
    - boostDuration만큼 시간 경과 후 jumpForce를 원래 값으로 복구한다.
