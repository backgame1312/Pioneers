using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public static UIManager Instance;

    public TextMeshProUGUI deathText; // ��� Ƚ�� ǥ��
    public GameObject deathUI;

    public TextMeshProUGUI elapsedTimeText; // ��� �ð� ǥ��
    public GameObject elapsedTimeUI;

    public GameObject mainMenuUI;

    public GameObject pauseMenuUI; // �Ͻ� ���� �޴� UI
    public GameObject resumeButton; // ����ϱ� ��ư
    public GameObject returnMainMenuButton; // ���� �޴��� ���ư��� ��ư
    private bool isPaused = false;

    private int deathCount = 0;
    private float elapsedTime = 0.0f;

    [Header("Tutorial UI")]
    public GameObject tutorialUI; // Ʃ�丮�� UI
    public Button buttonA, buttonD, buttonE, buttonSpace;
    private bool isFirstDeath = false;

    private Color normalColor = new Color(1, 1, 1, 0.5f); // ������
    private Color pressedColor = new Color(1, 1, 1, 1f);  // ������

    void Start()
    {
        // �ʱ� UI ����
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Time.timeScale = 0; // ���� ����

        deathUI.SetActive(false);
        elapsedTimeUI.SetActive(false);

        tutorialUI.SetActive(false);

        AudioManager.Instance.StopBGM(); // ������� ���߱�
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // �̱��� ����
    }

    void Update()
    {
        UpdateElapsedTime(); // ��� �ð� ������Ʈ

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu(); // �Ͻ� ���� �޴� ���
        }

        UpdateButtonState(buttonA, KeyCode.A); // A ��ư ���� ������Ʈ
        UpdateButtonState(buttonD, KeyCode.D); // D ��ư ���� ������Ʈ
        UpdateButtonState(buttonE, KeyCode.E); // E ��ư ���� ������Ʈ
        UpdateButtonState(buttonSpace, KeyCode.Space); // Space ��ư ���� ������Ʈ
    }

    // ��� �ð� ������Ʈ
    private void UpdateElapsedTime()
    {
        elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        elapsedTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // ��� Ƚ�� ������Ʈ
    public void UpdateDeathCount(int count)
    {
        deathCount = count;
        deathText.text = " " + deathCount;
    }

    // ���� ���� ��ư Ŭ��
    public void OnStartButtonClicked()
    {
        Time.timeScale = 1; // ���� ����
        mainMenuUI.SetActive(false);

        deathUI.SetActive(true);
        elapsedTimeUI.SetActive(true);

        if (!isFirstDeath)
            tutorialUI.SetActive(true);

        AudioManager.Instance.PlayButtonClick();
        AudioManager.Instance.PlayBGM(); // ������� ����
    }

    // ���� ��ư Ŭ��
    public void OnExitButtonClicked()
    {
        Application.Quit(); // ���� ����

        AudioManager.Instance.PlayButtonClick();
    }

    // �Ͻ� ���� �޴� ���
    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        mainMenuUI.SetActive(false);
        Time.timeScale = isPaused ? 0 : 1;
        tutorialUI.SetActive(false);

        if (isPaused)
        {
            AudioManager.Instance.StopBGM(); // ������� ����
        }
        else
        {
            AudioManager.Instance.PlayBGM(); // ������� ����
        }
    }

    // ���� �簳
    public void ResumeGame()
    {
        TogglePauseMenu();

        AudioManager.Instance.PlayButtonClick();
    }

    // ���� �޴��� ���ư���
    public void ReturnMainMenu()
    {
        Time.timeScale = 0;

        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            player.transform.position = player.startingPosition; // ���� ��ġ�� ����
        }

        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

        deathUI.SetActive(false);
        elapsedTimeUI.SetActive(false);

        AudioManager.Instance.PlayButtonClick();
        AudioManager.Instance.PlayBGM(); // ������� ����
    }

    // ��ư ���� ������Ʈ (���� �������� �ƴ���)
    void UpdateButtonState(Button btn, KeyCode key)
    {
        var colors = btn.colors;
        colors.normalColor = Input.GetKey(key) ? pressedColor : normalColor;
        btn.colors = colors;
    }

    // �÷��̾ �׾��� �� ȣ��Ǵ� �޼���
    public void OnPlayerDeath()
    {
        if (!isFirstDeath)
        {
            tutorialUI.SetActive(false); // ù ��° ���� ó�� �� Ʃ�丮�� ����
            isFirstDeath = true;
        }
    }
}
