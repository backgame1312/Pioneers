using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private SpeechBubble speechBubble;

    private void Start()
    {
        speechBubble = GetComponentInChildren<SpeechBubble>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (speechBubble != null && speechBubble.type == SpeechBubble.BalloonType.Question)
            {
                speechBubble.HideBalloon();
            }

            gameObject.SetActive(false);
        }
    }

    public void RestoreObstacle()
    {
        Debug.Log($"[RestoreObstacle] {gameObject.name} 활성화 시도됨");
        gameObject.SetActive(true);

        if (speechBubble != null)
        {
            Debug.Log($"[RestoreObstacle] {gameObject.name}의 BalloonHint 복구됨");
            speechBubble.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[RestoreObstacle] {gameObject.name}의 BalloonHint 없음");
        }
    }

}
