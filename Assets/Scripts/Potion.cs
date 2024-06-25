using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionData
{
    public string name;
    public int id;
    public Mesh mesh;
    public Sprite sprite;
    public Color color = Color.white;
    public int sellPrice;
}

public class Potion : MonoBehaviour
{
    public PotionData data;
}
