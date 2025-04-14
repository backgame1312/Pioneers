using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Respawn Settings")]
    public float deathY = -10.0f;
    private Vector2 respawnPosition = new Vector2(-5f, -2f);
    private int deathCount = 0;


    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    [Header("Jump Tuning")]
    public float defaultjump = 2f;
    public float jumpSpeed = 4.0f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovementController();
        JumpGravity();
        CheckFallDeath();
    }

    private void MovementController()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void JumpGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = jumpSpeed;
        }
        else
        {
            rb.gravityScale = defaultjump;
        }
    }

    private void CheckFallDeath()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    void Die()
    {
        deathCount++;
        Debug.Log("ÇÃ·¹ÀÌ¾î°¡ Á×¾ú½À´Ï´Ù. Á×Àº È½¼ö: " + deathCount);

        transform.position = respawnPosition;

        rb.velocity = Vector2.zero;

        UIManager.Instance.UpdateDeathCount(deathCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
