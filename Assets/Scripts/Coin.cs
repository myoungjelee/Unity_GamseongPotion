using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coin;

    public void GrabCoin()
    {
        GameManager.Instance.AddCoins(coin);
        Destroy(gameObject, 1);
    }
}
