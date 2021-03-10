using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class UI_S5 : MonoBehaviour {

    int id;
    bool minispiel = false;
    bool rene_anrufen = false;
    private bool interactive;
    public int buch_angeguckt;
    bool isFading;

    public AudioClip[] audioClips;

    private GameObject bgMusic;
    //private Music musicChange;
    //private AudioSource bgSource;
    private BGMusic bgm_script;
    //public AudioSource changeSource;

    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryElena inventoryElena;
    private Inventar inventory;
    private TimerBar timerBar;
    private CursorSettings cursorSettings;

    public GameObject prefabDialog;
    public GameObject prefabTisch;
    public GameObject prefabAktenSpiel;

    private GameObject dialog_1;
    private GameObject dialog_2;
    private GameObject dialog_3;
    private GameObject timer;
    private Text dialog_text;

    private GameObject telefon;
    private GameObject telefonbuch;
    private GameObject akten;


    public enum ButtonState { none, dialog, tisch}
    public ButtonState state;

    private void Awake()
    {
        inventoryElena = GameObject.Find("Inventory").GetComponent<InventoryElena>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
    }

    // Use this for initialization
    void Start () {

        setting = this.GetComponent<SettingsButton>();
        xml = this.GetComponent<Content_xml>();
        vidcont = this.GetComponent<VideoController>();
        bgMusic = GameObject.Find("BGMusic");
        bgm_script = bgMusic.GetComponent<BGMusic>();
        //musicChange = bgMusic.GetComponent<Music>();
        //bgSource = bgMusic.GetComponent<AudioSource>();
        //changeSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        cursorSettings = Camera.main.GetComponent<CursorSettings>();

        prefabDialog = Instantiate<GameObject>(prefabDialog);
        prefabTisch = Instantiate<GameObject>(prefabTisch);
        prefabAktenSpiel = Instantiate<GameObject>(prefabAktenSpiel);

        dialog_1 = GameObject.Find("Dialog_1");
        dialog_2 = GameObject.Find("Dialog_2");
        dialog_3 = GameObject.Find("Dialog_3");
        timer = GameObject.Find("Timer");
        dialog_text = GameObject.Find("Dialog_Subtitle").GetComponent<Text>();

        timerBar = timer.GetComponent<TimerBar>();

        akten = GameObject.Find("Akten");
        telefon = GameObject.Find("Telefon");
        telefonbuch = GameObject.Find("Telefonbuch");

        

        prefabDialog.SetActive(false);
        prefabTisch.SetActive(false);
        prefabAktenSpiel.SetActive(false);
        
    }

    private void Update()
    {
        if(vidcont.id == 0)
        {
            vidcont.answerTime = 15;
            timerBar.timeAmt = 15;
        }
        else
        {
            vidcont.answerTime = 5;
            timerBar.timeAmt = 5;
        }

        if (vidcont.id == 10 && !isFading)
        {
            StartCoroutine(bgm_script.WaitForSwitch(30.0f, 12));
            isFading = true;
        }

    }

    private void LateUpdate()
    {
        if (prefabAktenSpiel.activeInHierarchy)
        {
            cursorSettings.setMenu();
        }
    }

    public void display()
    {

        id = vidcont.id;

        state = (ButtonState)xml.getState(id);

        dialog_1.SetActive(false);
        dialog_2.SetActive(false);
        dialog_3.SetActive(false);
        timer.SetActive(false);
        akten.SetActive(false);

        switch (state)
        {
            case ButtonState.none:
                setting.disableSettings();
                checkNext(id);
                break;
            case ButtonState.dialog:
                setting.disableSettings();
                setDialog(id);
                checkNext(id);
                break;
            case ButtonState.tisch:
                setTisch();
                setting.setSettings();
                break;

        }
    }

    public void setTisch()
    {
        //bgSource.clip = musicChange.audioChangeImLevel[5];
        //if (!bgSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioIn(bgSource, true));
        //}

        //StartCoroutine(bgm_script.SwitchTrack(12));

        prefabTisch.SetActive(true);

        if (!minispiel)
        {
            akten.SetActive(true);
            //telefon.GetComponent<AudioSource>().clip = audioClips[0];
            set_audio(telefon);
            set_audio(telefonbuch);
            akten.GetComponent<Button>().onClick.AddListener(() => setAktenspiel());
        }
        else 
        {
            akten.SetActive(false);
            //telefon.GetComponent<AudioSource>().clip = audioClips[1];
            
            if (!rene_anrufen)
            {
                if (buch_angeguckt == 1)
                {
                    set_audio(telefonbuch);
                    set_button(telefon, xml.getBlock(id).option[2]);
                    telefon.GetComponent<Button>().onClick.AddListener(() => rene_anrufen = true);
                }
                else
                {
                    set_button(telefonbuch, xml.getBlock(id).option[0]);
                    telefonbuch.GetComponent<Button>().onClick.AddListener(() => buch_angeguckt = buch_angeguckt + 1);
                    set_audio(telefon);
                }
                
            }
            else
            {
                if (buch_angeguckt == 2)
                {
                    set_audio(telefonbuch);
                    set_button(telefon, xml.getBlock(id).option[3]);
                    telefon.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.SwitchTrack(32)));  
                }
                else
                {
                    telefonbuch.GetComponent<Button>().interactable = true;
                    telefonbuch.GetComponent<Button>().onClick.RemoveAllListeners();
                    set_button(telefonbuch, xml.getBlock(id).option[1]);
                    telefonbuch.GetComponent<Button>().onClick.AddListener(() => buch_angeguckt = buch_angeguckt + 1);
                    telefon.GetComponent<Button>().onClick.RemoveAllListeners();
                    set_audio(telefon);
                }
            }
        }
        
    }

    public void setDialog(int id)
    {

        if (xml.getVideo(id).preloads.Count >= 1)
        {
            prefabDialog.SetActive(true);
            timer.SetActive(true);
            vidcont.subtitlesUI._text.text = "";
            vidcont.subtitlesUI.ChangeTextObject(dialog_text);
            for (int i = 0; i < xml.getVideo(id).preloads.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        set_dialog(dialog_1, xml.getBlock(id).option[i]);
                        break;
                    case 1:
                        set_dialog(dialog_2, xml.getBlock(id).option[i]);
                        break;
                    case 2:
                        set_dialog(dialog_3, xml.getBlock(id).option[i]);
                        break;
                }
            }
        }

    }

    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

    }

    public void set_dialog(GameObject button, Option o)
    {
        button.SetActive(true);
        if (LocalizationManager.CurrentLanguageCode == "de")
        {
            button.GetComponentInChildren<Text>().text = o.textXML[0];
        }
        else if (LocalizationManager.CurrentLanguageCode == "en")
        {
            button.GetComponentInChildren<Text>().text = o.textXML[1];
        }
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disableDialog(button));
    }

    public void disableDialog(GameObject button)
    {
        if (button == dialog_1)
        {
            dialog_2.SetActive(false);
            dialog_3.SetActive(false);
        }
        else if (button == dialog_2)
        {
            dialog_1.SetActive(false);
            dialog_3.SetActive(false);
        }
        else
        {
            dialog_1.SetActive(false);
            dialog_2.SetActive(false);
        }


    }

    public void set_button(GameObject button, Option o)
    {
        button.SetActive(true);
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
       //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
    }

    public void disable()
    {
        prefabDialog.SetActive(false);
        prefabTisch.SetActive(false);
        prefabAktenSpiel.SetActive(false);
        setting.disableSettings();
    }

    public void checkInteractive(GameObject button, Option o)
    {
        interactive = o.interactive;

        if (interactive)
        {
            button.GetComponent<Button>().interactable = false;
            interactive = false;
        }
    }

    public void set_audio(GameObject button)
    {
        button.SetActive(true);

        button.GetComponent<Button>().onClick.RemoveAllListeners();

        button.GetComponent<Button>().onClick.AddListener(() => checkAudio(button));

        button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
    }

    public void checkAudio(GameObject button)
    {
        if (button == telefon)
        {
            if (!minispiel)
            {
                AudioManger.Instance.setAudio(1, true);
            }
            else
            {
                AudioManger.Instance.setAudio(2, true);
            }
           
        }
        else
        {
            int index = button.GetComponent<AudioIndex>().audioIndex;
            bool hasSub = button.GetComponent<AudioIndex>().hasSubtitle;
            AudioManger.Instance.setAudio(index, hasSub);
        }
        
    }

    public void setAktenspiel()
    {
        prefabAktenSpiel.SetActive(true);
        prefabTisch.SetActive(false);
        //cursorSettings.setMenu();
       
    }

    public IEnumerator disableAktenspiel()
    {

        minispiel = true;

        AudioManger.Instance.setAudio(0, true);

        while (AudioManger.Instance.audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        prefabAktenSpiel.SetActive(false);
        setTisch();
        cursorSettings.setDefault();
        StopCoroutine(disableAktenspiel());
    }


}
