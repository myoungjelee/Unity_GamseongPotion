using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class RayObjectName : MonoBehaviour
{
    private XRRayInteractor rayInteractor;

    void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    public void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }
}

