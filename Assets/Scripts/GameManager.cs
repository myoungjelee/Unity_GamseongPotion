using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int totalCoin;

    public GameObject customer;

    public TextMeshProUGUI text_CoinBank;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private int date;
    private bool isSleep;
    private int money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        text_CoinBank = GameObject.Find("Text_CoinBank").GetComponent<TextMeshProUGUI>();

        totalCoin = 1000;
        SetCoinText(totalCoin);

        StartCoroutine(TestRoutine());
    }


    IEnumerator TestRoutine()
    {
        yield return new WaitForSeconds(4);
        customer.gameObject.SetActive(true);
    }


    public void AddCoins(int amount)
    {
        totalCoin += amount;
        SetCoinText(totalCoin);
    }

    public void SubtractCoins(int amount)
    {
        totalCoin -= amount;
        SetCoinText(totalCoin);
    }

    private void SetCoinText(int coin)
    {
        text_CoinBank.text = $"{coin}\u20A9";
    }
}
