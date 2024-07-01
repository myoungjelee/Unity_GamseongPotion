using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateOnLookAt : MonoBehaviour
{
    public new Camera camera;
    public GameObject target;
    //public XRRayInteractor interactor;

    private float minThresholdAngle = 5f;
    private float maxThresholdAngle = 15f;
    private float thresholdDuration = 0f;

    private bool isLooking = false;
    private float showingTime;

    private void Awake()
    {
        target.SetActive(false);
    }

    private void Update()
    {
        var dir = target.transform.position - camera.transform.position;
        var angle = Vector3.Angle(camera.transform.forward, dir);


        if (angle >= minThresholdAngle && angle <= maxThresholdAngle)
        {
            if (!isLooking)
            {
                isLooking = true;
                showingTime = Time.time + thresholdDuration;
            }
            else
            {
                if (!target.activeSelf && Time.time >= showingTime)
                {
                    //Debug.Log(angle);
                    target.SetActive(true);
                }
            }
        }
        else
        {
            if (isLooking)
            {
                isLooking = false;
                target.SetActive(false);
            }
        }
    }
}
