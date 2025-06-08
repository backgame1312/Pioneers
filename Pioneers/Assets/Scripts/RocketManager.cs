using UnityEngine;

public class RocketManager : MonoBehaviour
{
    [Header("Rocket Configuration")]
    public GameObject[] rockets;   // 로켓 오브젝트 배열
    public float[] triggerXs;      // 각 로켓이 작동하는 X 좌표
    public float[] targetXs;       // 각 로켓이 도달할 목표 X 좌표
    public float speed = 10f;      // 로켓 이동 속도

    private GameObject player;
    private Vector3[] initialPositions;   // 초기 위치 저장 배열
    private bool[] isActivated;           // 로켓이 활성화되었는지 여부
    private bool[] hasReachedTarget;      // 목표 지점에 도달했는지 여부

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
                rockets[i].SetActive(false); // 처음에는 비활성화 상태
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < rockets.Length; i++)
        {
            if (rockets[i] == null) continue;

            // 플레이어가 트리거 X 이상으로 이동했을 때 로켓 활성화
            if (!isActivated[i])
            {
                if (player.transform.position.x >= triggerXs[i])
                {
                    ActivateRocket(i);
                }
            }
            // 로켓이 목표 지점까지 이동
            else if (!hasReachedTarget[i])
            {
                MoveRocketToTarget(i);
            }
        }
    }

    // 로켓 활성화 함수
    void ActivateRocket(int index)
    {
        rockets[index].SetActive(true);
        isActivated[index] = true;
    }

    // 로켓 목표 지점으로 이동
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

    // 모든 로켓 초기화 함수 (위치 및 상태 복원)
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
