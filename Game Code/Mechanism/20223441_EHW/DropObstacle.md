- 특정 구간 트리거 영역 지정
- 플레이어가 트리거 영역에 도달하면 장애물 생성 이벤트 실행
- 트리거 발동 후 플레이어 머리 위로 생성 or 미리 배치된 장애물 낙하

public GameObject obstaclePrefab; // 장애물 프리팹
public Transform dropPoint; // 장애물이 떨어질 위치

void OnTriggerEnter(Collider other) {
	if (other.CompareTag("Player")) {
		GameObject obstacle = Instantiate(obstaclePrefab, dropPoint.position, dropPoint.rotation);
	}
}

void OnCollisionEnter(Collision collision) {
if (collision.gameObject.CompareTag("Player")) {
		GameOver(); // 게임 오버
	}
}

void GameOver() 
{
	// 게임 오버 처리 로직 작성
	// Debug.Log("Game Over!");
}

++ 장애물 낙하 속도 조절해서 재미 유발