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
