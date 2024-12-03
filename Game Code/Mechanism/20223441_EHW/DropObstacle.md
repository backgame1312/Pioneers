- 특정 구간 트리거 영역 지정
- 플레이어가 트리거 영역에 도달하면 장애물 생성 이벤트 실행
- 트리거 발동 후 플레이어 머리 위로 생성 or 미리 배치된 장애물 낙하

```C++
public class ObstacleTrigger : MonoBehaviour
{
    public GameObject obstaclePrefab; 
    public Transform dropPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obstacle = Instantiate(obstaclePrefab, dropPoint.position, dropPoint.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
```
++ 장애물 낙하 속도 조절해서 재미 유발
