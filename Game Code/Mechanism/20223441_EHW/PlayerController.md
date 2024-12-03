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
