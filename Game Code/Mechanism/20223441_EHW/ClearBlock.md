# ClearBlock
## 기능
플레이어가 투명 블럭 위에 서 있을 때 일정 시간이 지나면 블럭을 삭제하여 플레이어를 떨어뜨린다.

```C++
public class ClearBlock : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D playerRb;

    public float disappearTime = 3f;
    private bool playerOnBlock = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnBlock = true;
            playerRb = collision.collider.GetComponent<Rigidbody2D>(); 
        }
    }

    private void Update()
    {
        if (playerOnBlock)
        {
            disappearTime -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && playerRb != null)
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (disappearTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnBlock = false;
        }
    }
}
```

## 주요 변수
- *jumpForce* : 플레이어의 점프 힘을 조절한다.
- *disappearTime* : 블럭이 사라지기 까지의 시간을 설정한다.
- *playerOnBlock* : 플레이어가 블럭 위에 있는지를 확인하는 bool 변수이다.
  
## 주요 메서드
1. OnCollisionEnter2D(Collision2D collision)
   - 플레이어가 블럭 위에 올라가면 호출되며, playerOnBlock를 true로 설정한다.
2. Update(): 매 프레임 호출되며, 다음을 처리한다.
   - disappearTime 감소: 시간이 줄어들며, 일정 시간이 지나면 블럭을 제거한다.
   - 점프 입력 처리: 스페이스바를 눌렀을 때 플레이어가 점프하도록 한다.
   - 블럭 삭제: disappearTime이 0 이하가 되면 블럭을 제거한다.
3. OnCollisionExit2D(Collision2D collision)
   - 플레이어가 블럭에서 벗어나면 호출되며, playerOnBlock를 false로 설정한다.
