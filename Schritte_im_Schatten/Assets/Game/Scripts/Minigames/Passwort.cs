using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class Passwort : MonoBehaviour {

    bool pause = false;
    public string passwort;
    public Image input_image;
    public InputField input_norm;
    public Sprite[] sprites;
    private bool correct;
    public Image popUp;
    private VideoController vidcont;
    private UI_S3 ui;
    public Image Archiv;
    private Text subText;


    private void Start()
    {
        popUp.gameObject.SetActive(false);
        Archiv.gameObject.SetActive(false);
        vidcont = Camera.main.GetComponent<VideoController>();
        ui = Camera.main.GetComponent<UI_S3>();
        subText = VideoController.Instance.subtitlesUI.gameObject.GetComponent<Text>();
        gameObject.GetComponent<AudioSource>().PlayDelayed(3);
        if (Einstellungen.Instance.currentSubtitle)
        {
            StartCoroutine(startSubDelay());
        }
        
    }

    private void Update()
    {
        if (popUp.gameObject.activeInHierarchy && input_norm.isFocused)
        {
            popUp.gameObject.SetActive(false);
            input_image.sprite = sprites[0];
        }

        if (BGMusic.Instance.gameIsPaused && !pause)
        {
            pause = true;
            gameObject.GetComponent<AudioSource>().Pause();
        }

        if (!BGMusic.Instance.gameIsPaused && pause)
        {
            pause = false;
            gameObject.GetComponent<AudioSource>().UnPause();
        }
    }

    IEnumerator startSubDelay()
    {
        yield return new WaitForSeconds(2.5f);
        subText.text = LocalizationManager.GetTranslation("Subtitle/" + gameObject.GetComponent<AudioSource>().clip.name);
        while (gameObject.GetComponent<AudioSource>().isPlaying || pause)
        {
            yield return new WaitForEndOfFrame();
        }
        subText.text = "";
        StopCoroutine(startSubDelay());
    }

    public void checkPasswort(string check)
    {
        if(check == passwort)
        {
            correct = true;
        }
    }

    public void Login()
    {
        if (correct)
        {
            Debug.Log("correct");
            ui.minigame = true;
            Archiv.gameObject.SetActive(true);

        }
        else
        {
            popUp.gameObject.SetActive(true);
            input_image.sprite = sprites[1];
            input_norm.text = "";
        }
    }

    public void back()
    {
        vidcont.SetSelectedOption(16);
    }

}
