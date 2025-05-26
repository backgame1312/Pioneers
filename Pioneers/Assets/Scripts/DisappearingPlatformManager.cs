using UnityEngine;

public class DisappearingPlatformManager : MonoBehaviour
{
    private DisappearingPlatform[] platforms;

    private void Awake()
    {
        // 비활성화된 오브젝트까지 모두 포함해서 가져옴
        platforms = Resources.FindObjectsOfTypeAll<DisappearingPlatform>();
        Debug.Log($"[DisappearingPlatformManager] 발견된 DisappearingPlatform 개수 (비활성 포함): {platforms.Length}");
    }

    // 모든 플랫폼을 한꺼번에 복원하는 함수
    public void RestoreObstacle()
    {
        foreach (var platform in platforms)
        {
            if (platform != null)
            {
                platform.RestoreObstacle();
            }
        }
        Debug.Log("[DisappearingPlatformManager] 모든 DisappearingPlatform 복원 완료");
    }
}
