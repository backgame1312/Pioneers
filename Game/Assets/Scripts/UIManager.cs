using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI death;
    public TextMeshProUGUI elapseTime;

    private int deathCount = 0;
    private float elapsed = 0.0f;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElapsedTime();
    }

    private void UpdateElapsedTime()
    {
        elapsed += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsed / 3600f);
        int minutes = Mathf.FloorToInt((elapsed % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);

        elapseTime.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void UpdateDeathCount(int count)
    {
        deathCount = count;
        death.text = "Á×Àº È½¼ö : " + deathCount;
    }
}
