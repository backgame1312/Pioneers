using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    [Header("Enemy Prefab (Trap to Spawn)")]
    public GameObject enemyPrefab;

    [Header("Enemy Spawn Position Offset")]
    public Vector3 spawnOffset = new Vector3(0, 1f, 0);

    [Header("Sprite After Trap Triggered")]
    public Sprite usedBlockSprite;

    [Header("Spike Direction")]
    public SpikeDirection spikeDirection = SpikeDirection.Up;

    private bool trapTriggered = false;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private GameObject spawnedEnemy; // 생성된 적 저장

    public enum SpikeDirection
    {
        Up,
        Down
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer 없음! 스프라이트 변경 불가");
        }
        else
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (trapTriggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 contactPoint = contact.point;
                Vector2 contactNormal = contact.normal;
                float blockBottomY = GetComponent<Collider2D>().bounds.min.y;

                bool isUnderBlock = contactPoint.y < blockBottomY + 0.05f;
                bool isFromBelow = Vector2.Dot(contactNormal, Vector2.up) > 0.9f;
                bool isMovingUpward = collision.relativeVelocity.y > 0;

                if (isUnderBlock && isFromBelow && isMovingUpward)
                {
                    TriggerTrap();
                    break;
                }
            }
        }
    }

    void TriggerTrap()
    {
        Vector3 spawnPos = transform.position + spawnOffset;

        if (enemyPrefab != null)
        {
            Quaternion rotation = Quaternion.identity;

            if (spikeDirection == SpikeDirection.Down)
            {
                rotation = Quaternion.Euler(0, 0, 180); // 아래를 보도록 회전
            }

            spawnedEnemy = Instantiate(enemyPrefab, spawnPos, rotation);
            Debug.Log("트랩 작동: 가시 생성됨!");
        }

        trapTriggered = true;

        if (spriteRenderer != null && usedBlockSprite != null)
        {
            spriteRenderer.sprite = usedBlockSprite;
        }
    }

    public void RestoreObstacle()
    {
        trapTriggered = false;

        // 스프라이트 원래대로
        if (spriteRenderer != null && originalSprite != null)
        {
            spriteRenderer.sprite = originalSprite;
        }

        // 생성된 적 제거
        if (spawnedEnemy != null)
        {
            Destroy(spawnedEnemy);
            spawnedEnemy = null;
        }

        Debug.Log("트랩이 초기 상태로 복원되었습니다.");
    }
}
