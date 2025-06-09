using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private AudioClip itemGetClip;
    [SerializeField] private AudioClip eggGetClip;
    [SerializeField] private AudioClip snakeAttackClip;
    [SerializeField] private AudioClip eagleCatchClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip mainMenuBgmClip;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip deathClip;


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
        }

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
        {
            Debug.LogWarning("SFX 재생 실패: clip 또는 sfxSource가 null입니다.");
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // 추가된 메서드들
    public void PlayMainMenuBGM()
    {
        if (mainMenuBgmClip == null || bgmSource == null)
        {
            Debug.LogWarning("MainMenu BGM 재생 실패: mainMenuBgmClip 또는 bgmSource가 null입니다.");
            return;
        }

        if (bgmSource.clip != mainMenuBgmClip)
        {
            bgmSource.clip = mainMenuBgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayGameBGM()
    {
        if (bgmClip == null || bgmSource == null)
        {
            Debug.LogWarning("Game BGM 재생 실패: bgmClip 또는 bgmSource가 null입니다.");
            return;
        }

        if (bgmSource.clip != bgmClip)
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
    public void PlayEggGet() => sfxSource.PlayOneShot(eggGetClip, sfxVolume * 0.6f);
    public void PlaySnakeAttack() => PlaySFX(snakeAttackClip);
    public void PlayEagleCatch() => PlaySFX(eagleCatchClip);
    public void PlayJump() => PlaySFX(jumpClip);
    public void PlayDeath() => sfxSource.PlayOneShot(deathClip, sfxVolume * 0.4f);

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
