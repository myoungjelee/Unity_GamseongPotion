using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionData
{
    public string name;
    public int sellPrice;
}

public class Potion : MonoBehaviour
{
    [Header("포션 데이터")]
    public PotionData data;

    [Header("이름표")]
    public GameObject nameTag;

    // 호버 시작 시 호출되는 메서드
    public void OnHoverEnter()
    {
        nameTag.SetActive(true);
    }

    // 호버 종료 시 호출되는 메서드
    public void OnHoverExit()
    {
        nameTag.SetActive(false);
    }
}
