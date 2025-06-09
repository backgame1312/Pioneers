using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject deathAndElapseTimeGameObject;
    public TextMeshProUGUI death;
    public TextMeshProUGUI elapseTime;
    public float ElapsedTime => elapsed;
    public int DeathCount => deathCount;

    public GameObject pauseUI;
    public GameObject resumeButton;
    public GameObject returnMainMenuButton;
    private bool isPaused = false;

    private int deathCount = 0;
    private float elapsed = 0.0f;

    public GameObject tutorialUI;
    public Button buttonA, buttonD, buttonE, buttonSpace;
    private bool isFirstDeath = false;

    Color normal = new Color(1, 1, 1, 0.5f); // 반투명
    Color pressed = new Color(1, 1, 1, 1f);  // 불투명

    private bool isTutorialStage = false;

    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI eggText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;

        deathAndElapseTimeGameObject.SetActive(true);

        string currentSceneName = SceneManager.GetActiveScene().name;
        isTutorialStage = currentSceneName == "Tutorial";

        if (SceneManager.GetActiveScene().name == "Tutorial" && !isFirstDeath)
        {
            if (tutorialUI != null)
            {
                tutorialUI.SetActive(true);
                StartCoroutine(HideTutorialAfterDelay(4f));
            }
        }

        AudioManager.Instance.PlayGameBGM();
    }

    IEnumerator HideTutorialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (tutorialUI != null)
            tutorialUI.SetActive(false);
    }

    void Update()
    {
        UpdateElapsedTime();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (isTutorialStage)
        {
            UpdateButton(buttonA, KeyCode.A);
            UpdateButton(buttonD, KeyCode.D);
            UpdateButton(buttonE, KeyCode.E);
            UpdateButton(buttonSpace, KeyCode.Space);
        }
    }

    private void UpdateElapsedTime()
    {
        elapsed += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsed / 3600f);
        int minutes = Mathf.FloorToInt((elapsed % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);

        elapseTime.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void UpdateDeathCount(int count)
    {
        deathCount = count;
        death.text = " " + deathCount;
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        tutorialUI.SetActive(false);

        if (isPaused)
            AudioManager.Instance.StopBGM();
        else
            AudioManager.Instance.PlayGameBGM();
    }

    public void ResumeGame()
    {
        TogglePauseMenu();
        AudioManager.Instance.PlayButtonClick();
    }

    public void ReturnMainMenuButton()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");

        AudioManager.Instance.PlayButtonClick();
        AudioManager.Instance.PlayGameBGM();
    }

    void UpdateButton(Button btn, KeyCode key)
    {
        if (btn == null) return;

        var colors = btn.colors;
        colors.normalColor = Input.GetKey(key) ? pressed : normal;
        btn.colors = colors;
    }

    public void OnPlayerDeath()
    {
        if (!isFirstDeath)
        {
            if (tutorialUI != null)
                tutorialUI.SetActive(false);
            isFirstDeath = true;
        }
    }

    public void ShowResult(float time, int deaths, int eggs)
    {
        resultPanel.SetActive(true);

        timeText.text = $"걸린 시간: {FormatTime(time)}";
        deathText.text = $"죽은 횟수: {deaths}번";
        eggText.text = $"되찾은 달걀: {eggs}/10개";
    }

    private string FormatTime(float t)
    {
        int min = Mathf.FloorToInt(t / 60);
        int sec = Mathf.FloorToInt(t % 60);
        return $"{min:00}:{sec:00}";
    }
    public void OnClickReturnToMainFromResult()
    {
        AudioManager.Instance.PlayButtonClick();
        Time.timeScale = 1f;
        FadeManager.Instance.FadeToScene("MainMenu");
    }
}
