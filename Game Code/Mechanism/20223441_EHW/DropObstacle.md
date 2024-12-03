# ObstacleTrigger
## 기능
플레이어가 특정 영역에 들어갈 때 머리 위로 장애물을 떨어뜨리며, 장애물이 플레이어와 충돌하면 게임 오버한다.

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

## 주요 변수
- *obstaclePrefab* : 떨어질 장애물의 프리팹을 참조한다.
- *dropPoint* : 장애물이 떨어지는 지점을 지정한다.

## 주요 메서드 
1. OnTriggerEnter2D(Collider2D other)
    - 플레이어가 트리거 영역에 들어오면 호출된다.
2. Instantiate(obstaclePrefab, dropPoint.position, dropPoint.rotation) 
    - 장애물을 지정된 위치에 생성한다.
3. OnCollisionEnter2D(Collision2D collision)
    - 장애물이 플레이어와 충돌하면 호출된다.
4. GameOver()
    - 게임 오버 상태를 트리거한다.
5. GameOver()
    - 게임 오버 시 실행될 로직 추가 예정...
