using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("방망이 되돌아가기")]
    public Transform cauldron; 
    private Transform stickPos; 
    public float maxDistance = 2.0f; 
    private bool isGrabbed = false;

    private void Awake()
    {
        stickPos = transform;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, cauldron.position);
            if (distance > maxDistance)
            {
                transform.position = stickPos.position;
                transform.rotation = stickPos.rotation;
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
