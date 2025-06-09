using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGoalFlag : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;

        if (collision.CompareTag("Player"))
        {
            isTriggered = true;

            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                FadeManager.Instance.FadeToScene("MainMenu");
            }
        }
    }
}
