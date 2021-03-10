using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    public AudioSource changeAudioSource;
    private AudioSource audioSource;
    public AudioClip[] audioLevelStart;
    public AudioClip[] audioChangeImLevel;
    private int levelIndex;
    private AudiFade audioFade;
    private bool wasInGame;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        //levelIndex = 0;
        audioFade = GetComponent<AudiFade>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioLevelStart[0];
        audioSource.Play();
	}
	
	// Update is called once per frame
	void OnEnable () {

        SceneManager.sceneLoaded += levelLoad;

	}

    public void levelLoad(Scene scene, LoadSceneMode mode)
    {
        
        int level = scene.buildIndex;
        if(scene.name != "Fade" && level !=0 && level !=9 && level != 11) 
        {
            audioFade.gameObject.GetComponent<AudioSource>().loop = true;
            //levelIndex++;
            audioSource.clip = audioLevelStart[level];
            //audioSource.Play() ;
            StartCoroutine(audioFade.FadeAudioIn(audioSource,false));
            wasInGame = true;
        }

        if (level == 0 && wasInGame)
        {
            audioFade.gameObject.GetComponent<AudioSource>().loop = true;
            audioSource.clip = audioLevelStart[level];
            StartCoroutine(audioFade.FadeAudioIn(audioSource, false));
        }

        if (level == 1 || level == 5)
        {
            audioFade.gameObject.GetComponent<AudioSource>().loop = false;
            StartCoroutine(FadeOutAfterTime());
        }

        if (scene.name == "Fade")
        {
            StartCoroutine(audioFade.FadeAudioOut(audioSource,false));
            StartCoroutine(audioFade.FadeAudioOut(changeAudioSource, false));
        }

    }

    public IEnumerator FadeOutAfterTime()
    {
        while ((audioFade.gameObject.GetComponent<AudioSource>().clip.length-2) > audioFade.gameObject.GetComponent<AudioSource>().time)
        {
            yield return null;
        }
        StartCoroutine(audioFade.FadeAudioOut(audioFade.gameObject.GetComponent<AudioSource>(), false));
    }

}
