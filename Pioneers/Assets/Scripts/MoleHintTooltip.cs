using UnityEngine;

public class HintTooltip : MonoBehaviour
{
    public GameObject player;
    public GameObject tooltipUI;
    public float triggerDistance = 10.0f;

    void Start()
    {
        if (tooltipUI != null)
            tooltipUI.SetActive(false);
    }

    void Update()
    {
        if (player == null || tooltipUI == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        tooltipUI.SetActive(distance <= triggerDistance);
    }
}
