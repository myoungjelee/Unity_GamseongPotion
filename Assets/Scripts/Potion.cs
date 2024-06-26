using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionData
{
    public string name;
    public int id;
    public Color color = Color.white;
    public int sellPrice;
}

public class Potion : MonoBehaviour
{
    [Header("포션 데이터")]
    public PotionData data;
}
