using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    [System.Serializable]
    public class SlideData
    {
        public Sprite image;
        public AudioClip bgm;
        public AudioClip sfx;
    }

    public Image storyImage;
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    public float displayDuration = 2f; 

    public SlideData[] slides;
    private int currentSlide = 0;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        StartCoroutine(PlaySlides());
    }

    IEnumerator PlaySlides()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            SlideData slide = slides[i];

            yield return StartCoroutine(Fade(1));

            storyImage.sprite = slide.image;

            bgmSource.Stop();
            bgmSource.clip = null;

            if (slide.bgm != null)
            {
                bgmSource.clip = slide.bgm;
                bgmSource.Play();
            }

            sfxSource.Stop();
            sfxSource.clip = null;

            if (slide.sfx != null)
            {
                sfxSource.PlayOneShot(slide.sfx); 
            }

            yield return StartCoroutine(Fade(0));

            yield return new WaitForSeconds(displayDuration);

        }
        bgmSource.Stop();

        FadeManager.Instance.FadeToScene("Stage_EX");
    }


    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator ChangeBGMWithFade(AudioClip newBGM)
    {
        float fadeTime = 0.5f;
        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.clip = newBGM;
        bgmSource.Play();

        while (bgmSource.volume < 1)
        {
            bgmSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
        bgmSource.volume = 1;
    }

}