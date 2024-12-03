# ClearBlock
## 기능
플레이어가 투명 블럭 위에 서 있을 때 일정 시간이 지나면 블럭을 삭제하여 플레이어를 떨어뜨린다.

```C++
public class ClearBlock : MonoBehaviour
{
    public float jumpForce = 5f; // 점프 힘
    private Rigidbody2D playerRb;

    public float disappearTime = 3f; // 블럭 사라지기까지의 시간
    private bool playerOnBlock = false; // 블럭위에 있는지 확인

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnBlock = true; // 블럭에 닿으면 true
            playerRb = collision.collider.GetComponent<Rigidbody2D>(); 
        }
    }

    private void Update()
    {
        if (playerOnBlock)
        {
            disappearTime -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && playerRb != null) // 점프 입력 시
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 점프력 적용
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
            playerOnBlock = false; // 블럭에서 벗어나면 false
        }
    }
}
```
