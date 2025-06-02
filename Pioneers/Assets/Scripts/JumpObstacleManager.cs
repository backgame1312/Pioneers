using UnityEngine;

public class JumpObstacleManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Platforms to Activate")]
    public GameObject[] platformsToActivate;

    [Header("X Range for Platform Activation (Min, Max)")]
    public float[] minXPositions;
    public float[] maxXPositions;

    [Header("Y Range for Platform Activation (Min, Max)")]
    public float[] minYPositions;
    public float[] maxYPositions;

    private bool[] isActivated;

    void Start()
    {
        isActivated = new bool[platformsToActivate.Length];

        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            if (platformsToActivate[i] != null)
            {
                platformsToActivate[i].SetActive(false); // 처음엔 비활성화
            }
            isActivated[i] = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        float playerX = player.position.x;
        float playerY = player.position.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < platformsToActivate.Length; i++)
            {
                if (!isActivated[i] &&
                    playerX >= minXPositions[i] && playerX <= maxXPositions[i] &&
                    playerY >= minYPositions[i] && playerY <= maxYPositions[i])
                {
                    platformsToActivate[i].SetActive(true);
                    isActivated[i] = true;
                    Debug.Log($"Platform {i} activated at ({playerX}, {playerY})");
                }
            }
        }
    }

    public void RestoreObstacle()
    {
        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            if (platformsToActivate[i] != null)
            {
                platformsToActivate[i].SetActive(false);
                isActivated[i] = false;
            }
        }
    }
}
