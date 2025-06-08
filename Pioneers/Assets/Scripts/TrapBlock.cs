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
            Debug.LogWarning("SpriteRenderer ����! ��������Ʈ ���� �Ұ�");
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
                rotation = Quaternion.Euler(0, 0, 180); // �Ʒ��� ������ ȸ��
            }

            Instantiate(enemyPrefab, spawnPos, rotation);
            Debug.Log("Ʈ�� �۵�: ���� ������!");
        }

        trapTriggered = true;

        if (spriteRenderer != null && usedBlockSprite != null)
        {
            spriteRenderer.sprite = usedBlockSprite;
        }
    }
}
