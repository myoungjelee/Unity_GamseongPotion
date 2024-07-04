using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;
    private Vector3 originalPosition; // 원래 위치를 저장할 변수

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalPosition = transform.position; // 원래 위치 저장
    }

    public void FadeIn()
    {
        Fade(1, 0);
        Debug.Log("페이드인");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.006f);
    }

    public void FadeOut()
    {
        Fade(0, 1);
        Debug.Log("페이드아웃");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.006f);
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

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;     // 한 프레임만 기다리기
        }

        // 페이드가 끝난 후 원래 위치로 되돌리기
        transform.position = originalPosition;
    }
}
