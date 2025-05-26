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

    private bool[] isActivated;

    void Start()
    {
        // Initialize platform activation status
        isActivated = new bool[platformsToActivate.Length];

        for (int i = 0; i < platformsToActivate.Length; i++)
        {
            if (platformsToActivate[i] != null)
            {
                platformsToActivate[i].SetActive(false); // Initially inactive
            }
            isActivated[i] = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        float playerX = player.position.x;

        // Check only when space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < platformsToActivate.Length; i++)
            {
                if (!isActivated[i] &&
                    playerX >= minXPositions[i] &&
                    playerX <= maxXPositions[i])
                {
                    platformsToActivate[i].SetActive(true);
                    isActivated[i] = true;
                    Debug.Log($"Platform {i} activated");
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
