using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private AudioClip itemGetClip;
    [SerializeField] private AudioClip snakeAttackClip;
    [SerializeField] private AudioClip eagleCatchClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip bgmClip;

    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
        {
            Debug.LogWarning("SFX ��� ����: clip �Ǵ� sfxSource�� null�Դϴ�.");
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayBGM()
    {
        if (bgmClip == null || bgmSource == null)
        {
            Debug.LogWarning("BGM ��� ����: bgmClip �Ǵ� bgmSource�� null�Դϴ�.");
            return;
        }

        if (!bgmSource.isPlaying)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void PlayButtonClick() => PlaySFX(buttonClickClip);
    public void PlayItemGet() => PlaySFX(itemGetClip);
    public void PlaySnakeAttack() => PlaySFX(snakeAttackClip);
    public void PlayEagleCatch() => PlaySFX(eagleCatchClip);
    public void PlayJump() => PlaySFX(jumpClip);
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
