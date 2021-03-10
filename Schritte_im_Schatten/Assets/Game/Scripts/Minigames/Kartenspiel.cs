using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class Kartenspiel : MonoBehaviour {

    bool pause;

    public Sprite[] kartenSprites;
    public Image kartenImage;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private int currentSpriteIndex;
    public Text subText;


    private void Update()
    {
        if (BGMusic.Instance.gameIsPaused && !pause)
        {
            pause = true;
            audioSource.Pause();
        }

        if (!BGMusic.Instance.gameIsPaused && pause)
        {
            pause = false;
            audioSource.UnPause();
        }
    }


    public void setStart()
    {
        int rand = Random.Range(0,2);
        Debug.Log(rand);
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[rand];
            if (Einstellungen.Instance.currentSubtitle)
            {
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSource.clip.name);
            }
            
            audioSource.Play();
            StartCoroutine(hideSub());
        }
        
        //checkAudio(audioSource);


        currentSpriteIndex = 0;

        kartenImage.sprite = kartenSprites[0];

        

    }

    public void changeImage()
    {

        currentSpriteIndex += 1;

        kartenImage.sprite = kartenSprites[currentSpriteIndex];

        if (currentSpriteIndex == 19)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClips[2];
                if (Einstellungen.Instance.currentSubtitle)
                {
                    subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSource.clip.name);
                }
                audioSource.Play();
                StartCoroutine(changeScene());
            } 
            
        }


    }

    IEnumerator hideSub()
    {
        while (audioSource.isPlaying || pause)
        {
            yield return new WaitForEndOfFrame();
        }
        subText.text = "";
        StopCoroutine(hideSub());
    }

    public IEnumerator changeScene()
    {
        while (audioSource.isPlaying || pause)
        {
            yield return new WaitForEndOfFrame();
        }
        subText.text = "";
        LoadingScreenManager.LoadScene(5);
    }

	public void checkRichtung(int richtungID)
    {
        switch (currentSpriteIndex)
        {
            case 0:
                if (richtungID!=2)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 1:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 2:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 3:
                if (richtungID != 0)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 4:
                if (richtungID != 0)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 5:
                if (richtungID != 2)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 6:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 7:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 8:
                if (richtungID != 2)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 9:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 10:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 11:
                if (richtungID != 2)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 12:
                if (richtungID != 2)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 13:
                if (richtungID != 0)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 14:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 15:
                if (richtungID != 0)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 16:
                if (richtungID != 1)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 17:
                if (richtungID != 2)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
            case 18:
                if (richtungID != 0)
                {
                    setStart();
                }
                else
                {
                    changeImage();
                }
                break;
        }
    }

}
