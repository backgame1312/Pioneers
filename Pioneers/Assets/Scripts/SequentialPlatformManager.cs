using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialPlatformController : MonoBehaviour
{
    [Header("List of platforms")]
    public List<GameObject> platforms;

    [Header("Timing Settings")]
    public float disableTime = 1.5f;       // 플랫폼이 사라져 있는 시간
    public float intervalBetween = 0.5f;   // 다음 플랫폼으로 넘어가기 전 대기 시간

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

                // 플랫폼 비활성화
                platform.SetActive(false);

                // 일정 시간 뒤 다시 활성화
                StartCoroutine(ReEnablePlatform(platform, disableTime));

                // 다음 플랫폼 처리까지 대기
                yield return new WaitForSeconds(intervalBetween);
            }

            // 한 바퀴 돌고 잠깐 대기
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ReEnablePlatform(GameObject platform, float waitTime)
    {
        // 일정 시간 뒤 플랫폼 다시 활성화
        yield return new WaitForSeconds(waitTime);
        platform.SetActive(true);
    }
}
