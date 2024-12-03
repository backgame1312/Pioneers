# JumpClearBlock
## 기능
투명 블럭 위에 플레이어가 있을 때 일정 시간 동안 점프하지 않으면 블럭을 삭제하여 플레이어를 떨어뜨린다.

```C++
public class JumpClearBlock : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D playerRb;

    public float jumpTimeout = 3f; // 점프 안할시 시간 감소
    private bool playerOnBlock = false;
    private float lastJump = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerRb = collision.collider.GetComponent<Rigidbody2D>();
            playerOnBlock = true; // 플레이어가 블럭에 닿았을 때
        }
    }

    private void Update()
    {
        if (playerOnBlock)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 눌렀을 때
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                lastJump = 0f; // 타이머 리셋
            }
            else
            {
                lastJump += Time.deltaTime; // 점프하지 않으면 시간 경과
            }

            if (lastJump >= jumpTimeout)
            {
                Destroy(gameObject); // 시간이 지날 시 블럭 제거
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnBlock = false; // 블럭 벗어났을 때
        }
    }
}
```

## 주요 변수
- *jumpForce* : 플레이어의 점프 힘을 조절한다.
- *jumpTimeout* : 플레이어가 점프하지 않고 블럭 위에 있을 수 있는 최대 시간을 조절한다.
- *playerOnBlock* : 플레이어가 블럭 위에 있는지를 확인하는 bool 변수이다.
- *lastJump* : 마지막으로 점프한 이후 경과 시간을 기록한다.
## 주요 메서드
1. OnCollisionEnter2D(Collision2D collision)
    - 플레이어가 블럭에 올라왔을 때 호출되며, playerOnBlock을 true로 설정한다.
2. Update()
    - 플레이어가 블럭 위에 있을 때 매 프레임 호출되며, 다음을 처리한다.
        - 점프할 시 타이머를 리셋한다.
        - 점프하지 않으면 경과 시간을 누적한다.
        - 경과 시간이 jumpTimeout을 초과하면 블럭을 삭제한다.
3. OnCollisionExit2D(Collision2D collision)
    - 플레이어가 블럭에서 벗어났을 때 호출되며, playerOnBlock을 false로 설정한다.
