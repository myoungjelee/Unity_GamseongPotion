using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicPocket : MonoBehaviour
{
    private GameObject world;


    private void Awake()
    {
        world = GameObject.Find("World");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            other.transform.SetParent(world.transform);
            other.gameObject.layer = 6;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
