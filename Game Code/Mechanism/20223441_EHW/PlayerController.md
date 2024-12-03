# PlayerController 
## 기능
플레이어 캐릭터의 이동과 점프 동작을 제어한다.

```C++
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private bool isGrounded = true;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput * moveSpeed, playerRb.velocity.y);
        playerRb.velocity = moveDirection;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (playerRb != null)
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
```

## 주요 변수
- *playerRb* : Rigidbody2D 컴포넌트를 참조하여 플레이어의 물리적 움직임을 제어한다.
- *moveSpeed* : 플레이어의 좌우 이동 속도를 설정한다.
- *jumpForce* : 플레이어의 점프 힘을 설정한다.
- *isGrounded* : 플레이어가 땅에 닿아 있는지를 판단하는 bool 변수. 점프 가능 여부를 결정한다.
  
## 주요 메서드
1. Start()
   - 스크립트가 처음 실행될 때 호출되며, playerRb를 초기화한다.
2. Update()
   - 매 프레임 호출되며, 플레이어의 움직임을 처리한다.
3. Input.GetAxis("Horizontal")
   - 키보드의 방향키 입력을 받아 플레이어의 이동 방향을 결정한다.
4. Input.GetKeyDown(KeyCode.Space)
   - 스페이스바를 눌렀을 때 점프한다.
5. OnCollisionEnter2D(Collision2D collision)
    - 플레이어가 다른 객체와 충돌했을 때 호출되며, 충돌한 객체가 "Ground" 태그를 가진 경우 isGrounded를 true로 설정한다.
