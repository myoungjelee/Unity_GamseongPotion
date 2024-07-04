using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calander : MonoBehaviour
{
    public TextMeshProUGUI dateText;

    void Start()
    {
        dateText.text = $"DAY {GameManager.Instance.date}";
        //Debug.Log($"¿À´Ã ³¯Â¥ : {GameManager.Instance.date}");
    }
}
