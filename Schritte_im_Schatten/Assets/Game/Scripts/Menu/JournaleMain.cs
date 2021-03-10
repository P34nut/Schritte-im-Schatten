using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class JournaleMain : MonoBehaviour {

    public Button weiter_rene;
    public Button zurueck_rene;
    public Image journalImage;
    public Button weiter_elena;
    public Button zurueck_elena;
    public Button back_rene;
    public Button back_elena;

    private int currentSpriteRene = 0;
    private int currentSpriteElena = 0;
    public int maxSpriteIndex;
    public bool rene;

    public GameObject prefabJournal;

    public Sprite[] spritesRene;
    public Sprite[] spritesElena;

    public Sprite[] spritesReneEnglish;
    public Sprite[] spriteElenaEnglish;

    // Use this for initialization
    void Start ()
    {
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


        if (rene)
        {
            back_rene.gameObject.SetActive(true);
        }
        else
        {
            back_elena.gameObject.SetActive(true);
        }

    }

    public void display(bool boo)
    {
        rene = boo;

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

        if (!rene)
        {
            currentSpriteElena = 0;
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesElena[currentSpriteElena];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spriteElenaEnglish[currentSpriteElena];
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

        if (!rene)
        {
            currentSpriteElena += 1;

            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesElena[currentSpriteElena];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spriteElenaEnglish[currentSpriteElena];
            }
            

        }
        else
        {
            currentSpriteRene += 1;

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

    public void changeImageZurueck()
    {
        if (!rene)
        {
            currentSpriteElena -= 1;
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                journalImage.sprite = spritesElena[currentSpriteElena];
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                journalImage.sprite = spriteElenaEnglish[currentSpriteElena];
            }
        }
        else
        {
            currentSpriteRene -= 1;
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

    public void getMaxSprites()
    {
        if (rene)
        {
            maxSpriteIndex = 3;
        }
        else
        {
            maxSpriteIndex = 6;
        }
    }

}
