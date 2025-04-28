using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    public AudioClip buttonClickClip;
    public AudioClip itemGetClip;
    public AudioClip snakeAttackClip;
    public AudioClip eagleCatchClip;
    public AudioClip jumpClip;
    public AudioClip bgmClip;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM()
    {
        if (bgmClip != null && bgmSource != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    public void PlayButtonClick() => PlaySFX(buttonClickClip);
    public void PlayItemGet() => PlaySFX(itemGetClip);
    public void PlaySnakeAttack() => PlaySFX(snakeAttackClip);
    public void PlayEagleCatch() => PlaySFX(eagleCatchClip);
    public void PlayJump() => PlaySFX(jumpClip);
}
