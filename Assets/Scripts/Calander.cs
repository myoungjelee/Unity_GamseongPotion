using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calander : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public GameObject Bed_UI;
    private float duration = 4.1f;

    void Start()
    {
        dateText.text = $"DAY {GameManager.Instance.date}";


        if (GameManager.Instance.currentState == GameManager.State.CanSleep || GameManager.Instance.currentState == GameManager.State.Sleeping)
        {
            StartCoroutine(MorningSceneRoutine());
        }
        else
        {
            Bed_UI.SetActive(true);
        }
    }

    IEnumerator MorningSceneRoutine()
    {
        yield return new WaitForSeconds(duration);

        Bed_UI.SetActive(true);
    }
}
