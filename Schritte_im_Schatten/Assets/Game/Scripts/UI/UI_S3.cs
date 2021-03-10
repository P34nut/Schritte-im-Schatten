using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class UI_S3 : MonoBehaviour {

    public bool minigame=false;
    private bool interactive;
    public int archivBefore=0;
    bool isFading;

    private GameObject bgMusic;
    //private Music musicChange;
    //private AudioSource bgSource;
    private BGMusic bgm_script;
    //public AudioSource changeSource;
    private CursorSettings cursorSettings;

    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryElena inventoryElena;
    private Inventar inventory;

    private GameObject dialog_1;
    private GameObject dialog_2;
    private GameObject dialog_3;
    private GameObject timer;

    private GameObject pc;
    private GameObject ausgang;

    private GameObject vorwaerts;
    private GameObject links;
    private GameObject rechts;
    private GameObject zurueck;
    private GameObject linksEck;
    private GameObject rechtsEck;
    private GameObject correct;
    private GameObject wrong_1;
    private GameObject wrong_2;
    private GameObject wrong_3;
    private GameObject correct_links;
    private GameObject correct_rechts;
    private Text dialog_text;

    public GameObject prefabDialog;
    public GameObject prefabVorraum;
    public GameObject prefabArchiv;
    public GameObject prefabPasswort;

    public enum ButtonState { none, dialog, raum, passwort,archiv}
    public ButtonState state;

    private void Awake()
    {
        inventoryElena = GameObject.Find("Inventory").GetComponent<InventoryElena>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
    }

    // Use this for initialization
    void Start () {

        cursorSettings = Camera.main.GetComponent<CursorSettings>();
        setting = this.GetComponent<SettingsButton>();
        xml = this.GetComponent<Content_xml>();
        vidcont = this.GetComponent<VideoController>();
        bgMusic = GameObject.Find("BGMusic");
        bgm_script = bgMusic.GetComponent<BGMusic>();
        //musicChange = bgMusic.GetComponent<Music>();
        //bgSource = bgMusic.GetComponent<AudioSource>();
        //changeSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        prefabDialog = Instantiate<GameObject>(prefabDialog);
        prefabVorraum = Instantiate<GameObject>(prefabVorraum);
        prefabArchiv = Instantiate<GameObject>(prefabArchiv);
        prefabPasswort = Instantiate<GameObject>(prefabPasswort);

        dialog_1 = GameObject.Find("Dialog_1");
        dialog_2 = GameObject.Find("Dialog_2");
        dialog_3 = GameObject.Find("Dialog_3");
        timer = GameObject.Find("Timer");
        dialog_text = GameObject.Find("Dialog_Subtitle").GetComponent<Text>();

        pc = GameObject.Find("Pc");
        ausgang = GameObject.Find("Ausgang");

        vorwaerts = GameObject.Find("Vorwaerts");
        zurueck = GameObject.Find("Zurueck");
        links = GameObject.Find("Links");
        rechts = GameObject.Find("Rechts");
        linksEck = GameObject.Find("LinksEck");
        rechtsEck = GameObject.Find("RechtsEck");
        correct = GameObject.Find("Correct");
        wrong_1 = GameObject.Find("Wrong_1");
        wrong_2 = GameObject.Find("Wrong_2");
        wrong_3 = GameObject.Find("Wrong_3");
        correct_links = GameObject.Find("CorrectLinks");
        correct_rechts = GameObject.Find("CorrectRechts");

        prefabDialog.SetActive(false);
        prefabVorraum.SetActive(false);
        prefabArchiv.SetActive(false);
        prefabPasswort.SetActive(false);
        dialog_1.SetActive(false);
        dialog_2.SetActive(false);
        dialog_3.SetActive(false);
        timer.SetActive(false);
    }

    private void Update()
    {
        if (vidcont.id == 12 && !isFading)
        {
            isFading = true;
            StartCoroutine(bgm_script.WaitForSwitch(36.5f, 6));
            
        }
    }

    public void display()
    {
        
        int id = vidcont.id;

        state = (ButtonState)xml.getState(id);

        dialog_1.SetActive(false);
        dialog_2.SetActive(false);
        dialog_3.SetActive(false);
        timer.SetActive(false);

        ausgang.SetActive(false);
        pc.SetActive(false);

        vorwaerts.SetActive(false);
        zurueck.SetActive(false);
        rechts.SetActive(false);
        links.SetActive(false);
        linksEck.SetActive(false);
        rechtsEck.SetActive(false);
        correct.SetActive(false);
        wrong_1.SetActive(false);
        wrong_2.SetActive(false);
        wrong_3.SetActive(false);
        correct_links.SetActive(false);
        correct_rechts.SetActive(false);

        switch (state)
        {
            case ButtonState.none:
                setting.disableSettings();
                checkNext(id);
                break;
            case ButtonState.dialog:
                checkNext(id);
                setDialog(id);
                break;
            case ButtonState.raum:
                setRaum(id);
                setting.setSettings();
                break;
            case ButtonState.passwort:
                setPasswort();
                setting.setSettings();
                break;
            case ButtonState.archiv:
                setArchiv(id);
                vidcont.useFade = true;
                break;
          
        }
    }

    //Minigame
    public void setArchiv(int id)
    {
        prefabArchiv.SetActive(true);

        //StartCoroutine(bgm_script.SwitchTrack(7));

        switch (id)
        {
            case 18:
                set_button(vorwaerts, xml.getBlock(id).option[0]);
                set_button(links, xml.getBlock(id).option[2]);
                set_button(rechts, xml.getBlock(id).option[1]);
                break;
            case 19:
                set_button(vorwaerts, xml.getBlock(id).option[1]);
                set_button(links, xml.getBlock(id).option[3]);
                set_button(rechts, xml.getBlock(id).option[2]);
                set_button(zurueck, xml.getBlock(id).option[0]);
                break;
            case 20:
                set_button(links, xml.getBlock(id).option[1]);
                set_button(rechts, xml.getBlock(id).option[2]);
                set_button(zurueck, xml.getBlock(id).option[0]);
                archivBefore = 1;
                break;
            case 21:
                set_button(links, xml.getBlock(id).option[2]);
                set_button(rechts, xml.getBlock(id).option[1]);
                set_button(zurueck, xml.getBlock(id).option[0]);
                archivBefore = 3;
                break;
            case 22:
                
                if (archivBefore == 1 || archivBefore ==2)
                {
                    set_button(rechtsEck, xml.getBlock(id).option[1]);
                    set_button(linksEck, xml.getBlock(id).option[3]);
                }
                else
                {
                    set_button(rechtsEck, xml.getBlock(id).option[0]);
                    set_button(linksEck, xml.getBlock(id).option[2]);
                } 
                break;
            case 23:
                if (archivBefore == 1 || archivBefore == 3)
                {
                    set_button(rechtsEck, xml.getBlock(id).option[2]);
                    set_button(linksEck, xml.getBlock(id).option[1]);
                }
                else
                {
                    set_button(rechtsEck, xml.getBlock(id).option[3]);
                    set_button(linksEck, xml.getBlock(id).option[0]);
                }
                break;
            case 24:
                set_correct(correct,1);
                set_button(correct_links, xml.getBlock(id).option[1]);
                set_button(correct_rechts, xml.getBlock(id).option[2]);
                set_audio(wrong_1);
                set_audio(wrong_2);
                set_audio(wrong_3);
                break;
            case 26:
                set_button(links, xml.getBlock(id).option[2]);
                set_button(rechts, xml.getBlock(id).option[1]);
                set_button(zurueck, xml.getBlock(id).option[0]);
                archivBefore = 2;
                break;
        }

    }

    public void setPasswort()
    {
        prefabPasswort.SetActive(true);
        cursorSettings.setMenu();

    }

    public void setDialog(int id)
    {
        
        if (xml.getVideo(id).preloads.Count >= 1)
        {
            //vidcont.subtitlesUI.GetComponent<Text>().text = "";
            prefabDialog.SetActive(true);
            timer.SetActive(true);
           
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

    public void setRaum(int id)
    {
        prefabVorraum.SetActive(true);

        //StartCoroutine(bgm_script.SwitchTrack(6));

        set_button(pc, xml.getBlock(id).option[1]);

        if (minigame)
        {

            set_button(ausgang, xml.getBlock(id).option[0]);
            ausgang.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.WaitForSwitch(3.5f, 7)));
        }
        else
        {
            set_audio(ausgang);
        }

    }

    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;
        //vidcont.subtitlesUI.ChangeTextObject(vidcont.subtitlesUI.gameObject.GetComponent<Text>());
    }

    public void set_dialog(GameObject button, Option o)
    {
        button.SetActive(true);
        vidcont.subtitlesUI._text.text = "";
        vidcont.subtitlesUI.ChangeTextObject(dialog_text);
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

        if(button == dialog_1)
        {
            dialog_2.SetActive(false);
            dialog_3.SetActive(false);
        }
        else if( button == dialog_2)
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
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button,-1));
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
    }

    public void set_button_take(GameObject button, Option o, int inventoryIndex)
    {
        if (!button.GetComponent<Button>().interactable)
        {
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }

        button.GetComponent<Button>().onClick.RemoveAllListeners();
        
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button,-1));
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
        button.GetComponent<Button>().onClick.AddListener(delegate { inventoryElena.setImage(inventoryIndex); });
    }

    public void set_audio(GameObject button)
    {
        button.SetActive(true);

        button.GetComponent<Button>().onClick.AddListener(() => checkAudio(button));

        button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
    }

    public void disable()
    {
        prefabDialog.SetActive(false);
        prefabVorraum.SetActive(false);
        prefabArchiv.SetActive(false);
        prefabPasswort.SetActive(false);
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

    public void set_correct(GameObject button, int inventoryIndex)
    {
        button.SetActive(true);
        button.GetComponent<Button>().onClick.AddListener(() => vidcont.SetSelectedOption(25));
        button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
        //button.GetComponent<Button>().onClick.AddListener(delegate { inventoryElena.setImage(inventoryIndex); });
    }

    /*public IEnumerator correct_audio()
    {
        if (!AudioManger.Instance.audioSource.isPlaying)
        {
            int index = correct.GetComponent<AudioIndex>().audioIndex;
            AudioManger.Instance.setAudio(index, true);
            //correct.GetComponent<AudioSource>().Play();
            yield return new WaitWhile(() => AudioManger.Instance.audioSource.isPlaying);
            vidcont.SetSelectedOption(25);
        }
        
    }*/

    public void checkAudio(GameObject button)
    {
        int index = button.GetComponent<AudioIndex>().audioIndex;
        bool hasSub = button.GetComponent<AudioIndex>().hasSubtitle;
        AudioManger.Instance.setAudio(index, hasSub);
    }
}
