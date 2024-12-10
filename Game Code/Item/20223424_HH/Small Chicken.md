# 작은 닭 이벤트
## 기능
플레이어가 장애물에 충돌할 때 10% 확률로 '작은 닭' 이벤트가 발생한다. 해당 이벤트 발생 시 플레이어는 리셋되지 않고, 일정 시간 동안 쿨다운 타이머가 적용된다. 쿨다운이 끝나면, 플레이어는 다시 스페셜 이벤트를 발생시킬 수 있다.

## 코드
`PlayerController` 스크립트에서 작은 닭 이벤트 발생 부분만 추출
```C++
private bool isEventTriggered = false;
public float eventCooldownTime = 1.0f;

private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Hurdle"))
    {
        TrySpecialEvent();
    }
}

private void TrySpecialEvent()
{
    if (isEventTriggered) return;  

    if (Random.value <= 0.1f)  
    {
        TriggerSpecialEvent();
    }
    else
    {
        ResetPlayer();  
    }
}

private void TriggerSpecialEvent()
{
    isEventTriggered = true;
    Debug.Log("Special Event Triggered! Player does not reset.");

    StartCoroutine(ResetEventCooldown());  
}

private IEnumerator ResetEventCooldown()
{
    yield return new WaitForSeconds(eventCooldownTime); 
    isEventTriggered = false; 
    Debug.Log("Event cooldown finished. Player can trigger event again.");
}
```

## 주요 변수
- *isEventTriggered* : 스페셜 이벤트가 이미 발생했는지 여부를 추적하는 변수
- *eventCooldownTime* : 스페셜 이벤트 발생 후, 다시 이벤트를 발생시킬 수 있는 쿨다운 시간을 설정하는 변수

## 주요 로직
1. `OnCollisionEnter` : 충돌 처리
   - 플레이어가 장애물과 충돌하면, `TrySpecialEvent()` 메소드가 호출되어 스페셜 이벤트를 시도함
2. `TrySpecialEvent()` : 이벤트 시도
   - 만약 `isEventTriggered`가 `true`라면, 이미 이벤트가 발생한 상태이므로 더 이상 이벤트를 시도하지 않음
   - `Random.value <= 0.1f` 조건을 통해 10% 확률로 스페셜 이벤트를 발생시키고, 그 외의 경우에는 플레이어를 리셋함
3. `TriggerSpecialEvent()` : 스페셜 이벤트 발생
   - 스페셜 이벤트가 발생하면 `isEventTriggered`를 `true`로 설정하고, 콘솔에 "Special Event Triggered!" 메시지를 출력함
   - `ResetEventCooldown()` 메소드를 호출하여 이벤트 쿨다운을 시작함
4. `ResetEventCooldown()` : 쿨다운 처리
   - `eventCooldownTiem`만큼 기다린 후, `isEventTriggered`를 `fasle`로 설정하여 플레이어가 다시 이벤트를 발생시킬 수 있도록 함
   - 쿨다운이 끝났음을 로그로 출력

## 작동 흐름
1. 플레이어가 장애물에 충돌
   - 플레이어가 장애물과 충돌하면 `OnCollisionEnter` 함수가 호출됨
2. 스페셜 이벤트 시도
   - `OnCollisionEnter`에서 `TrySpecialEvent()` 함수가 호출됨
   - `isEventTriggered`가 `false`이면 이벤트를 시도함
3. 스페셜 이벤트 발생 여부 결정
   - 조건을 통해 10% 확률로 스페셜 이벤트 발생
     - 만약 10% 확률이 맞다면, `TriggerSpecialEvent()` 함수가 호출되고,
     - `isEventTriggered`가 `true`로 설정되어 이벤트가 활성화됨
   - 90% 확률로는 플레이어가 리셋됨
4. 스페셜 이벤트 트리거
   - 이벤트가 발생하면, `ResetEventCooldown()` 함수가 호출되어 쿨다운이 시작됨
5. 쿨다운 처리
   - `eventCooldownTime`만큼 기다린 후, `isEventTriggered`를 `false`로 설정함
   - 이렇게하면, 쿨다운이 끝난 후, 플레이어가 다시 이벤트를 발생시킬 수 있음
6. 플레이어가 다시 장애물에 충돌
   - `isEventTriggered`가 `false`로 초기화되면, 플레이어는 다시 10% 확률로 스페셜 이벤트를 발생시킬 수 있음
