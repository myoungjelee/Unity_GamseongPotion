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
        if( isInMagicPocket )
        {
            StartCoroutine(OutPocketRoutine());
        }
    }

    IEnumerator OutPocketRoutine()
    {

        gameObject.layer = 0;

        transform.SetParent(null);

        transform.localScale = originScale;

        yield return new WaitForSeconds(3f);

        rigid.isKinematic = false;

    }
}
