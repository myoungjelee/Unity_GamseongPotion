using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Image image;
    //private Renderer rend;
    //public Animator animator;

    private void Start()
    {
        //rend = GetComponent<Renderer>();
        image = GetComponent<Image>();
        FadeIn();
        //FadeOut();
    }

    public void FadeIn()
    {
        Fade(1, 0);
        //Debug.Log("페이드인");

    }

    public void FadeOut()
    {
        Fade(0, 1);
        //Debug.Log("페이드아웃");
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            image.color = newColor;

            timer += Time.deltaTime;
            yield return null;     // 한 프레임만 기다리기
        }
    }
}
