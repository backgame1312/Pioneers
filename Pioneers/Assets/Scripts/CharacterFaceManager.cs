using System.Collections;
using UnityEngine;

public class CharacterFaceManager : MonoBehaviour
{
    public GameObject basicImage;
    public GameObject debuffImage;
    public GameObject buffImage;
    public GameObject dieImage;
    public GameObject clearImage;

    void Start()
    {
        ShowBasic();
    }

    public void HideAll()
    {
        basicImage.SetActive(false);
        debuffImage.SetActive(false);
        buffImage.SetActive(false);
        dieImage.SetActive(false);
        clearImage.SetActive(false);
    }

    public void ShowBasic()
    {
        HideAll();
        basicImage.SetActive(true);
    }

    public void ShowDebuff()
    {
        HideAll();
        debuffImage.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(ReturnToBasicAfterDelay(2f));
    }

    public void ShowBuff()
    {
        HideAll();
        buffImage.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(ReturnToBasicAfterDelay(2f));
    }

    public void ShowFall()
    {
        HideAll();
        dieImage.SetActive(true);

        StopAllCoroutines(); 
        StartCoroutine(ReturnToBasicAfterDelay(2f)); 
    }

    public void ShowClear()
    {
        HideAll();
        clearImage.SetActive(true);
    }

    private IEnumerator ReturnToBasicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowBasic();
    }
}
