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
    }

    /// <summary>
    /// 효과음을 재생한다.
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
        {
            Debug.LogWarning("SFX 재생 실패: clip 또는 sfxSource가 null입니다.");
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// 배경음악을 재생한다.
    /// </summary>
    public void PlayBGM()
    {
        if (bgmClip == null || bgmSource == null)
        {
            Debug.LogWarning("BGM 재생 실패: bgmClip 또는 bgmSource가 null입니다.");
            return;
        }

        if (!bgmSource.isPlaying)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }
    }

    /// <summary>
    /// 배경음악을 정지한다.
    /// </summary>
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // 개별 효과음 메서드
    public void PlayButtonClick() => PlaySFX(buttonClickClip);
    public void PlayItemGet() => PlaySFX(itemGetClip);
    public void PlaySnakeAttack() => PlaySFX(snakeAttackClip);
    public void PlayEagleCatch() => PlaySFX(eagleCatchClip);
    public void PlayJump() => PlaySFX(jumpClip);
}
