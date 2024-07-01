using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("방망이 되돌아가기")]
    public Transform grinder; // 절구통의 Transform
    private Vector3 originPos; // 방망이 처음 위치
    private Quaternion originRot; // 방망이 처음 회전값
    private float maxDistance = 1.5f; // 절구통에서 벗어날 최대 거리
    private bool isGrabbed = false;

    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.transform.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }

    private void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void OnGrab()
    {
        isGrabbed = true;
    }

    public void UnGrab()
    {
        isGrabbed = false;
    }
}
