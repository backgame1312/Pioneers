using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    public GameObject deathEffectPrefab;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayDeathEffect(Vector3 position)
    {
        if (deathEffectPrefab != null)
        {
            Vector3 spawnPos = new Vector3(position.x, position.y, -10f); // z°ª °íÁ¤
            Instantiate(deathEffectPrefab, position, Quaternion.identity);
        }
    }
}
