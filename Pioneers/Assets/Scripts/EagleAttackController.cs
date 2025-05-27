using UnityEngine;

public class EagleAttackController : MonoBehaviour
{
    [Header("Eagle Settings")]
    public GameObject player;
    public float triggerXPosition = 100.0f;
    public float eagleSpeed = 50.0f;

    [Header("Initial Position")]
    public float Xposition = 121.0f;
    public float Yposition = 20.5f;

    [Header("Movement Target Positions")]
    public Vector2 targetPosition = new Vector2(100, 1);
    public Vector2 destroyPosition = new Vector2(80, 20.5f);

    private bool isChasing = false;
    private PlayerController playerController;

    private SpeechBubble speechBubble; // ¡ç Ãß°¡

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        speechBubble = GetComponentInChildren<SpeechBubble>();
    }

    void Update()
    {
        if (!isChasing && player.transform.position.x >= triggerXPosition)
        {
            isChasing = true;
            AudioManager.Instance.PlayEagleCatch();

            if (speechBubble != null && speechBubble.type == SpeechBubble.BalloonType.Exclamation)
            {
                speechBubble.HideBalloon();
            }
        }

        if (isChasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, eagleSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = destroyPosition;
            }

            if (Vector2.Distance(transform.position, destroyPosition) < 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            playerController.Die();
        }
    }

    public void RestoreObstacle()
    {
        Debug.Log("Eagle º¹¿øµÊ");
        isChasing = false;
        targetPosition = new Vector2(75, -3);
        transform.position = new Vector2(Xposition, Yposition);
        gameObject.SetActive(true);

        if (speechBubble != null)
        {
            speechBubble.gameObject.SetActive(true); 
        }
    }
}
