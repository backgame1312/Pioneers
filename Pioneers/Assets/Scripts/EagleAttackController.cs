using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttackController : MonoBehaviour
{
    [Header("Eagle Settings")]
    public GameObject player; // �÷��̾�
    public float triggerXPosition = 100.0f; // �������� ������ �����ϴ� X��
    public float eagleSpeed = 50.0f; // ������ �ӵ�

    [Header("Initial Position")]
    public float Xposition = 121.0f; // �������� �ʱ� X��
    public float Yposition = 20.5f; // �������� �ʱ� Y��

    [Header("Movement Target Positions")]
    public Vector2 targetPosition = new Vector2(100, 1); // �������� �̵��� ù ��° ��ǥ ��ġ
    public Vector2 destroyPosition = new Vector2(80, 20.5f); // �������� �̵��� �� ��° ��ǥ ��ġ

    private bool isChasing = false; // ���� ���� Ȯ��

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ Ư�� ��ġ�� ������ �������� ������ ����
        if (!isChasing && player.transform.position.x >= triggerXPosition)
        {
            isChasing = true;
            AudioManager.Instance.PlayEagleCatch();
        }

        // ���� ������ �� �������� ��ǥ ��ġ�� �̵�
        if (isChasing)
        {
            // ù ��° ��ǥ ��ġ(targetPosition)�� �̵�
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, eagleSpeed * Time.deltaTime);

            // ù ��° ��ǥ�� �����ϸ� �� ��° ��ǥ(destroyPosition)�� �̵�
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = destroyPosition; // �� ��° ��ǥ�� ����
            }

            // �� ��° ��ǥ�� �����ϸ� �������� ��Ȱ��ȭ
            if (Vector2.Distance(transform.position, destroyPosition) < 0.1f)
            {
                gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            }
        }
    }

    // �÷��̾�� �浹 �� ������ ��Ȱ��ȭ �� �÷��̾� ��� ó��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chicken"))
        {
            gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            playerController.Die(); // �÷��̾� ��� ó��
        }
    }

    // ������ ���� �޼��� (�ʱ� ��ġ�� �ǵ�����)
    public void RestoreObstacle()
    {
        transform.position = new Vector2(Xposition, Yposition); // �ʱ� ��ġ�� ����
        gameObject.SetActive(true); // ������ Ȱ��ȭ
        isChasing = false; // ���� ���� �ʱ�ȭ
        targetPosition = new Vector2(100, 1); // ù ��° ��ǥ ��ġ�� ����
    }
}
