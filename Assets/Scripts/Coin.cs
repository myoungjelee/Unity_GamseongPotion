using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Coin : MonoBehaviour
{
    public int coin;

    public GameObject coinUI;
    public TextMeshProUGUI coinText;
    public MeshRenderer meshRenderer;



    public void GetCoin()
    {
        meshRenderer.enabled = false;
        coinUI.SetActive(true);
        coinText.text = $"+{coin}";
        GameManager.Instance.AddCoins(coin);
        coinUI.transform.DOMoveY(1.5f, 1).OnComplete(() => Destroy(gameObject));
    }
}
