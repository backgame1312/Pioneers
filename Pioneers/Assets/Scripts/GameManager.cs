using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerController playerController;
    private ObstacleRestoreManager obstacleRestoreManager;

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
        // playerObject���� PlayerController ���������� ����
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogWarning("playerObject�� PlayerController�� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("playerObject�� �������� �ʾҽ��ϴ�.");
        }

        obstacleRestoreManager = FindAnyObjectByType<ObstacleRestoreManager>();
        if (obstacleRestoreManager == null)
            Debug.LogWarning("ObstacleRestoreManager�� ���� �����ϴ�!");
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
            Debug.LogWarning("playerObject�� null�Դϴ�. ��Ȱ��ȭ ����");
        }

        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        Debug.Log("RespawnPlayer �ڷ�ƾ ����");

        yield return new WaitForSeconds(1.0f);

        if (playerObject != null)
        {
            Vector3 respawnPos = respawnPoint != null ? respawnPoint.position : playerObject.transform.position;

            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            if (playerController != null && playerController.hasLastNest)
            {
                respawnPos = playerController.lastCheckpointPosition;
            }

            playerObject.transform.position = respawnPos;
            playerObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("playerObject�� null�Դϴ�.");
        }

        yield return new WaitForEndOfFrame();

        if (playerController != null)
        {
            obstacleRestoreManager?.RestoreAllObstacles();
        }

        FindAnyObjectByType<DisappearingPlatformManager>()?.RestoreObstacle();
    }
}
