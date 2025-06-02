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

    // ���� ��������Ʈ �����
    private Sprite originalSprite;

    void Start()
    {
        // ��������Ʈ ������ ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer�� �����! ��������Ʈ ���� �Ұ�");
        }
        else
        {
            // ���� ��������Ʈ ����
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

                // ���� 1: �浹 ������ ��� �ظ鿡�� ���� �Ÿ� �̳����� Ȯ�� (0.05f�� ��� ����)
                bool isUnderBlock = contactPoint.y < blockBottomY + 0.05f;

                // ���� 2: �浹 ������ ���� �������� Ȯ��
                bool isFromBelow = Vector2.Dot(contactNormal, Vector2.up) > 0.9f;

                // ���� 3: ��� �ӵ��� ����
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
        // �ް� ���� ��ġ ���
        Vector3 spawnPos = transform.position + eggSpawnOffset;
        // �ް� ����
        Instantiate(eggPrefab, spawnPos, Quaternion.identity);

        // �̺�Ʈ �߻� �÷��� ���� (�� ���� ����)
        eventTriggered = true;

        // ��������Ʈ�� �ִٸ� ����
        if (spriteRenderer != null && usedBlockSprite != null)
        {
            spriteRenderer.sprite = usedBlockSprite;
        }

        Debug.Log("�̺�Ʈ �߻�: �ް� ������! ��������Ʈ ���� �Ϸ�.");
    }
}
