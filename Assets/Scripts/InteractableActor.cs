using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableActor : MonoBehaviour
{
    private Vector3 originScale;
    private Rigidbody rigid;

    public bool isInMagicPocket;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        originScale = transform.localScale;

        Debug.Log($"{originScale}");
    }

    public void PocketUnGrab()
    {
        if( isInMagicPocket )
        {
            Debug.Log("Æ÷ÄÏ ¾ð±×·¦");
            gameObject.layer = 6;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void PocketGrab()
    {
        if( isInMagicPocket )
        {
            gameObject.layer = 0;

            //transform.localScale = originScale;

            rigid.useGravity = true;

            rigid.isKinematic = false;
        }
    }

}
