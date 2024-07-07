using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;



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
        BeAwake,          // 깨어있는 상태
        Sleeping,       // 자는 중 상태
        CanSleep        // 잘 수 있는 상태
    }

    [Header("상태변환")]
    public State currentState;
    public int customerCount;

    [Header("저장데이터")]
    private int totalCoin;
    public int date;

    [Header("페이드인/아웃")]
    public FadeScreen fadeScreen;
    private bool isSceneChanging;
    public CharacterController characterController;
    public GameObject locomotion;
   

    //public XRController

    [SerializeField] private TextMeshProUGUI text_CoinBank;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        date = 1;
        totalCoin = 1000;
        SetCoinText(totalCoin);

        currentState = State.CanSleep;
    }

    private void Start()
    {
        WakeUp();
        AudioManager.Instance.PlayBgm("BedRoom_Morning");
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
        SceneManager.sceneLoaded += OnMainHallLoaded;
        GoToSceneAsync("MainHall");

    }

    private void OnMainHallLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainHall")
        {
            // PlayerStart라는 이름의 게임 오브젝트를 찾음
            GameObject playerStart = GameObject.Find("PlayerStart");
            if (playerStart != null)
            {
                // 플레이어의 위치와 회전 값을 PlayerStart 오브젝트의 위치와 회전 값으로 설정
                gameObject.transform.position = playerStart.transform.position;
                gameObject.transform.rotation = playerStart.transform.rotation;
                //fadeScreen.FadeIn();
                isSceneChanging = false; // 플래그 설정
            }
            else
            {
                Debug.LogWarning("PlayerStart 오브젝트를 찾을 수 없습니다.");
            }
        }

        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnMainHallLoaded;
    }

    public void GoToBedRoom()
    {
        switch (currentState)
        {
            case State.BeAwake:
            case State.Sleeping:
                DOTween.KillAll();
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                GoToSceneAsync("BedRoom_Morning");
                break;

            case State.CanSleep:
                DOTween.KillAll();
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                GoToSceneAsync("BedRoom_Night");
                break;
        }
    }

    private void OnBedRoomLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("BedRoom"))
        {
            GameObject playerStart;
            switch (currentState)
            {
                case State.BeAwake:
                case State.CanSleep:
                    // PlayerStart라는 이름의 게임 오브젝트를 찾음
                    playerStart = GameObject.Find("PlayerStart_BeAwake");
                    if (playerStart != null)
                    {
                        // 플레이어의 위치와 회전 값을 PlayerStart 오브젝트의 위치와 회전 값으로 설정
                        transform.position = playerStart.transform.position;
                        transform.rotation = playerStart.transform.rotation;
                        //fadeScreen.FadeIn();
                        isSceneChanging = false; // 플래그 설정
                    }
                    else
                    {
                        Debug.LogWarning("PlayerStart_BeAwake 오브젝트를 찾을 수 없습니다.");
                    }
                    break;

                case State.Sleeping:
                    // PlayerStart라는 이름의 게임 오브젝트를 찾음
                    playerStart = GameObject.Find("PlayerStart_Bed");
                    if (playerStart != null)
                    {
                        // 플레이어의 위치와 회전 값을 PlayerStart 오브젝트의 위치와 회전 값으로 설정
                        transform.position = playerStart.transform.position;
                        transform.rotation = playerStart.transform.rotation;
                        WakeUp();
                        isSceneChanging = false; // 플래그 설정
                    }
                    else
                    {
                        Debug.LogWarning("PlayerStart_Bed 오브젝트를 찾을 수 없습니다.");
                    }
                    break;
            }
        }

        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnBedRoomLoaded;
    }

    public void GoToNextDay()
    {
        SceneManager.sceneLoaded += OnBedRoomLoaded;
        GoToSceneAsync("BedRoom_Morning");
        SetCalendar();
    }

    public void SetCalendar()
    {
        date++;
        GameManager.Instance.customerCount = 0;
    }

    public void GoToSceneAsync(string sceneName)
    {
        if(!isSceneChanging)
        {
            StartCoroutine(SceneChangeAsync(sceneName));
           // Debug.Log("씬 전환");
        }
    }

    IEnumerator SceneChangeAsync(string sceneName)
    {
        isSceneChanging = true; // 플래그 설정

        AudioManager.Instance.StopAllSfx();
        locomotion.SetActive(false);
        DOTween.KillAll();
        fadeScreen.FadeOut();
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while(timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
        AudioManager.Instance.PlayBgm(sceneName);
        fadeScreen.FadeIn();
        locomotion.SetActive(true);
    }

    void WakeUp()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(1);
        locomotion.SetActive(false);
        fadeScreen.FadeIn();

        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        fadeScreen.FadeOut();

        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        fadeScreen.FadeIn();

        transform.position = new Vector3(-0.5f, 0, -2.3f);
        transform.rotation = Quaternion.identity;
        currentState = State.BeAwake;
        locomotion.SetActive(true);
    }

    public void GoToEnding()
    {
        SceneManager.sceneLoaded += OnEndingLoaded;
        GoToSceneAsync("Ending_Credit");
    }

    void OnEndingLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Ending"))
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            characterController.enabled = false;
            locomotion.SetActive(false);

            DOTweenAnimation Anim = FindFirstObjectByType<DOTweenAnimation>();
            if(Anim != null )
            {
                Anim.RecreateTweenAndPlay();
            }
            else
            {
                Debug.Log("애니메이션 못찾음");
            }
        }
    }
}
