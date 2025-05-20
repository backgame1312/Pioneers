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

        // 2. ���
        yield return new WaitForSeconds(1.0f);

        // 3. �÷��̾� ��ġ �ʱ�ȭ �� Ȱ��ȭ
        if (playerObject != null && respawnPoint != null)
        {
            playerObject.transform.position = respawnPoint.position;
            playerObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("playerObject �Ǵ� respawnPoint�� null�Դϴ�.");
        }

        // 4. �÷��̾� Ȱ��ȭ �� ���� �۾� ����
        yield return new WaitForEndOfFrame(); // 1������ ����Ͽ� SetActive �ݿ�
        if (playerController != null)
        {
            playerController.RestoreAllObstacles(); // ���� ȣ��
        }
    }
}
