using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiFade : MonoBehaviour {

    public float fadeIn;
    public float fadeOut;
    public bool fadeIn_running;
    public bool fadeOut_running;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeAudioOut(AudioSource Music,bool ueberlappen)
    {
        if (!Music.isPlaying)
        {
            Debug.Log(Music);
            yield break;
        }

        if (fadeIn_running&&!ueberlappen)
        {
            while (fadeIn_running)
            {
                yield return new WaitForEndOfFrame();
            }
            fadeOut_running = true;
            float startVolume = Music.volume;

            while (Music.volume > 0)
            {
                Music.volume -= startVolume * Time.deltaTime / fadeOut;
                yield return null;
            }
            Music.Stop();
            Music.volume = 0;
            fadeOut_running = false;
            StopCoroutine(FadeAudioOut(Music,ueberlappen));
        }
        else
        {
            fadeOut_running = true;
            float startVolume = Music.volume;

            while (Music.volume > 0)
            {
                Music.volume -= startVolume * Time.deltaTime / fadeOut;
                yield return null;
            }
            Music.Stop();
            Music.volume = 0;
            fadeOut_running = false;
            StopCoroutine(FadeAudioOut(Music,ueberlappen));
        }
        
    }

    public IEnumerator FadeAudioIn(AudioSource Music, bool ueberlappen)
    {
        if (fadeOut_running&&!ueberlappen)
        {
            while (fadeOut_running)
            {
                yield return new WaitForEndOfFrame();
            }
            fadeIn_running = true;
            float startVolume = 0.2f;
            Music.volume = 0;
            Music.Play();

            while (Music.volume < 1f)
            {
                Music.volume += startVolume * Time.deltaTime / fadeIn;
                yield return null;
            }
            Music.volume = 1f;
            fadeIn_running = false;
            StopCoroutine(FadeAudioIn(Music,ueberlappen));

        }
        else
        {
            fadeIn_running = true;
            float startVolume = 0.2f;
            Music.volume = 0;
            Music.Play();

            while (Music.volume < 1f)
            {
                Music.volume += startVolume * Time.deltaTime / fadeIn;
                yield return null;
            }
            Music.volume = 1f;
            fadeIn_running = false;
            StopCoroutine(FadeAudioIn(Music,ueberlappen));
        }
        
    }

}
