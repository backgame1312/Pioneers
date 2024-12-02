# PlayerController
## 기능
이 스크립트는 플레이어의 이동과 점프를 제어하며, 장애물과의 충돌 시 플레이어를 리셋하는 역할을 한다. 또한, 플레이어가 점프할 때 장애물의 상태를 업데이트하고, 일정 조건에서 플레이어를 초기 위치로 리셋한다.

## 코드
```C++
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 startPoint;

    [Header("Set in Inspector")]
    public float jumpForce = 5.0f;
    public float moveSpeed = 5.0f;

    [Header("Set Dynamically")]
    public bool isOnGround = true;

    public HurdleController hurdleController;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startPoint = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            if (hurdleController != null)
            {
                hurdleController.SetPlayerJumping(true);
            }
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(0, 0, horizontalInput);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (transform.position.y < -15)
        {
            ResetPlayer();
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hurdle"))
        {
            if (hurdleController != null)
            {
                hurdleController.SetPlayerJumping(false); 
            }

            ResetPlayer();  
        }
        else
        {
            isOnGround = true;
        }
    }

    private void ResetPlayer()
    {
        transform.position = startPoint;
        playerRb.velocity = Vector3.zero;
    }    
}
```

## 주요 변수
- *playerRb* : 플레이어의 ``Rigidbody`` 컴포넌트를 참조하는 변수이며, 물리적 이동과 점프를 처리하는 데 사용
- *startPoint* : 플레이어의 시작 위치를 저장하는 변수이며, 리셋 시 원래 위치로 돌아가기 위해 사용됨
- *jumpForce* : 플레이어가 점프할 때 가해지는 힘을 설정하는 변수
- *moveSpeed* : 플레이어의 이동 속도를 설정하는 변수
- *isOnGround* : 플레이어가 땅에 있는지 여부를 나타내는 변수로, 점프할 수 있는지 여부를 결정
- *hurdleController* : 장애물 컨트롤러를 참조하는 변수로, 플레이어가 점프할 때 장애물의 상태를 업데이트하는 데 사용됨

## 주요 메서드
1. Start()
   - 게임 시작 시 호출되어 ``playerRb``와 ``startPoint``를 초기화함
2. Update()
   - 매 프레임마다 호출되며, 플레이어의 이동과 점프를 처리함
   - *점프* : ``Space`` 키를 눌렀을 때, 플레이어가 땅에 있을 경우 점프를 실행하고 ``hurdleController``의 ``SetPlayerJumping(true)``를 호출하여 장애물에게 플레이어가 점프 중임을 알려줌
   - *이동* : 좌우 이동은 ``Horizontal`` 입력을 받아 처리함
   - *플레이어 리셋* : 플레이어의 ``y`` 좌표가 -15 이하로 떨어지면 ``ResetPlayer()``를 호출하여 초기 위치로 리셋함
3. OnCollisionEnter(Collision collision)
   - 충돌이 발생했을 때 호출됨
   - *장애물과 충돌* : 장애물과 충돌하면 ``hurdleController.SetPlayerJumping(false)``를 호출하여 점프 상태를 종료하고, ``ResetPlayer()``를 호출하여 플레이어를 리셋함
   - *그 외의 충돌* : 땅에 착지했을 때 ``isOnGround``를 ``true``로 설정함
4. ResetPlayer()
   - 플레이어의 위치를 시작 위치로 되돌리고, 속도를 -으로 리셋하여 물리적 움직임을 초기화함
