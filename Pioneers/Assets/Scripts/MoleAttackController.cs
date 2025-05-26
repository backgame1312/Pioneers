using UnityEngine;

public class MoleAttackController : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform player;

    [Header("Trigger Setting")]
    public float triggerMinX = 0.0f;
    public float triggerMaxX = 0.0f;

    [Header("Mole Movement")]
    public float targetY = 0.0f;
    public float moveSpeed = 5.0f;

    [Header("Initial Position")]
    public float Xposition = 0.0f;
    public float Yposition = 0.0f;

    private bool isActivated = false;
    private bool isRising = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        transform.position = new Vector3(Xposition, Yposition, transform.position.z);

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component is missing!");
        }
    }

    private void Update()
    {
        if (!isActivated && IsPlayerInRange() && PressingDownKey())
        {
            isActivated = true;
            isRising = true;
        }

        if (isRising)
        {
            Rise();
        }
    }

    private bool IsPlayerInRange()
    {
        return player.position.x >= triggerMinX && player.position.x <= triggerMaxX;
    }

    private bool PressingDownKey()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    private void Rise()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition.y < targetY)
        {
            transform.position = Vector3.MoveTowards(currentPosition, new Vector3(currentPosition.x, targetY, currentPosition.z), moveSpeed * Time.deltaTime);
        }
        else
        {
            isRising = false;

            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = 2;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated) return;

        if (other.CompareTag("Player"))
        {
            PlayerController playerCtrl = other.GetComponentInParent<PlayerController>();
            if (playerCtrl != null)
            {
                playerCtrl.Die();
            }
            else
            {
                Debug.LogWarning("PlayerController not found on parent object!");
            }
        }
    }

    public void RestoreObstacle()
    {
        transform.position = new Vector3(Xposition, Yposition, transform.position.z);

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 0;
        }

        isActivated = false;
        isRising = false;
    }
}
