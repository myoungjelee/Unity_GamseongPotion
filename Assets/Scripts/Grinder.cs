using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    public Transform grinder; // 절구통의 Transform
    public Transform batPos; // 절구방망이가 돌아갈 위치 (절구 옆)
    public float maxDistance = 2.0f; // 절구통에서 벗어날 최대 거리
    private bool isGrabbed = false;

    // VR 그랩 이벤트 처리
    public void OnGrab()
    {
        isGrabbed = true;
    }

    public void OnRelease()
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.position);
            if (distance > maxDistance)
            {
                // 절구방망이가 절구통에서 너무 멀어지면 절구 옆으로 이동
                transform.position = batPos.position;
                transform.rotation = batPos.rotation;
            }
        }
    }
}
