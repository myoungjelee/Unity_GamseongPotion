using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("싱글톤")]
    private static GameManager instance;
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

    public enum State
    {
        Awake,          // 깨어있는 상태
        Sleeping,       // 자는 중 상태
        CanSleep        // 잘 수 있는 상태
    }

    [Header("상태변환")]
    private State currentState;

    [Header("저장데이터")]
    private int totalCoin;
    private int date;

    [SerializeField]private TextMeshProUGUI text_CoinBank;

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

        totalCoin = 1000;
        SetCoinText(totalCoin);

        currentState = State.Sleeping;
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

    public void GoToMainHall()
    {
        SceneManager.LoadScene("MainHall");
    }

    public void GoToBedRoom()
    {
        switch (currentState)
        {
            case State.Awake:
                SceneManager.LoadScene("Start Scenes");
                break;
            case State.CanSleep:
                SceneManager.LoadScene("Start Scenes");
                break;
            default:
                break;
        }
    }
}
