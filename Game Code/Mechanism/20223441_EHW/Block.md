- 유저가 점프하면 위에 블럭이 생겨 점프를 방해함
    - 낭떠러지 도달 직전 트리거 영역 지정
    - 플레이어가 트리거 영역에 도달하면 블럭 생성 이벤트 실행
    - 플레이어 점프 예상 경로에 블럭 생성

```C++
public class Block : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(blockPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
```
