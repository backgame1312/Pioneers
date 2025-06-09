using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public Image storyImage;
    public Sprite[] storySprites;
    public float fadeDuration = 0.5f;
    public float displayDuration = 2f;

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(PlayStory());
    }

    IEnumerator PlayStory()
    {
        while (currentIndex < storySprites.Length)
        {
            storyImage.sprite = storySprites[currentIndex];

            yield return StartCoroutine(FadeImage(0, 1));

            yield return new WaitForSeconds(displayDuration);

            yield return StartCoroutine(FadeImage(1, 0));

            currentIndex++;
        }

        FadeManager.Instance.FadeToScene("Stage_EX");
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        float t = 0;
        Color c = storyImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            storyImage.color = c;
            yield return null;
        }
        c.a = endAlpha;
        storyImage.color = c;
    }
}
