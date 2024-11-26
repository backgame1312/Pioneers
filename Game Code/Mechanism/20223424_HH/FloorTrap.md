# FloorTrap
## 기능
이 스크립트는 바닥에 설치된 함정이 플레이어와 충돌할때 지정된 시간이 지난 후 함정을 파괴하는 역할을 한다. 주로 플레이어와의 충돌을 감지하여 함정이 작동하도록 처리한다.

## 코드
```C++
public class FloorTrap : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float trapDelay = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Trap());
        }
    }

    private IEnumerator Trap()
    {
        yield return new WaitForSeconds(trapDelay);
        Destroy(gameObject);
    }
}
```

## 주요 변수
- *trapDelay* : 함정이 작동하고 파괴되기까지의 지연 시간을 설정하는 변수이며, 설정한 시간만큼 기다린 후 함정이 파괴된다.

## 주요 메서드
1. OnCollisionEnter(Collision collision)
   - 플레이어와의 충돌을 감지하는 메서드
   - 충돌한 객체의 태그가 ``Player``일 경우, ``Trap()`` 코루틴을 시작하여 함정을 작동시킴
2. Trap()
   - ``IEnumerator`` 형태의 코루틴으로, ``trapDelay`` 시간이 지난 후 함정 객체를 파괴함
   - ``WaitForSeconds(trapdelay)``로 지정된 지연 시간을 기다린 후 ``Destroy(gameObject)``를 호출하여 함정을 파괴함 
