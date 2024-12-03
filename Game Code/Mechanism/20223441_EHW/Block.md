# Block
## 기능
유저가 특정 구역에서 점프를 할 시 위에 블럭을 생성하여 점프를 방해한다.

## 코드
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

## 주요 변수
- *blockPrefab* : 이 프리팹을 사용하여 새로운 블럭을 게임 내에 생성한다.
- *spawnPoint* : 블럭의 생성 위치를 지정한다.

## 주요 메서드
1. OnTriggerEnter(Collider other)
   - 충돌 감지 함수로, 플레이어가 지정된 트리거 영역에 들어오면 호출된다.
3. other.CompareTag("Player")
   - 충돌한 객체가 "Player" 태그를 가진 객체일 경우, 블럭을 생성한다.
5. Instantiate(blockPrefab, spawnPoint.position, spawnPoint.rotation)
   - spawnPoint에 블럭을 생성한다.
