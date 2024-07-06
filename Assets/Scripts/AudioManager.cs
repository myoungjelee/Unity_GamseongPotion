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
    string[] sceneNames = { "MainHall", "BedRoom_Morning", "BedRoom_Night", "Ending_Credit" };

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelsIndex;

    private Dictionary<AudioSource, AudioClip> activeSfxClips = new Dictionary<AudioSource, AudioClip>();

    public enum Sfx { Herb, Bubble = 7, Crystal, Coin = 15, Fail, UI, Dialogue, Bell, MagicPocket }

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
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

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

        sceneBgmMap = new Dictionary<string, AudioClip>();
        if (sceneNames.Length == bgmClips.Length)
        {
            for (int i = 0; i < bgmClips.Length; i++)
            {
                sceneBgmMap.Add(sceneNames[i], bgmClips[i]);
            }
        }
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
            int loopIndex = (i + channelsIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) continue;

            int ranIndex = 0;
            if (sfx == Sfx.Herb || sfx == Sfx.Crystal)
            {
                ranIndex = UnityEngine.Random.Range(0, 7);
            }

            channelsIndex = loopIndex;
            AudioClip clipToPlay = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].clip = clipToPlay;
            sfxPlayers[loopIndex].Play();

            activeSfxClips[sfxPlayers[loopIndex]] = clipToPlay;

            break;
        }
    }

    public void StopSfx(Sfx sfxToStop)
    {
        AudioClip clipToStop = sfxClips[(int)sfxToStop];

        foreach (var kvp in activeSfxClips)
        {
            if (kvp.Value == clipToStop && kvp.Key.isPlaying)
            {
                kvp.Key.Stop();
            }
        }
    }

    public void StopAllSfx()
    {
        foreach (var sfxPlayer in sfxPlayers)
        {
            if (sfxPlayer.isPlaying)
            {
                sfxPlayer.Stop();
            }
        }
        activeSfxClips.Clear();
    }
}
