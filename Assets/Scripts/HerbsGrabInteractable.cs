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

        //rb.isKinematic = false;
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

            if (herbLight != null)
            {
                herbLight.SetActive(false);
            }
        }
        else
        {
            rb.isKinematic = false;
            movementType = MovementType.VelocityTracking;
            
            if (herbLight != null)
            {
                herbLight.SetActive(true);
            }
        }    
    }
}