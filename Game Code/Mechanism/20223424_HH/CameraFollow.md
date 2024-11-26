# CameraFollow
## 기능
이 스크립트는 카메라가 플레이어를 따라가도록 하여, 카메라의 위치를 플레이어의 위치에 맞춰 조정하는 역할을 한다. 카메라는 일정한 오프셋을 유지하면서, 고정된 Y좌표에서 플레이어를 따라간다.

## 코드
```C++
public class CameraFollow : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform player;
    public Vector3 offset; 
    public float fixedY; 

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;

            transform.position = new Vector3(targetPosition.x, fixedY, targetPosition.z);
        }
    }
}
```

## 주요 변수
- *player* : 카메라가 따라갈 ``Transform`` 타입의 플레이어 객체
- *offset* : 카메라와 플레이어 간의 상대적인 위치차이(오프셋)을 정의하는 변수로, 카메라가 플레이어의 위치에 따라 이동할 때 기준이 됨
- *fixedY* : 카메라의 Y 좌표를 고정시키는 값으로, 플레이어의 Y 위치에 관계없이 카메라는 항상 설정된 값으로 Y 좌표가 설정됨

## 주요 메서드
1. LateUpdate()
   - 매 프레임마다 호출되며, 플레이어가 이동한 후 카메라의 위치를 업데이트함
   - 플레이어 객체가 존재하는 경우, 플레이어의 위치와 오프셋을 합쳐서 카메라의 목표 위치를 계산하고, 고정된 Y 값으로 카메라의 위치를 설정함 
