using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrindingBat : MonoBehaviour
{
    [Header("방망이 되돌아가기")]
    private GameObject grinderObject; // 절구통의 Transform
    private Grinder grinder; // 
    private Vector3 originPos; // 방망이 처음 위치
    private Quaternion originRot; // 방망이 처음 회전값
    private float maxDistance = 1.2f; // 절구통에서 벗어날 최대 거리
    private bool isGrabbed = false;

    [Header("햅틱")]
    public float amplitude = 0.5f;      // 진동의 강도
    public float duration = 0.1f;
    private XRBaseInteractable target;

    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
        target = GetComponent<XRBaseInteractable>();

        // 하이어라키에서 "Grinder" 태그를 가진 오브젝트를 찾음
        grinderObject = GameObject.FindWithTag("Grinder");
        grinder = grinderObject.GetComponent<Grinder>();
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinderObject.transform.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }

    private void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("_2")) return;

        if ((collision.gameObject.CompareTag("Herb") || collision.gameObject.CompareTag("HerbPowder")) && grinder.isInSide)
        {
            if (target == null) return;
            if (target.firstInteractorSelecting == null) return;
            if (!(target.firstInteractorSelecting is XRBaseControllerInteractor)) return;

            var interactor = target.firstInteractorSelecting as XRBaseControllerInteractor;

            if (interactor.xrController == null) return;

            interactor.xrController.SendHapticImpulse(amplitude, duration);

            if(collision.gameObject.name.Contains("Garnet") || collision.gameObject.name.Contains("Crystal"))
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Crystal);
            }
            else
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Herb);
            }

            
        }
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
