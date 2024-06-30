using UnityEngine;

public class GazeObjectName : MonoBehaviour
{
    public Transform mainCamera;
    public float maxDistance = 10f;
    public LayerMask layerMask;  // 필요한 경우 레이어 마스크 추가

    void Update()
    {
        if (mainCamera == null) return;

        // 레이캐스트의 시작 위치와 방향 설정
        Vector3 rayOrigin = mainCamera.transform.position;
        Vector3 rayDirection = mainCamera.transform.forward;

        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        // 디버그 라인 그리기
        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * maxDistance, Color.red);

        // 트리거 콜라이더와의 충돌을 포함하여 레이캐스트
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }
}
