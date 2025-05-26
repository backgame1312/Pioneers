using UnityEngine;

public class FallingAttackManager : MonoBehaviour
{
    private FallingAttack[] fallingObjects;
    public Transform player;

    void Awake()
    {
        fallingObjects = GameObject.FindObjectsByType<FallingAttack>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (player == null) return;

        foreach (var fallingObject in fallingObjects)
        {
            fallingObject.CheckAndStartFalling(player.position.x);
        }
    }

    public void RestoreObstacles()
    {
        foreach (var fallingObject in fallingObjects)
        {
            fallingObject.RestoreObstacle();
        }
    }
}
