using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSnapTurn : MonoBehaviour
{
    private float snapTurnAngle = 45f;

    public void SnapTurnRight()
    {
        // 현재 회전 값을 가져옵니다.
        Quaternion currentRotation = transform.rotation;

        // Y축을 기준으로 45도 회전하는 Quaternion을 생성합니다.
        Quaternion turnRotation = Quaternion.Euler(0, snapTurnAngle, 0);

        // 현재 회전에 새로 만든 회전을 더합니다.
        transform.rotation = currentRotation * turnRotation;
    }

    public void SnapTurnLeft()
    {
        // 현재 회전 값을 가져옵니다.
        Quaternion currentRotation = transform.rotation;

        // Y축을 기준으로 -45도 회전하는 Quaternion을 생성합니다.
        Quaternion turnRotation = Quaternion.Euler(0, -snapTurnAngle, 0);

        // 현재 회전에 새로 만든 회전을 더합니다.
        transform.rotation = currentRotation * turnRotation;
    }
}
