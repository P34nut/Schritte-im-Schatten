using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using I2.Loc;

public class ArchivSpiel : MonoBehaviour {

    bool pause = false;
    public GameObject[] texte;
    public GameObject[] buttons;
    public ButtonFilter[] bFilter;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Passwort passwort;
    private Text subText;

	// Use this for initialization
	void Start () {

        subText = VideoController.Instance.subtitlesUI.gameObject.GetComponent<Text>();

        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI text = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

            bFilter[i] = buttons[i].GetComponent<ButtonFilter>();

            switch (i)
            {
                case 0:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 0));
                    break;
                case 1:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 1));
                    break;
                case 2:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 2));
                    break;
                case 3:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 3));
                    break;
                case 4:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 4));
                    break;
                case 5:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 5));
                    break;
                case 6:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 6));
                    break;
                case 7:
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => FilterChange(text, 7));
                    break;
            }
        }

	}

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

    public void FilterChange(TextMeshProUGUI buttonText, int buttonID)
    {
   
        Debug.Log(buttonText.text);

        switch (buttonID)
        {
            case 0:
                if (bFilter[0].tag == -1)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Alltag;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 0)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Gesundheit;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 1)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Kriminalit_t;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 2)
                {
                    buttonText.text = "Lifestyle";
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 3)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Menschen;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 4)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Natur;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 5)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Politik;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 6)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Sport;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 7)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Unterhaltung;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 8)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Wirtschaft;
                    bFilter[0].tag += 1;
                }
                else if (bFilter[0].tag == 9)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Alltag;
                    bFilter[0].tag = 0;
                }
                break;
            case 1:
                if (bFilter[1].gelöst == -1)
                {
                    bFilter[1].gelöst = 0;
                    buttonText.text = ScriptLocalization.Texte.NEIN;
                }
                else if (bFilter[1].gelöst==1)
                {
                    bFilter[1].gelöst = 0;
                    buttonText.text = ScriptLocalization.Texte.NEIN;
                }
                else
                {
                    bFilter[1].gelöst = 1;
                    buttonText.text = ScriptLocalization.Texte.JA;
                }
                break;
            case 2:
                if (bFilter[2].Datum == -1)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Siebzig;
                    bFilter[2].Datum += 1;
                }
                else if (bFilter[2].Datum == 0)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Achtzig;
                    bFilter[2].Datum += 1;
                }
                else if(bFilter[2].Datum == 1)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Neunzig;
                    bFilter[2].Datum += 1;
                }
                else if (bFilter[2].Datum == 2)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Siebzig;
                    bFilter[2].Datum = 0;
                }
                break;
            case 3:
                if (bFilter[3].Entfernung == -1)
                {
                    bFilter[3].Entfernung += 1;
                    buttonText.text = "<50km";
                }
                else if (bFilter[3].Entfernung==0)
                {
                    bFilter[3].Entfernung += 1;
                    buttonText.text = "<300km";
                }
                else if(bFilter[3].Entfernung == 1)
                {
                    bFilter[3].Entfernung += 1;
                    buttonText.text = "<1000km";
                }
                else if (bFilter[3].Entfernung == 2)
                {
                    bFilter[3].Entfernung += 1;
                    buttonText.text = ">1000km";
                }
                else if (bFilter[3].Entfernung == 3)
                {
                    bFilter[3].Entfernung = 0;
                    buttonText.text = "<50km";
                }
                break;
            case 5:
                if (bFilter[5].ermittlung == -1)
                {
                    bFilter[5].ermittlung = 0;
                    buttonText.text = ScriptLocalization.Texte.NEIN;
                }
                if (bFilter[5].ermittlung == 1)
                {
                    bFilter[5].ermittlung = 0;
                    buttonText.text = ScriptLocalization.Texte.NEIN;
                }
                else
                {
                    bFilter[5].ermittlung = 1;
                    buttonText.text = ScriptLocalization.Texte.JA;
                }
                break;
            case 4:
                if (bFilter[4].last == -1)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Siebzig;
                    bFilter[4].last += 1;
                }
                else if (bFilter[4].last == 0)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Achtzig;
                    bFilter[4].last += 1;
                }
                else if (bFilter[4].last == 1)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Neunzig;
                    bFilter[4].last += 1;
                }
                else if (bFilter[4].last == 2)
                {
                    buttonText.text = ScriptLocalization.Archivspiel.Siebzig;
                    bFilter[4].last = 0;
                }
                break;
            case 6:
                if (bFilter[6].aufgearbeitet == -1)
                {
                    bFilter[6].aufgearbeitet = 0;
                    buttonText.text = ScriptLocalization.Texte.NEIN;
                }
                if (bFilter[6].aufgearbeitet == 1)
                {
                    bFilter[6].aufgearbeitet = 0;
                    buttonText.text = ScriptLocalization.Texte.NEIN;
                }
                else
                {
                    bFilter[6].aufgearbeitet = 1;
                    buttonText.text = ScriptLocalization.Texte.JA;
                }
                break;
            case 7:
                if (bFilter[7].unterhaltung == -1)
                {
                    bFilter[7].unterhaltung += 1;
                    buttonText.text = ScriptLocalization.Archivspiel.gering;
                }
                else if (bFilter[7].unterhaltung == 0)
                {
                    bFilter[7].unterhaltung += 1;
                    buttonText.text = ScriptLocalization.Archivspiel.mittel;
                }
                else if (bFilter[7].unterhaltung == 1)
                {
                    bFilter[7].unterhaltung += 1;
                    buttonText.text = ScriptLocalization.Archivspiel.hoch;
                }
                else if (bFilter[7].unterhaltung == 2)
                {
                    bFilter[7].unterhaltung = 0;
                    buttonText.text = ScriptLocalization.Archivspiel.gering;
                }
                break;

        }

        for (int i = 0; i < texte.Length; i++)
        {
            TextFilter Tfilter = texte[i].GetComponent<TextFilter>();
            
            int filterTag = (int)Tfilter.tag;

           if((bFilter[0].tag == filterTag || bFilter[0].tag == -1) &&
                (bFilter[1].gelöst == Tfilter.gelöst || bFilter[1].gelöst == -1) &&
                (bFilter[2].Datum == Tfilter.Datum || bFilter[2].Datum == -1) && 
                (bFilter[3].Entfernung == Tfilter.Entfernung || bFilter[3].Entfernung == -1) &&
                (bFilter[5].ermittlung == Tfilter.ermittlung || bFilter[5].ermittlung == -1) && 
                (bFilter[4].last == Tfilter.last || bFilter[4].last == -1) &&
                (bFilter[6].aufgearbeitet == Tfilter.aufgearbeitet || bFilter[6].aufgearbeitet == -1) &&
                (bFilter[7].unterhaltung == Tfilter.unterhaltung || bFilter[7].unterhaltung == -1))
            {
                texte[i].SetActive(true);
            }
            else
            {
                texte[i].SetActive(false);
            }

        }
    }

    public void TextClick(GameObject text)
    {

        TextFilter textFilter = text.GetComponent<TextFilter>();

        if (!audioSource.isPlaying)
        {
            if (textFilter.correct == true)
            {
                audioSource.clip = audioClips[1];

            }
            else
            {
                audioSource.clip = audioClips[0];
            }

            if (Einstellungen.Instance.currentSubtitle)
            {
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSource.clip.name);
            }
            
            audioSource.Play();
            StartCoroutine(WaitForSound());
        }
    }

    IEnumerator WaitForSound()
    {
        while (audioSource.isPlaying || pause)
        {
            yield return new WaitForEndOfFrame();

        }
        subText.text = "";
        if (audioSource.clip == audioClips[1])
        {
            passwort.back();
        }

    }

    public void showAll()
    {
        for (int i = 0; i < texte.Length; i++)
        {
            texte[i].SetActive(true);
        }

        bFilter[0].tag = -1;
        bFilter[1].gelöst = -1;
        bFilter[2].Datum = -1;
        bFilter[3].Entfernung = -1;
        bFilter[4].last = -1;
        bFilter[5].ermittlung = -1;
        bFilter[6].aufgearbeitet = -1;
        bFilter[7].unterhaltung = -1;

        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI text = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

            text.text = "---";

        }


    }


}
