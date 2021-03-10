using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;


public class Journal : MonoBehaviour {

    private SettingsButton settingsButton;
    private VideoController vidcont;
    private PauseMenu pause;

    private Button weiter_rene;
    private Button zurueck_rene;
    private Image journalImage;
    private Button weiter_elena;
    private Button zurueck_elena;
    private Button back_rene;
    private Button back_elena;

    private int currentSpriteRene = 0;
    private int currentSpriteElena = 0;
    public int maxSpriteIndex;
    public bool rene;

    public GameObject prefabJournal;

    public Sprite[] spritesRene;
    public Sprite[] spritesElena;

    public Sprite[] spritesReneEnglish;
    public Sprite[] spritesElenaEnglish;

    // Use this for initialization
    void Start () {

        pause = this.GetComponent<PauseMenu>();
        vidcont = this.GetComponent<VideoController>();
        settingsButton = this.GetComponent<SettingsButton>();
        prefabJournal = Instantiate<GameObject>(prefabJournal);

        weiter_rene = GameObject.Find("Weiter").GetComponent<Button>();
        zurueck_rene = GameObject.Find("Zurueck").GetComponent<Button>();
        journalImage = GameObject.Find("JournalImage").GetComponent<Image>();
        weiter_elena = GameObject.Find("Weiter_Elena").GetComponent<Button>();
        zurueck_elena = GameObject.Find("Zurueck_Elena").GetComponent<Button>();
        back_elena = GameObject.Find("Button_Elena").GetComponent<Button>();
        back_rene = GameObject.Find("Button_Rene").GetComponent<Button>();


        prefabJournal.SetActive(false);

    }

    private void Update()
    {
        if (currentSpriteElena > 0 && !rene)
        {
           
            zurueck_elena.gameObject.SetActive(true);
        }
        else
        {
            zurueck_elena.gameObject.SetActive(false);
        }

        if (currentSpriteRene > 0 && rene)
        {

            zurueck_rene.gameObject.SetActive(true);
        }
        else
        {
            zurueck_rene.gameObject.SetActive(false);
        }

        if (vidcont.currentSceneIndex > 1)
        {
            if (currentSpriteElena < maxSpriteIndex && !rene)
            {
                weiter_elena.gameObject.SetActive(true);
            }
            else
            {
                weiter_elena.gameObject.SetActive(false);
            }

            if (currentSpriteRene < maxSpriteIndex && rene)
            {
                weiter_rene.gameObject.SetActive(true);
            }
            else
            {
                weiter_rene.gameObject.SetActive(false);
            }

        }

        if (rene)
        {
            back_rene.gameObject.SetActive(true);
        }
        else
        {
            back_elena.gameObject.SetActive(true);
        }
        
    }

