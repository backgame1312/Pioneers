using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlayBGM();
    }

    public void OnStartButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        SceneManager.LoadScene("Tutorial");
        AudioManager.Instance.StopBGM();
    }

    public void OnExitButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        Application.Quit();
    }
}
