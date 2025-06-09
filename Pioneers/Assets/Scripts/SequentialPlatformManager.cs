using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialPlatformController : MonoBehaviour
{
    [Header("List of platforms")]
    public List<GameObject> platforms;

    [Header("Timing Settings")]
    public float disableTime = 1.5f;       // �÷����� ����� �ִ� �ð�
    public float intervalBetween = 0.5f;   // ���� �÷������� �Ѿ�� �� ��� �ð�

    private void Start()
    {
        StartCoroutine(ControlPlatformsRoutine());
    }

    private IEnumerator ControlPlatformsRoutine()
    {
        while (true)
        {
            for (int i = 0; i < platforms.Count; i++)
            {
                GameObject platform = platforms[i];

                // �÷��� ��Ȱ��ȭ
                platform.SetActive(false);

                // ���� �ð� �� �ٽ� Ȱ��ȭ
                StartCoroutine(ReEnablePlatform(platform, disableTime));

                // ���� �÷��� ó������ ���
                yield return new WaitForSeconds(intervalBetween);
            }

            // �� ���� ���� ��� ���
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ReEnablePlatform(GameObject platform, float waitTime)
    {
        // ���� �ð� �� �÷��� �ٽ� Ȱ��ȭ
        yield return new WaitForSeconds(waitTime);
        platform.SetActive(true);
    }
}
