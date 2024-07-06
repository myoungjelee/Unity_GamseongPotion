using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;

    public bool isInPocket;

    public Vector3 originScale;

    [Header("이펙트")]
    public GameObject effect;
    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        isInPocket = false;
        originScale = transform.localScale;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        rb.isKinematic = false;  

        if (!args.interactorObject.transform.CompareTag("Socket"))
        {
            effect.SetActive(false);
            audioSource.enabled = false;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (isInPocket)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
            transform.localScale = originScale;

            // 네임태그를 갖고 있는 게임 오브젝트를 찾아서 활성화
            Transform nameTag = gameObject.transform.Find("NameTag");
            if (nameTag != null)
            {
                nameTag.gameObject.SetActive(true);
            }
        }
    }   
}