    public void display()
    {
        vidcont.pauseVideo();

        zurueck_rene.gameObject.SetActive(false);
        weiter_rene.gameObject.SetActive(false);
        weiter_elena.gameObject.SetActive(false);
        zurueck_elena.gameObject.SetActive(false);
        back_rene.gameObject.SetActive(false);
        back_elena.gameObject.SetActive(false);

        weiter_elena.onClick.RemoveAllListeners();
        weiter_rene.onClick.RemoveAllListeners();
        zurueck_elena.onClick.RemoveAllListeners();
        zurueck_rene.onClick.RemoveAllListeners();

        weiter_rene.onClick.AddListener(() => changeImageWeiter());
        zurueck_rene.onClick.AddListener(() => changeImageZurueck());
        weiter_elena.onClick.AddListener(() => changeImageWeiter());
        zurueck_elena.onClick.AddListener(() => changeImageZurueck());
        getMaxSprites();

        prefabJournal.SetActive(true);

        if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 ||
            vidcont.currentSceneIndex == 10 || vidcont.currentSceneIndex == 12)
        {
            currentSpriteElena = 0;
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesElena[currentSpriteElena];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spritesElenaEnglish[currentSpriteElena];
            }
        }
        else
        {
            currentSpriteRene = 0;
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesRene[currentSpriteRene];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spritesReneEnglish[currentSpriteRene];
            }
        }

    }

    public void changeImageWeiter()
    {
        

        if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 ||
            vidcont.currentSceneIndex == 10 || vidcont.currentSceneIndex == 12)
        {
            currentSpriteElena += 1;
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesElena[currentSpriteElena];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spritesElenaEnglish[currentSpriteElena];
            }

        }
        else
        {
            currentSpriteRene += 1;

            if (vidcont.currentSceneIndex > 11 && currentSpriteRene == 3)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[6];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[6];
                }
                //journalImage.sprite = spritesRene[6];
            }
            else if (vidcont.currentSceneIndex == 11 && currentSpriteRene == 3)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[5];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[5];
                }
                //journalImage.sprite = spritesRene[5];
            }
            else if (vidcont.currentSceneIndex > 8 && currentSpriteRene == 2)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[4];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[4];
                }
                //journalImage.sprite = spritesRene[4];
            }
            else if (vidcont.currentSceneIndex == 8 && currentSpriteRene == 2)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[3];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[3];
                }
                //journalImage.sprite = spritesRene[3];
            }
            else if (vidcont.currentSceneIndex > 4 && currentSpriteRene == 1)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[2];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[2];
                }
                //journalImage.sprite = spritesRene[2];
            }
            else
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[currentSpriteRene];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[currentSpriteRene];
                }
                //journalImage.sprite = spritesRene[currentSpriteRene];
            }
            
        }
            

    }

    public void changeImageZurueck()
    {
        if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 ||
     vidcont.currentSceneIndex == 10 || vidcont.currentSceneIndex == 12)
        {
            currentSpriteElena -= 1;
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesElena[currentSpriteElena];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spritesElenaEnglish[currentSpriteElena];
            }
        }
        else
        {
            currentSpriteRene -= 1;
            if (vidcont.currentSceneIndex > 11 && currentSpriteRene == 3)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[6];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[6];
                }
                //journalImage.sprite = spritesRene[6];
            }
            else if (vidcont.currentSceneIndex > 8 && currentSpriteRene == 2)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[4];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[4];
                }
                //journalImage.sprite = spritesRene[4];
            }
            else if (vidcont.currentSceneIndex > 4 && currentSpriteRene == 1)
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[2];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[2];
                }
                //journalImage.sprite = spritesRene[2];
            }
            else
            {
                if (LocalizationManager.CurrentLanguageCode == "de")
                {
                    journalImage.sprite = spritesRene[currentSpriteRene];
                }
                else if (LocalizationManager.CurrentLanguageCode == "en")
                {
                    journalImage.sprite = spritesReneEnglish[currentSpriteRene];
                }
                //journalImage.sprite = spritesRene[currentSpriteRene];
            }
        }

    }

    public void getMaxSprites()
    {
        switch (vidcont.currentSceneIndex)
        {

            case 2:
                maxSpriteIndex = 0;
                rene = true;
                break;
            case 3:
                maxSpriteIndex = 1;
                rene = false;
                break;
            case 4:
                maxSpriteIndex = 1;
                rene = true;
                break;
            case 5:
                maxSpriteIndex = 2;
                rene = false;
                break;
            case 6:
                maxSpriteIndex = 1;
                rene = true;
                break;
            case 7:
                maxSpriteIndex = 3;
                rene = false;
                break;
            case 8:
                maxSpriteIndex = 2;
                rene = true;
                break;
            case 9:
                maxSpriteIndex = 2;
                rene = true;
                break;
            case 10:
                maxSpriteIndex = 4;
                rene = false;
                break;
            case 11:
                maxSpriteIndex = 3;
                rene = true;
                break;
            case 12:
                maxSpriteIndex = 5;
                rene = false;
                break;
            case 13:
                maxSpriteIndex = 3;
                rene = true;
                break;


        }
    }

}
