using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Herb") || collision.gameObject.CompareTag("HerbPowder"))
        //{
        //    Debug.Log("«„∫Í ¥Í¿Ω");
        //    Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        //    Collider collider = collision.gameObject.GetComponent<Collider>();

        //    rb.isKinematic = true;
        //    collider.isTrigger = true;
        //}
    }
}
