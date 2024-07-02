using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaringPanel : MonoBehaviour
{
    public Material warningMaterial;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            warningMaterial.SetFloat("_FadeAmount",-0.1f);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            warningMaterial.SetFloat("_FadeAmount", 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }


}
