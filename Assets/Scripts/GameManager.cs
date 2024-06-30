using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int totalCoins;

    public GameObject customer;

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

        StartCoroutine(TestRoutine());
    }


    IEnumerator TestRoutine()
    {
        yield return new WaitForSeconds(4);
        customer.gameObject.SetActive(true);
    }


    public void AddCoins(int amount)
    {
        totalCoins += amount;
    }

    public void SubtractCoins(int amount)
    {
        totalCoins -= amount;

    }
}
