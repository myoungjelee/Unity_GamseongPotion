using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicPocket : MonoBehaviour
{
    private GameObject world;

    private bool isTrigger;

    private void Awake()
    {
        world = GameObject.Find("World");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder") || other.gameObject.CompareTag("Potion"))
        {
            InteractableActor item = other.gameObject.GetComponent<InteractableActor>();

            if (item.isInMagicPocket) return;

            item.gameObject.transform.SetParent(world.transform);
            item.gameObject.layer = 6;
            item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            item.isInMagicPocket = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder") || other.gameObject.CompareTag("Potion"))
    //    {
    //        InteractableActor interactableActor = other.GetComponent<InteractableActor>();
    //        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();

    //        if (interactableActor.isInMagicPocket && isTrigger)
    //        {
    //            other.transform.SetParent(null);
    //            other.gameObject.layer = 0;
    //            other.GetComponent<Rigidbody>().isKinematic = false;
    //            interactableActor.isInMagicPocket = false;
    //        }
    //    }
    //}
}
