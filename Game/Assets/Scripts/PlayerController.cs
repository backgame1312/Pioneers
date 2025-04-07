using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float deathY = -10f;
    private Vector2 respawnPosition = new Vector2(-5f, -2f);
    private int deathCount = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    void Die()
    {
        deathCount++;

        transform.position = respawnPosition;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        UIManager.Instance.UpdateDeathCount(deathCount);
    }
}
