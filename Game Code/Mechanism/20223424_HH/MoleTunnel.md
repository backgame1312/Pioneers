# MoleTunnel
## 기능
이 스크립트는 플레이어가 터널과 충돌할 때, 플레이어를 지정된 범위 내에서 랜덤한 위치로 이동시키는 역할을 한다. 주로 게임 내에서 터널을 통과하는 효과를 구현하거나, 특정 구역에서 플레이어의 위치를 무작위로 변경하고자 할 때 사용된다.

## 코드
```C++
public class MoleTunnel : MonoBehaviour
{
    public GameObject player;

    public Vector3 minPosition;
    public Vector3 maxPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z)
            );

            player.transform.position = randomPosition;
        }
    }
}
```

## 주요 변수
- *player* : 이동시킬 플레이어 객체를 참조하는 변수로, 플레이어의 위치를 변경하기 위해 사용됨
- *minPosition* : 플레이어가 이동할 수 있는 최소 범위의 위치로, 이 값은 X, Y, Z 좌표 모두에 대해 설정됨
- *maxPosition* : 플레이어가 이동할 수 있는 최대 범위의 위치로, 이 값도 X, Y, Z 좌표에 대해 설정됨

## 주요 메서드
1. OnCollisionEnter(Collision collision)
   - 충돌이 발생했을 때 호출되는 함수
   - 충돌할 객체가 `"Player"` 태그를 가진 객체일 경우, 플레이어를 랜덤한 위치로 이동시킴
   - `Random.Range()`를 사용하여 `minPosition`과 `maxPosition` 사이의 랜덤한 X, Y, Z 값을 계산한 후, 이를 플레이어의 새로운 위치로 설정함
