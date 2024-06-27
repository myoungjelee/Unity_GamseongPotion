using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeLayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 충돌된 객체의 XRGrabInteractable 컴포넌트를 가져옵니다.
        XRGrabInteractable grabInteractable = other.gameObject.GetComponent<XRGrabInteractable>();

        // XRGrabInteractable 컴포넌트가 존재하면 레이어를 변경합니다.
        if (grabInteractable != null)
        {
            other.gameObject.layer = 0;
            Debug.Log($"레이어가 {LayerMask.LayerToName(0)}로 변경되었습니다.");
        }
        else
        {
            Debug.Log("그랩 없음");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌된 객체의 XRGrabInteractable 컴포넌트를 가져옵니다.
        XRGrabInteractable grabInteractable = other.gameObject.GetComponent<XRGrabInteractable>();

        // XRGrabInteractable 컴포넌트가 존재하면 레이어를 변경합니다.
        if (grabInteractable != null)
        {
            other.gameObject.layer = 6;
            Debug.Log($"레이어가 {LayerMask.LayerToName(6)}로 변경되었습니다.");
        }
        else
        {
            Debug.Log("그랩 없음");
        }
    }
}
