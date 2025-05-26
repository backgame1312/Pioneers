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

        // 플레이어 태그 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어가 아래에서 위로 충돌했을 때 (점프해서 블럭 밑에서 부딪힌 경우)
            if (collision.relativeVelocity.y > 0)
            {
                TriggerEvent();
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
