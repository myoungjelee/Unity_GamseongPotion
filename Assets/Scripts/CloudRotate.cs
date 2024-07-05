using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotate : MonoBehaviour
{
    // 회전 속도 (각도/초)
    public float rotationSpeed = 1f;

    private void FixedUpdate()
    {
        // Y축을 기준으로 회전
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
