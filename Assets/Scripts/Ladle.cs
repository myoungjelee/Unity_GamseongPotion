using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladle : MonoBehaviour
{
    [Header("방망이 되돌아가기")]
    public Transform cauldron;
    private Vector3 originPos; // 방망이 처음 위치
    private Quaternion originRot; // 방망이 처음 회전값
    private float maxDistance = 1.8f; 
    public bool isGrabbed = false;

    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, cauldron.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }
    public void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
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
