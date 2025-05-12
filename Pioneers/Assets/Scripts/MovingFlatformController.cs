using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement Range (X Axis)")]
    public float minX = -5f;
    public float maxX = 5f;

    [Header("Movement Speed")]
    public float moveSpeed = 2f;

    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= maxX)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x <= minX)
                movingRight = true;
        }
    }
}
