using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 오브젝트를 비활성화
            gameObject.SetActive(false);
        }
    }

    public void RestoreObstacle()
    {
        gameObject.SetActive(true);
    }
}
