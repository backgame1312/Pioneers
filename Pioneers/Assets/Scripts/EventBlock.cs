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

        // �÷��̾� �±� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ �Ʒ����� ���� �浹���� �� (�����ؼ� �� �ؿ��� �ε��� ���)
            if (collision.relativeVelocity.y > 0)
            {
                TriggerEvent();
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
