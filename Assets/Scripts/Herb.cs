using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HerbData
{
    public string name;
    public int id;
    public Mesh mesh;
    public Color color = Color.white;
    public int usePrice;
}

public class Herb : MonoBehaviour
{
    public HerbData data;
}
