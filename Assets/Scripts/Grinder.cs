using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    public bool isInSide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder"))
        {
            isInSide = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder"))
        {
            isInSide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder"))
        {
            isInSide = false;
        }
    }
}
