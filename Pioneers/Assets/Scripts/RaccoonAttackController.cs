using UnityEngine;

public class RaccoonAttackController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Vector2 moveDirection = Vector2.left;
    private bool facingLeft = true;

    private bool canFlip = true;
    public float flipCooldown = 0.5f;

    void Start()
    {
        moveDirection = Vector2.left;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurnPoint"))
        {
            if (canFlip)  
            {
                FlipDirection();
            }
        }
        else if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    private void FlipDirection()
    {
        moveDirection *= -1;

        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingLeft ? 1 : -1);
        transform.localScale = scale;

        canFlip = false;
        Invoke(nameof(ResetFlip), flipCooldown);
    }

    private void ResetFlip()
    {
        canFlip = true;
    }
}
