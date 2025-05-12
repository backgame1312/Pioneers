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

        if (!isFirstDeath)
        {
            tutorialUI.SetActive(true);
            StartCoroutine(HideTutorialAfterDelay(5f));
        }

        AudioManager.Instance.PlayBGM();
    }

    IEnumerator HideTutorialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialUI.SetActive(false);
    }

    void Update()
    {
        UpdateElapsedTime();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        UpdateButton(buttonA, KeyCode.A);
        UpdateButton(buttonD, KeyCode.D);
        UpdateButton(buttonE, KeyCode.E);
        UpdateButton(buttonSpace, KeyCode.Space);
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
            AudioManager.Instance.PlayBGM();
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
        AudioManager.Instance.PlayBGM();
    }

    void UpdateButton(Button btn, KeyCode key)
    {
        var colors = btn.colors;
        colors.normalColor = Input.GetKey(key) ? pressed : normal;
        btn.colors = colors;
    }

    public void OnPlayerDeath()
    {
        if (!isFirstDeath)
        {
            tutorialUI.SetActive(false);
            isFirstDeath = true;
        }
    }
}
