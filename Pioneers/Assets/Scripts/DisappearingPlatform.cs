using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private BalloonHint balloonHint;

    private void Start()
    {
        balloonHint = GetComponentInChildren<BalloonHint>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (balloonHint != null && balloonHint.type == BalloonHint.BalloonType.Question)
            {
                balloonHint.HideBalloon();
            }

            gameObject.SetActive(false);
        }
    }

    public void RestoreObstacle()
    {
        gameObject.SetActive(true);

        if (balloonHint != null)
            balloonHint.gameObject.SetActive(true);
    }
}
