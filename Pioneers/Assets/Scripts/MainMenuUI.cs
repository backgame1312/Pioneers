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

        // �����̴� �ʱ�ȭ
        bgmSlider.value = AudioManager.Instance.GetBGMVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        // �����̴� �̺�Ʈ ����
        bgmSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetBGMVolume(value));
        sfxSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetSFXVolume(value));

        // ���� ��ư Ŭ�� ����
        settingsButton.onClick.AddListener(ToggleSettingsPanel);

        // ���� �� ����â ���α�
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
