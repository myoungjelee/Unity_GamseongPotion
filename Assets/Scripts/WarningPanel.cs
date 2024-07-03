using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{
    public Material warningMaterial;
    private bool isFading = false;
    public TextMeshProUGUI[] warningText;
    
    private void Start()
    {
        warningMaterial.SetFloat("_FadeAmount", 1f);//시작하자마자 투명하게 만들기

        for (int i = 0; i < warningText.Length; i++)
        {
            warningText[i].DOFade(0f, 1f);
        }
    }
    private void OnTriggerEnter(Collider other) //트리거 감지했을때 나타나는거
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("트리거 감지");
            // _FadeAmount 값을 1에서 0.1로 변경 (애니메이션으로)
            if (!isFading)
            {
                warningMaterial.DOFloat(-0.1f, "_FadeAmount", 1f); // 1초 동안 애니메이션

                for (int i = 0; i < warningText.Length; i++)
                {
                    warningText[i].DOFade(1f, 1f);
                }
                isFading = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIFadeOut();
        }
    }

    public void UIFadeOut()
    {
        // _FadeAmount 값을 0.1에서 1로 변경 (애니메이션으로)
        if (isFading)
        {
            warningMaterial.DOFloat(1f, "_FadeAmount", 1f); // 1초 동안 애니메이션

        }

        isFading = false;
    }
}
