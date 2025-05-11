using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public static UIManager Instance;

    public TextMeshProUGUI deathText; // 사망 횟수 표시
    public GameObject deathUI;

    public TextMeshProUGUI elapsedTimeText; // 경과 시간 표시
    public GameObject elapsedTimeUI;

    public GameObject mainMenuUI;

    public GameObject pauseMenuUI; // 일시 정지 메뉴 UI
    public GameObject resumeButton; // 계속하기 버튼
    public GameObject returnMainMenuButton; // 메인 메뉴로 돌아가기 버튼
    private bool isPaused = false;

    private int deathCount = 0;
    private float elapsedTime = 0.0f;

    [Header("Tutorial UI")]
    public GameObject tutorialUI; // 튜토리얼 UI
    public Button buttonA, buttonD, buttonE, buttonSpace;
    private bool isFirstDeath = false;

    private Color normalColor = new Color(1, 1, 1, 0.5f); // 반투명
    private Color pressedColor = new Color(1, 1, 1, 1f);  // 불투명

    void Start()
    {
        // 초기 UI 설정
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Time.timeScale = 0; // 게임 멈춤

        deathUI.SetActive(false);
        elapsedTimeUI.SetActive(false);

        tutorialUI.SetActive(false);

        AudioManager.Instance.StopBGM(); // 배경음악 멈추기
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // 싱글톤 구현
    }

    void Update()
    {
        UpdateElapsedTime(); // 경과 시간 업데이트

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu(); // 일시 정지 메뉴 토글
        }

        UpdateButtonState(buttonA, KeyCode.A); // A 버튼 상태 업데이트
        UpdateButtonState(buttonD, KeyCode.D); // D 버튼 상태 업데이트
        UpdateButtonState(buttonE, KeyCode.E); // E 버튼 상태 업데이트
        UpdateButtonState(buttonSpace, KeyCode.Space); // Space 버튼 상태 업데이트
    }

    // 경과 시간 업데이트
    private void UpdateElapsedTime()
    {
        elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        elapsedTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // 사망 횟수 업데이트
    public void UpdateDeathCount(int count)
    {
        deathCount = count;
        deathText.text = " " + deathCount;
    }

    // 게임 시작 버튼 클릭
    public void OnStartButtonClicked()
    {
        Time.timeScale = 1; // 게임 시작
        mainMenuUI.SetActive(false);

        deathUI.SetActive(true);
        elapsedTimeUI.SetActive(true);

        if (!isFirstDeath)
            tutorialUI.SetActive(true);

        AudioManager.Instance.PlayButtonClick();
        AudioManager.Instance.PlayBGM(); // 배경음악 시작
    }

    // 종료 버튼 클릭
    public void OnExitButtonClicked()
    {
        Application.Quit(); // 게임 종료

        AudioManager.Instance.PlayButtonClick();
    }

    // 일시 정지 메뉴 토글
    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        mainMenuUI.SetActive(false);
        Time.timeScale = isPaused ? 0 : 1;
        tutorialUI.SetActive(false);

        if (isPaused)
        {
            AudioManager.Instance.StopBGM(); // 배경음악 멈춤
        }
        else
        {
            AudioManager.Instance.PlayBGM(); // 배경음악 시작
        }
    }

    // 게임 재개
    public void ResumeGame()
    {
        TogglePauseMenu();

        AudioManager.Instance.PlayButtonClick();
    }

    // 메인 메뉴로 돌아가기
    public void ReturnMainMenu()
    {
        Time.timeScale = 0;

        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            player.transform.position = player.startingPosition; // 시작 위치로 복귀
        }

        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

        deathUI.SetActive(false);
        elapsedTimeUI.SetActive(false);

        AudioManager.Instance.PlayButtonClick();
        AudioManager.Instance.PlayBGM(); // 배경음악 시작
    }

    // 버튼 상태 업데이트 (누른 상태인지 아닌지)
    void UpdateButtonState(Button btn, KeyCode key)
    {
        var colors = btn.colors;
        colors.normalColor = Input.GetKey(key) ? pressedColor : normalColor;
        btn.colors = colors;
    }

    // 플레이어가 죽었을 때 호출되는 메서드
    public void OnPlayerDeath()
    {
        if (!isFirstDeath)
        {
            tutorialUI.SetActive(false); // 첫 번째 죽음 처리 후 튜토리얼 숨김
            isFirstDeath = true;
        }
    }
}
