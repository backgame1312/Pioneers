using UnityEngine;

public class RocketManager : MonoBehaviour
{
    [Header("Rocket Configuration")]
    public GameObject[] rockets;   // ���� ������Ʈ �迭
    public float[] triggerXs;      // �� ������ �۵��ϴ� X ��ǥ
    public float[] targetXs;       // �� ������ ������ ��ǥ X ��ǥ
    public float speed = 10f;      // ���� �̵� �ӵ�

    private GameObject player;
    private Vector3[] initialPositions;   // �ʱ� ��ġ ���� �迭
    private bool[] isActivated;           // ������ Ȱ��ȭ�Ǿ����� ����
    private bool[] hasReachedTarget;      // ��ǥ ������ �����ߴ��� ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        int count = rockets.Length;
        initialPositions = new Vector3[count];
        isActivated = new bool[count];
        hasReachedTarget = new bool[count];

        for (int i = 0; i < count; i++)
        {
            if (rockets[i] != null)
            {
                initialPositions[i] = rockets[i].transform.position;
                rockets[i].SetActive(false); // ó������ ��Ȱ��ȭ ����
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < rockets.Length; i++)
        {
            if (rockets[i] == null) continue;

            // �÷��̾ Ʈ���� X �̻����� �̵����� �� ���� Ȱ��ȭ
            if (!isActivated[i])
            {
                if (player.transform.position.x >= triggerXs[i])
                {
                    ActivateRocket(i);
                }
            }
            // ������ ��ǥ �������� �̵�
            else if (!hasReachedTarget[i])
            {
                MoveRocketToTarget(i);
            }
        }
    }

    // ���� Ȱ��ȭ �Լ�
    void ActivateRocket(int index)
    {
        rockets[index].SetActive(true);
        isActivated[index] = true;
    }

    // ���� ��ǥ �������� �̵�
    void MoveRocketToTarget(int index)
    {
        Vector3 current = rockets[index].transform.position;
        Vector3 target = new Vector3(targetXs[index], current.y, current.z);
        rockets[index].transform.position = Vector3.MoveTowards(current, target, speed * Time.deltaTime);

        if (Mathf.Abs(current.x - target.x) < 0.01f)
        {
            hasReachedTarget[index] = true;
        }
    }

    // ��� ���� �ʱ�ȭ �Լ� (��ġ �� ���� ����)
    public void RestoreObstacle()
    {
        for (int i = 0; i < rockets.Length; i++)
        {
            if (rockets[i] != null)
            {
                rockets[i].transform.position = initialPositions[i];
                rockets[i].SetActive(false);
                isActivated[i] = false;
                hasReachedTarget[i] = false;
            }
        }
    }
}
