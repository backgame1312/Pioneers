using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerController playerController;

    [Header("Settings")]
    public GameObject playerObject;
    public Transform respawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // playerObject에서 PlayerController 가져오도록 수정
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogWarning("playerObject에 PlayerController가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("playerObject가 설정되지 않았습니다.");
        }
    }

    public void HandlePlayerDeath(Vector3 deathPosition)
    {
        ParticleManager.Instance.PlayDeathEffect(deathPosition);

        if (playerObject != null)
        {
            playerObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("playerObject가 null입니다. 비활성화 실패");
        }

        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        Debug.Log("RespawnPlayer 코루틴 시작");

        // 2. 대기
        yield return new WaitForSeconds(1.0f);

        // 3. 플레이어 위치 초기화 및 활성화
        if (playerObject != null && respawnPoint != null)
        {
            playerObject.transform.position = respawnPoint.position;
            playerObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("playerObject 또는 respawnPoint가 null입니다.");
        }

        // 4. 플레이어 활성화 후 복원 작업 수행
        yield return new WaitForEndOfFrame(); // 1프레임 대기하여 SetActive 반영
        if (playerController != null)
        {
            playerController.RestoreAllObstacles(); // 복원 호출
        }
    }
}
