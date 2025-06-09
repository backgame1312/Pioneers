using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public AudioClip checkpointSound;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.volume = 0.6f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.PlayOneShot(checkpointSound);
            hasPlayed = true;
            Debug.Log("체크포인트 완료");
        }
    }
}
