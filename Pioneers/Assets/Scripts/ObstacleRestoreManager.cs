using System.Collections.Generic;
using UnityEngine;

public class ObstacleRestoreManager : MonoBehaviour
{
    // ��ֹ� �Ŵ�����
    private DisappearingPlatformManager disappearingPlatformManager;
    private AppearingPlatformManager appearingPlatformManager;
    private FallingAttackManager fallingAttackManager;
    private JumpObstacleManager jumpObstacleManager;
    private RocketManager rocketManager;

    // ��ֹ� �迭��
    private FallingPlatform[] fallingPlatforms;
    private SpikeBlockActivator[] spikeBlockActivators;
    private RisingPlatform[] risingPlatforms;
    private EagleAttackController[] eagleAttackControllers;
    private MoleAttackController[] moleAttackControllers;
    private RaccoonAttackController[] raccoonAttackControllers;
    private TrapBlock[] trapBlocks;
    private StepActivatedPlatform[] stepActivatedPlatforms;
    private ClosingWallController[] closingWallControllers;

    // ������ ����Ʈ�� (�ν����Ϳ��� ���� ����)
    [Header("Item Lists")]
    public List<GameObject> speedUPItems;
    public List<GameObject> doubleJumpItems;
    public List<GameObject> speedDownItems;
    public List<GameObject> weakenedJumpItems;
    public List<GameObject> eggObstacles;

    void Start()
    {
        // ��ֹ� �Ŵ����� ã��
        disappearingPlatformManager = FindAnyObjectByType<DisappearingPlatformManager>();
        appearingPlatformManager = FindAnyObjectByType<AppearingPlatformManager>();
        fallingAttackManager = FindAnyObjectByType<FallingAttackManager>();
        jumpObstacleManager = FindAnyObjectByType<JumpObstacleManager>();
        rocketManager = FindAnyObjectByType<RocketManager>();

        // ��ֹ��� ã��
        fallingPlatforms = FindObjectsByType<FallingPlatform>(FindObjectsSortMode.None);
        spikeBlockActivators = FindObjectsByType<SpikeBlockActivator>(FindObjectsSortMode.None);
        risingPlatforms = FindObjectsByType<RisingPlatform>(FindObjectsSortMode.None);
        eagleAttackControllers = FindObjectsByType<EagleAttackController>(FindObjectsSortMode.None);
        moleAttackControllers = FindObjectsByType<MoleAttackController>(FindObjectsSortMode.None);
        raccoonAttackControllers = FindObjectsByType<RaccoonAttackController>(FindObjectsSortMode.None);
        trapBlocks = FindObjectsByType<TrapBlock>(FindObjectsSortMode.None);
        stepActivatedPlatforms = FindObjectsByType<StepActivatedPlatform>(FindObjectsSortMode.None);
        closingWallControllers = FindObjectsByType<ClosingWallController>(FindObjectsSortMode.None);
    }

    public void RestoreAllObstacles()
    {
        // ��ֹ� �Ŵ��� ����
        fallingAttackManager?.RestoreObstacles();
        disappearingPlatformManager?.RestoreObstacle();
        appearingPlatformManager?.RestoreObstacle();
        jumpObstacleManager?.RestoreObstacle();
        rocketManager?.RestoreObstacle();

        // ��ֹ� ���� ����
        foreach (var platform in fallingPlatforms)
            platform?.RestoreObstacle();

        foreach (var spike in spikeBlockActivators)
            spike?.RestoreObstacle();

        foreach (var rise in risingPlatforms)
            rise?.RestoreObstacle();

        foreach (var eagle in eagleAttackControllers)
            eagle?.RestoreObstacle();

        foreach (var mole in moleAttackControllers)
            mole?.RestoreObstacle();

        foreach (var raccoon in raccoonAttackControllers)
            raccoon?.RestoreObstacle();

        foreach (var trap in trapBlocks)
            trap?.RestoreObstacle();

        foreach (var step in stepActivatedPlatforms)
            step?.RestoreObstacle();

        foreach (var wall in closingWallControllers)
            wall?.RestoreObstacle();

        // ������ ���� (SetActive(true))
        RestoreGameObjectList(speedUPItems);
        RestoreGameObjectList(doubleJumpItems);
        RestoreGameObjectList(speedDownItems);
        RestoreGameObjectList(weakenedJumpItems);
        RestoreGameObjectList(eggObstacles);
    }

    private void RestoreGameObjectList(List<GameObject> itemList)
    {
        if (itemList == null) return;

        foreach (var item in itemList)
        {
            if (item != null)
                item.SetActive(true);
        }
    }
}
