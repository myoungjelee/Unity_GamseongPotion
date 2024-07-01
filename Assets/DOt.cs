using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();
    }

}
