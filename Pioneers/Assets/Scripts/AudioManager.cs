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
    /// ȿ������ ����Ѵ�.
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
        {
            Debug.LogWarning("SFX ��� ����: clip �Ǵ� sfxSource�� null�Դϴ�.");
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// ��������� ����Ѵ�.
    /// </summary>
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
            bgmSource.Play();
        }
    }

    /// <summary>
    /// ��������� �����Ѵ�.
    /// </summary>
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // ���� ȿ���� �޼���
    public void PlayButtonClick() => PlaySFX(buttonClickClip);
    public void PlayItemGet() => PlaySFX(itemGetClip);
    public void PlaySnakeAttack() => PlaySFX(snakeAttackClip);
    public void PlayEagleCatch() => PlaySFX(eagleCatchClip);
    public void PlayJump() => PlaySFX(jumpClip);
}
