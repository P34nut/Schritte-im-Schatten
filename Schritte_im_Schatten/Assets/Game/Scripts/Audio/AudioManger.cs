using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class AudioManger : MonoBehaviour {

    public static AudioManger Instance;

    public AudioClip[] audioClips;

    public AudioSource audioSource;
    public AudioSource audioSourceDelay;

    private Text subText;


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
        subText = VideoController.Instance.subtitlesUI.gameObject.GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		


	}



    public void setAudio(int audioIndex, bool hasSubtitle)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[audioIndex];
           
            if (hasSubtitle && Einstellungen.Instance.currentSubtitle)
            {
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSource.clip.name);
            }
            audioSource.Play();
            StartCoroutine(DisableSubText());
            

        }
    }

    public void setAudioDelayed(int audioIndex, float delay, bool hasSubtitle)
    {
        if (!audioSourceDelay.isPlaying)
        {
            audioSourceDelay.clip = audioClips[audioIndex];
            if (hasSubtitle && Einstellungen.Instance.currentSubtitle)
            {
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSourceDelay.clip.name);
            }
            audioSourceDelay.PlayDelayed(delay);
            StopCoroutine(DisableSubText());
        }
    }

    IEnumerator DisableSubText()
    {
        while (audioSource.isPlaying || audioSourceDelay.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        subText.text = "";
        StopCoroutine(DisableSubText());
    }
}
