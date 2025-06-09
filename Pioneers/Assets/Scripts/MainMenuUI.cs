using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button settingsButton;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        Time.timeScale = 1;

        // 슬라이더 초기화
        bgmSlider.value = AudioManager.Instance.GetBGMVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        // 슬라이더 이벤트 연결
        bgmSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetBGMVolume(value));
        sfxSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetSFXVolume(value));

        // 설정 버튼 클릭 연결
        settingsButton.onClick.AddListener(ToggleSettingsPanel);

        // 시작 시 설정창 꺼두기
        settingsPanel.SetActive(false);

        AudioManager.Instance.PlayMainMenuBGM();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }

    public void ToggleSettingsPanel()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);

        AudioManager.Instance.PlayButtonClick();
    }

    public void OnStartButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        FadeManager.Instance.FadeToScene("Story");
        AudioManager.Instance.StopBGM();
    }

    public void OnTutorialButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        FadeManager.Instance.FadeToScene("Tutorial"); 
        AudioManager.Instance.StopBGM();
    }

    public void OnExitButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        Application.Quit();
    }
}
