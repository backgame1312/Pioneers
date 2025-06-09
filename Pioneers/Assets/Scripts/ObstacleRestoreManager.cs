using UnityEngine;

public class ObstacleRestoreManager : MonoBehaviour
{
    private DisappearingPlatformManager disappearingPlatformManager;
    private AppearingPlatformManager appearingPlatformManager;
    private FallingAttackManager fallingAttackManager;
    private JumpObstacleManager jumpObstacleManager;
    private RocketManager rocketManager;
    private FallingPlatform[] fallingPlatforms;
    private SpikeBlockActivator[] spikeBlockActivators;
    private RisingPlatform[] risingPlatforms;
    private EagleAttackController[] eagleAttackControllers;
    private MoleAttackController[] moleAttackControllers;
    private RaccoonAttackController[] raccoonAttackControllers;
    private TrapBlock[] trapBlocks;
    private StepActivatedPlatform[] stepActivatedPlatforms;
    private ClosingWallController[] closingWallControllers;

    void Start()
    {
        disappearingPlatformManager = FindAnyObjectByType<DisappearingPlatformManager>();
        appearingPlatformManager = FindAnyObjectByType<AppearingPlatformManager>();
        fallingAttackManager = FindAnyObjectByType<FallingAttackManager>();
        jumpObstacleManager = FindAnyObjectByType<JumpObstacleManager>();
        rocketManager = FindAnyObjectByType<RocketManager>();
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
        if (fallingAttackManager != null)
            fallingAttackManager.RestoreObstacles();

        if (disappearingPlatformManager != null)
            disappearingPlatformManager.RestoreObstacle();

        if (appearingPlatformManager != null)
            appearingPlatformManager.RestoreObstacle();

        if (jumpObstacleManager != null)
            jumpObstacleManager.RestoreObstacle();

        if (rocketManager != null)
            rocketManager.RestoreObstacle();

        if (fallingPlatforms != null)
        {
            foreach (var platform in fallingPlatforms)
            {
                if (platform != null)
                    platform.RestoreObstacle();
            }
        }

        if (spikeBlockActivators != null)
        {
            foreach (var spikePlatform in spikeBlockActivators)
            {
                if (spikePlatform != null)
                    spikePlatform.RestoreObstacle();
            }
        }

        if (risingPlatforms != null)
        {
            foreach (var risingPlatform in risingPlatforms)
            {
                if (risingPlatform != null)
                    risingPlatform.RestoreObstacle();
            }
        }

        if (eagleAttackControllers != null)
        {
            foreach (var eagle in eagleAttackControllers)
            {
                if (eagle != null)
                    eagle.RestoreObstacle();
            }
        }

        if (moleAttackControllers != null)
        {
            foreach (var mole in moleAttackControllers)
            {
                if (mole != null)
                    mole.RestoreObstacle();
            }
        }

        if (raccoonAttackControllers != null)
        {
            foreach (var raccoon in raccoonAttackControllers)
            {
                if (raccoon != null)
                    raccoon.RestoreObstacle();
            }
        }

        if (trapBlocks != null)
        {
            foreach (var trap in trapBlocks)
            {
                if (trap != null)
                    trap.RestoreObstacle();
            }
        }

        if (stepActivatedPlatforms != null)
        {
            foreach (var block in stepActivatedPlatforms)
            {
                if (block != null)
                    block.RestoreObstacle();
            }
        }

        if (closingWallControllers != null)
        {
            foreach (var wall in closingWallControllers)
            {
                if (wall != null)
                    wall.RestoreObstacle();
            }
        }
    }
}


