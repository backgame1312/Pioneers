# 점프 약화 아이템
## 기능
플레이어가 특정 트리거에 진입하면 점프력이 약화된다. 약화 상태는 점프를 통해 회복할 수 있으며, 플레이어가 리셋되거나 특정 조건을 만족하면 점프력이 원래대로 복구된다.

## 코드
PlayerController 스크립트에서 점프가 약화되는 코드만 따로 추출
```C++
[Header("Set Dynamically")]
public bool isWeaken = false;
public bool jumpToRecover = false;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Weaken"))
    {
        if (!isWeaken)
        {
            isWeaken = true;
            jumpForce -= 3.0f;
            jumpToRecover = false;
            Destroy(other.gameObject);
        }
    }
}

private void RecoverFromSlow()
{
    jumpForce += 3.0f;
    isWeaken = false;
    jumpToRecover = true;
}

private void ResetPlayer()
{
    if (isWeaken)
    {
        RecoverFromSlow();
    }
}
```

## 주요 변수
- *isWeaken* : 플레이어가 약화 상태인지 나타내는 불리언 변수
  - `true` : 약화 상태
  - `false` : 정상 상태
- *jumpForce* : 점프의 힘을 제어하는 변수이며, 약화 시 점프력이 감소함
- *jumpToRecover* : 점프후 약화 상태가 복구되었는지 나타내는 변수로, 약화 회복 시 `true`로 설정됨

## 주요 로직
1. 약화상태 진입
   - 위치 : `OnTriggerEnter(Collider other)`
   - 조건 : 플레이어가 "Weaken" 태그를 가진 오브젝트와 충돌하면 약화 상태가 활성화됨
   - 효과
     - `isWeakend = true`로 설정됨
     - `jumpForce`가 3 감소
     - 약화를 유발한 오브젝트 삭제
2. 약화 상태 회복
   - 위치 : `RecoverFromSlow()`
   - 조건 : 플레이어가 점프를 통해 약화를 회복하려고 시도하ㅓ나, 리셋 이벤트에서 호출됨
   - 효과
     - 감소된 `jumpForce`가 원래 값으로 복구됨
     - `isWeaken = false`로 설정되어 정상 상태로 전환됨
     - `jumpToRecover = true`로 설정됨

## 작동 흐름
1. 플레이어가 "Weakend" 트리거에 진입
   - 약화 상태가 활성화되고, `jumpForce`가 3만큼 감소
2. 플레이어가 점프
   - 점프 시 `RecoverFromSlow()`가 호출되어 약화 상태를 해제하고 `jumpForce`를 복구함
3. 리셋 이벤트 발생
   - `ResetPlayer()`에서 `RecoverFromSlow()`를 호출하여 약화 상태를 강제로 초기화
