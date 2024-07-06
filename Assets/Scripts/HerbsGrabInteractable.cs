using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HerbsGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;

    public bool isInPocket;

    public Vector3 originScale;

    [Header("조명")]
    public GameObject herbLight;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        isInPocket = false; // 초기화
        originScale = transform.localScale;

        //Debug.Log(originScale);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        rb.isKinematic = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (isInPocket)
        {
            rb.isKinematic = true;
            if (herbLight != null)
            {
                herbLight.SetActive(false);
            }
        }
        else
        {
            rb.isKinematic = false;
            transform.localScale = originScale;
            if (herbLight != null)
            {
                herbLight.SetActive(true);
            }
        }    
    }
}