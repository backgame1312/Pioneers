using UnityEngine;

public class DisappearingPlatformManager : MonoBehaviour
{
    private DisappearingPlatform[] platforms;

    private void Awake()
    {
        // ��Ȱ��ȭ�� ������Ʈ���� ��� �����ؼ� ������
        platforms = Resources.FindObjectsOfTypeAll<DisappearingPlatform>();
        Debug.Log($"[DisappearingPlatformManager] �߰ߵ� DisappearingPlatform ���� (��Ȱ�� ����): {platforms.Length}");
    }

    // ��� �÷����� �Ѳ����� �����ϴ� �Լ�
    public void RestoreObstacle()
    {
        foreach (var platform in platforms)
        {
            if (platform != null)
            {
                platform.RestoreObstacle();
            }
        }
        Debug.Log("[DisappearingPlatformManager] ��� DisappearingPlatform ���� �Ϸ�");
    }
}
