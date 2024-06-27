using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableActor : MonoBehaviour
{
    private Transform originTransform;
    private Rigidbody rigid;

    public bool isInMagicPocket;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        originTransform = transform;
    }

    public void OutMagicPocket()
    {
        StartCoroutine(OutPocketRoutine());
    }

    IEnumerator OutPocketRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.layer = 0;

        rigid.isKinematic = false;

        transform.localScale = originTransform.localScale;

        transform.SetParent(originTransform);
    }
}
