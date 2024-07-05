using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calander : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public GameObject Bed_UI;

    void Start()
    {
        dateText.text = $"DAY {GameManager.Instance.date}";
        //Debug.Log($"¿À´Ã ³¯Â¥ : {GameManager.Instance.date}");

        if(GameManager.Instance.currentState == GameManager.State.CanSleep)
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
        yield return new WaitForSeconds(4.1f);

        Bed_UI.SetActive(true);
    }
}
