using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BGMusic : MonoBehaviour {

    public static BGMusic Instance;

    public AudioMixerGroup audioMixer;
    private AudioSource musicSourceA;
    private AudioSource musicSourceB;
    public AudioClip[] audioClips;
    public bool[] hasLoop;
    public bool gameIsPaused;
    public float fadeTime;
    private int level;
    private UI_S2 s2;

    private bool wasInGame;
    public float musicVolume = 1.0f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void OnEnable()
    {

        SceneManager.sceneLoaded += levelLoad;

    }

    // Use this for initialization
    void Start () {

        musicSourceA = gameObject.AddComponent<AudioSource>();
        musicSourceB = gameObject.AddComponent<AudioSource>();
        musicSourceA.volume = musicVolume;
        musicSourceB.volume = 0.0f;
        musicSourceA.outputAudioMixerGroup = audioMixer;
        musicSourceB.outputAudioMixerGroup = audioMixer;


        if (audioClips.Length != 0)
        {
            musicSourceA.clip = audioClips[0];
            musicSourceA.loop = true;
            musicSourceA.Play();
        }
       
	}
	
    private IEnumerator CrossFade(AudioSource a, AudioSource b, float seconds)
    {
        float step_interval = seconds / 20.0f;
        float vol_interval = musicVolume / 20.0f;

        b.Play();

        for (int i = 0; i < 20; i++)
        {
            a.volume -= vol_interval;
            b.volume += vol_interval;

            yield return new WaitForSeconds(step_interval);

            
        }

        a.Stop();
    }

    public IEnumerator SwitchTrack(int trackIndex)
    {
        bool play_a = true;

        if (musicSourceB.volume == 0.0f)
        {
            play_a = false;
        }

        if (play_a)
        {
            Debug.Log("Play A");
            musicSourceA.clip = audioClips[trackIndex];
            musicSourceA.loop = hasLoop[trackIndex];
            if (musicSourceA.clip == musicSourceB.clip){ }
            else
            {
                yield return StartCoroutine(CrossFade(musicSourceB, musicSourceA, 2.0f));

            }

        }
        else
        {
            Debug.Log("Play B");
            musicSourceB.clip = audioClips[trackIndex];
            musicSourceB.loop = hasLoop[trackIndex];
            if (musicSourceA.clip == musicSourceB.clip) { }
            else
            {
                yield return StartCoroutine(CrossFade(musicSourceA, musicSourceB, 2.0f));
            }
            
        }
        

    }

    public void levelLoad(Scene scene, LoadSceneMode mode)
    {

        level = scene.buildIndex;
        int index = 0;

        switch (level)
        {
            case 0:
                index = 0;
                break;
            case 1:
                index = 1;
                break;
            case 2:
                index = 2;
                s2 = Camera.main.GetComponent<UI_S2>();
                break;
            case 3:
                index = 5;
                break;
            case 4:
                index = 8;
                break;
            case 5:
                index = 11;
                break;
            case 6:
                index = 13;
                break;
            case 7:
                index = 12;
                break;
            case 8:
                index = 16;
                break;
            case 9:
                index = 32;
                break;
            case 10:
                index = 12;
                break;
            case 11:
                index = 32;
                break;
            case 12:
                index = 23;
                break;
            case 13:
                index = 24;
                break;
            case 14:
                index = 6;
                break;
        }

        if(level != 0 && level !=15)
        {
            StartCoroutine(SwitchTrack(index));
            wasInGame = true;
        }

        if (level == 0 && wasInGame)
        {
            StartCoroutine(SwitchTrack(index));
        }
    }

    public void PauseBGM()
    {
        musicSourceA.Pause();
        musicSourceB.Pause();
        if (level == 2 && s2.ringing)
        {
            s2.telephone_audio.GetComponent<AudioSource>().Pause();
        }
        AudioManger.Instance.audioSource.Pause();
        AudioManger.Instance.audioSourceDelay.Pause();
        gameIsPaused = true;
        
    }

    public void UnPauseBGM()
    {
        musicSourceA.UnPause();
        musicSourceB.UnPause();
        if (level == 2 && s2.ringing)
        {
            s2.telephone_audio.GetComponent<AudioSource>().UnPause();
        }
        AudioManger.Instance.audioSource.UnPause();
        AudioManger.Instance.audioSourceDelay.UnPause();
        gameIsPaused = false;
        
    }

    public IEnumerator WaitForSwitch(float seconds, int index)
    {
        while(seconds > 0)
        {
            yield return new WaitForEndOfFrame();
            if (!gameIsPaused)
            {
                seconds -= Time.deltaTime;
                //Debug.Log(seconds);
            }
            
        }
        //yield return new WaitForSeconds(seconds);
        StartCoroutine(SwitchTrack(index));
    }

}
