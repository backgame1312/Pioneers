- 유저가 점프하면 위에 블럭이 생겨 점프를 방해함
    - 낭떠러지 도달 직전 트리거 영역 지정
    - 플레이어가 트리거 영역에 도달하면 블럭 생성 이벤트 실행
    - 플레이어 점프 예상 경로에 블럭 생성

```C++
public GameObject blockPrefab; // 장애물 블럭 프리팹
public Transform spawnPoint; // 빈 게임오브젝트로 생성 후 원하는 위치로 지정
```

```C++
private void OnTriggerEnter(Collider other) {
	if (other.CompareTag("Player")) // 플레이어 Tag 설정해야함, 플레이어가 트리거에 진입했을 때 실행되도록
	{
		Instantiate(blockPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}

// 빈 오브젝트를 생성하고 위치를 점프 직전 구간으로 조정
// Box Collider, Is Trigger 설정
```