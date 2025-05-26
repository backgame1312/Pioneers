using UnityEngine;

public class MoleHoleTeleporter : MonoBehaviour
{
    [Header("Target Mole Hole to Teleport To")]
    public GameObject targetObject;

    private GameObject playerInHole = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            playerInHole = player.gameObject;
            Debug.Log("플레이어 감지됨");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            playerInHole = null;
            Debug.Log("플레이어 나감");
        }
    }

    void Update()
    {
        if (playerInHole != null && Input.GetKeyDown(KeyCode.S))
        {
            Vector3 currentPos = playerInHole.transform.position;
            float targetX = targetObject.transform.position.x;
            playerInHole.transform.position = new Vector3(targetX, currentPos.y, currentPos.z);
        }
    }
}
