# WallTrap
## 기능
이 스크립트는 플레이어가 벽에 가까워지면 벽을 활성화하고 플레이어가 벽에서 멀어지면 벽을 비활성화하는 역할을 한다. 벽은 `BoxCollider`와 `MeshRenderer`를 사용하여 활성화/비활성화 되며, 플레이어와의 거리 기반응로 제어된다.

## 코드
```c++
public class WallTrap : MonoBehaviour
{
    public Transform player; 
    public float activationDistance = 2f; 
    private BoxCollider wallCollider; 
    private MeshRenderer wallRenderer;

    private void Start()
    {
        wallCollider = GetComponent<BoxCollider>();
        wallRenderer = GetComponent<MeshRenderer>();

        if (wallCollider != null)
        {
            wallCollider.enabled = false; 
        }

        if (wallRenderer != null)
        {
            wallRenderer.enabled = false;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0,
          transform.position.z), new Vector3(player.position.x, 0, player.position.z));

        if (distance < activationDistance)
        {
            if (wallCollider != null)
            {
                wallCollider.enabled = true; 
            }

            if (wallRenderer != null)
            {
                wallRenderer.enabled = true; 
            }
        }
        else
        {
            if (wallCollider != null)
            {
                wallCollider.enabled = false; 
            }

            if (wallRenderer != null)
            {
                wallRenderer.enabled = false; 
            }
        }
    }
}
```

## 주요 변수
- *player* : 벽과의 거리를 계산할 때 기준이 되는 플레이어의 `Transform`을 참조함
- *activationDistance* : 벽이 활성화될 거리로, 플레이어와 벽 사이의 거리가 이 값보다 작으면 벽이 활성화됨
- *wallCollider* : 벽의 `BoxCollider`를 참조하며 플레이어와의 충돌을 감지하는 역할
- *wallRenderer* : 벽의 `MeshRenderer`를 참조하며 벽을 화면에 보이게/보이지 않게 하는 역할

## 주요 메서드
1. Start()
   - 초기화 함수로, 벽의 `BoxCollider`와 `MeshRenderer`를 가져와서 각각 초기 상태로 비활성화함
2. Update()
   - 매 프레임 호출되며, 벽과 플레이어 간의 XZ 평면에서의 거리를 계싼함
     > 만약 그 거리가 `activationDistance`보다 작으면 벽을 활성화하고, 그렇지 않으면 벽을 비활성화 함
