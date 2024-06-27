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

    public void OutMagicPocket()
    {
        //StartCoroutine(OutPocketRoutine());

        //gameObject.layer = 0;

        //rigid.isKinematic = false;

        //transform.SetParent(null);

        //transform.localScale = originScale;

        //Debug.Log("±×·¦¹öÆ°");
    }

    IEnumerator OutPocketRoutine()
    {
        yield return new WaitForSeconds(3f);

        gameObject.layer = 0;

        rigid.isKinematic = false;

        transform.SetParent(null);

        transform.localScale = originScale;
    }
}
