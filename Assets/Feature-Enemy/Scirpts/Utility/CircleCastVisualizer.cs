using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCastVisualizer : MonoBehaviour
{
    public float radius = 5f;      // CircleCast의 반지름
    public float distance = 1f;     // CircleCast의 최대 거리
    public Vector2 direction = Vector2.right; // CircleCast 방향
    public LayerMask layerMask;      // 충돌 감지 레이어 마스크
    public Color gizmosColor = Color.green; // Gizmos 색상



    // 씬 뷰에 Gizmos를 그리는 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor; // Gizmos 색상 설정

        // 1. 시작 위치에 원 그리기
        Gizmos.DrawWireSphere(transform.position, radius);

        // 2. 방향 선 그리기 (CircleCast의 방향과 거리 표시)
        Vector3 castDirection = transform.TransformDirection(direction); // 로컬 방향을 월드 방향으로 변환
        Vector3 endPoint = transform.position + (Vector3)castDirection.normalized * distance;
        Gizmos.DrawRay(transform.position, castDirection.normalized * distance);


        // 3.  최대 거리 끝에 원 그리기 (CircleCast가 도달하는 최대 범위 표시)
        Gizmos.DrawWireSphere(endPoint, radius);

        // (선택 사항) CircleCast 경로를 따라 점선으로 원을 여러 개 그려서 이동 경로를 시각화할 수도 있습니다.
        // 하지만, Gizmos를 너무 많이 그리면 성능에 영향을 줄 수 있으므로 주의해야 합니다.
    }
}