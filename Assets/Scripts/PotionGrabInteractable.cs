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

        //rb.isKinematic = false;  

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
            movementType = MovementType.Instantaneous;

            // 매직포켓 자식으로 재설정
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f); // 포켓을 찾기 위해 작은 반경을 설정합니다.
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("MagicPocket"))
                {
                    Transform pocketWorld = hitCollider.transform.Find("PocketWorld");
                    if (pocketWorld != null)
                    {
                        isInPocket = true;
                        transform.SetParent(pocketWorld); // 포켓월드의 자식으로 설정합니다.
                        break;
                    }
                }
            }
        }
        else
        {
            rb.isKinematic = false;
            movementType = MovementType.VelocityTracking;

            // 네임태그를 갖고 있는 게임 오브젝트를 찾아서 활성화
            Transform nameTag = gameObject.transform.Find("NameTag");
            if (nameTag != null)
            {
                nameTag.gameObject.SetActive(true);
            }
        }
    }   
}
