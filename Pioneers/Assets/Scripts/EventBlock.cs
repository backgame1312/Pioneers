using UnityEngine;

public class EventBlock : MonoBehaviour
{
    [Header("Egg Prefab (Object to Spawn)")]
    public GameObject eggPrefab;

    [Header("Egg Spawn Position Offset (Above the block)")]
    public Vector3 eggSpawnOffset = new Vector3(0, 1f, 0);

    [Header("Sprite After Event Triggered")]
    public Sprite usedBlockSprite;

    private bool eventTriggered = false;
    private SpriteRenderer spriteRenderer;

    // 원래 스프라이트 저장용
    private Sprite originalSprite;

    void Start()
    {
        // 스프라이트 렌더러 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer가 없어요! 스프라이트 변경 불가");
        }
        else
        {
            // 원래 스프라이트 저장
            originalSprite = spriteRenderer.sprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (eventTriggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 contactPoint = contact.point;
                Vector2 contactNormal = contact.normal;

                float blockBottomY = GetComponent<Collider2D>().bounds.min.y;

                // 조건 1: 충돌 지점이 블록 밑면에서 일정 거리 이내인지 확인 (0.05f는 허용 오차)
                bool isUnderBlock = contactPoint.y < blockBottomY + 0.05f;

                // 조건 2: 충돌 방향이 거의 위쪽인지 확인
                bool isFromBelow = Vector2.Dot(contactNormal, Vector2.up) > 0.9f;

                // 조건 3: 상대 속도가 위쪽
                bool isMovingUpward = collision.relativeVelocity.y > 0;

                if (isUnderBlock && isFromBelow && isMovingUpward)
                {
                    TriggerEvent();
                    break;
                }
            }
        }
    }


    void TriggerEvent()
    {
        // 달걀 생성 위치 계산
        Vector3 spawnPos = transform.position + eggSpawnOffset;
        // 달걀 생성
        Instantiate(eggPrefab, spawnPos, Quaternion.identity);

        // 이벤트 발생 플래그 설정 (한 번만 실행)
        eventTriggered = true;

        // 스프라이트가 있다면 변경
        if (spriteRenderer != null && usedBlockSprite != null)
        {
            spriteRenderer.sprite = usedBlockSprite;
        }

        Debug.Log("이벤트 발생: 달걀 생성됨! 스프라이트 변경 완료.");
    }
}
