using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
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
    private bool isPlaying;


    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    private Dictionary<string, AudioClip> sceneBgmMap;
    // 씬 이름 배열
    string[] sceneNames = { "MainHall", "BedRoom_Morning", "BedRoom_Night", "Ending_Credit" };

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelsIndex;

    public enum Sfx { Herb, Bubble = 7, Crystal, Coin = 15, Fail, UI }
    

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

        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].bypassEffects = true;
            sfxPlayers[i].volume = sfxVolume;
        }

        // sceneBgmMap 초기화
        sceneBgmMap = new Dictionary<string, AudioClip>();
        if (sceneNames.Length == bgmClips.Length)
        {
            for (int i = 0; i < bgmClips.Length; i++)
            {
                sceneBgmMap.Add(sceneNames[i], bgmClips[i]);

            }
        }

        //foreach (var kvp in sceneBgmMap)
        //{
        //    Debug.Log($"씬 이름: {kvp.Key}, 클립 이름: {kvp.Value.name}");
        //}
    }

    public void PlayBgm(string sceneName)
    {
        StartCoroutine(BgmRoutine(sceneName));
    }

    IEnumerator BgmRoutine(string sceneName)
    {
        if (bgmPlayer.isPlaying)
        {
            bgmPlayer.Stop();
        }

        yield return new WaitForSeconds(3);

        if (sceneBgmMap.TryGetValue(sceneName, out AudioClip bgmClip))
        {
            bgmPlayer.clip = bgmClip;
            bgmPlayer.Play();
            //Debug.Log(bgmClip.name);
        }
        else
        {
            Debug.LogError($"'{sceneName}'에 해당하는 BGM을 찾을 수 없습니다.");
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            // loopIndex가 sfxPlayers.Length를 넘어가지 않게 해주는 수식
            int loopIndex = (i + channelsIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) continue;

            int ranIndex = 0;
            if (sfx == Sfx.Herb || sfx == Sfx.Crystal)
            {
                ranIndex = UnityEngine.Random.Range(0, 8);
            }

            // 사용 가능한 sfxPlayer를 찾았으므로 channelsIndex를 현재 loopIndex로 업데이트
            channelsIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();

            // 사용 가능한 sfxPlayer를 찾았으므로 반복을 종료합니다.
            break;
        }
    }

public void StopSfx(Sfx sfxToStop)
{
    foreach (var sfxPlayer in sfxPlayers)
    {
        if (sfxPlayer.isPlaying && sfxPlayer.clip == sfxClips[(int)sfxToStop])
        {
            sfxPlayer.Stop();
        }
    }
}
}
