using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HerbGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    //protected override void OnSelectEntered(SelectEnterEventArgs args)
    //{
    //    base.OnSelectEntered(args);

    //    // 그랩할 때 isKinematic을 false로 설정하여 물리 상호작용을 유지
    //    if (rb != null)
    //    {
    //        rb.isKinematic = false;
    //    }
    //}

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // 그랩을 놓을 때 isKinematic을 true로 설정
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
