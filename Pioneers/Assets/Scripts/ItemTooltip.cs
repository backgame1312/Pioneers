using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public GameObject player;
    public GameObject tooltipUI;
    public GameObject itemSprite;

    public float triggerDistance = 10.0f;

    private bool hasBeenCollected = false;

    void Start()
    {
        if (tooltipUI != null)
            tooltipUI.SetActive(false);
    }

    void Update()
    {
        if (hasBeenCollected || player == null || tooltipUI == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= triggerDistance)
        {
            tooltipUI.SetActive(true);
        }
        else
        {
            tooltipUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenCollected && other.CompareTag("Player"))
        {
            hasBeenCollected = true;
            tooltipUI?.SetActive(false);
            itemSprite?.SetActive(false);
        }
    }

    public void ResetItem()
    {
        hasBeenCollected = false; 
        if (tooltipUI != null)
            tooltipUI.SetActive(false); 
        if (itemSprite != null)
            itemSprite.SetActive(true);
        gameObject.SetActive(true);
    }
}
