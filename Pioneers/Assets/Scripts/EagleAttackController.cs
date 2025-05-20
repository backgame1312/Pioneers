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
        if (!isChasing && player.transform.position.x >= triggerXPosition)
        {
            isChasing = true;
            AudioManager.Instance.PlayEagleCatch();
        }

        if (isChasing)
        {
            // 1�� ��ǥ(targetPosition)�� �̵�
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, eagleSpeed * Time.deltaTime);

            // 1�� ��ǥ�� �����ϸ� 2�� ��ǥ(destroyPosition)�� �̵�
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = destroyPosition; // 2�� ��ǥ�� ����
            }

            // 2�� ��ǥ�� �����ϸ� ������ ��Ȱ��ȭ
            if (Vector2.Distance(transform.position, destroyPosition) < 0.1f)
            {
                gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            playerController.Die();
        }
    }

    public void RestoreObstacle()
    {
        Debug.Log("Eagle ������");
        isChasing = false;
        targetPosition = new Vector2(70, 2);
        transform.position = new Vector2(Xposition, Yposition);
        gameObject.SetActive(true);
    }
}
