using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_S6 : MonoBehaviour {

    int id;
    bool interactive;
    public bool key;
    bool isFading;

    private GameObject bgMusic;
    //private Music musicChange;
    //private AudioSource bgSource;
    private BGMusic bgm_script;
    //public AudioSource changeSource;

    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryRene inventoryRene;
    private Inventar inventory;

    private GameObject statue;
    private GameObject geld;
    private GameObject hinten;
    private GameObject tuere;

    private GameObject ausweis;
    private GameObject karte;
    private GameObject bigger;
    private GameObject back_geld;

    private GameObject axt;
    private GameObject back_hinten;
    private GameObject tuere_hinten;

    public GameObject prefabHof;
    public GameObject prefabGeld;
    public GameObject prefabHinten;


    public enum ButtonState { none, dialog, hof, geldbeutel, hinten }
    public ButtonState state;

    private void Awake()
    {
        inventoryRene = GameObject.Find("Inventory").GetComponent<InventoryRene>();
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

        prefabHof = Instantiate<GameObject>(prefabHof);
        prefabGeld = Instantiate<GameObject>(prefabGeld);
        prefabHinten = Instantiate<GameObject>(prefabHinten);

        statue = GameObject.Find("Statue");
        geld = GameObject.Find("Geld");
        hinten = GameObject.Find("nach_Hinten");
        tuere = GameObject.Find("Türe");

        bigger = GameObject.Find("Bigger");
        ausweis  = GameObject.Find("Ausweis");
        karte = GameObject.Find("Karte");
        back_geld = GameObject.Find("Back_Geld");

        back_hinten = GameObject.Find("Back_Hinten");
        tuere_hinten = GameObject.Find("Türe_Hinten");
        axt = GameObject.Find("Axt");

        axt.SetActive(false);
        prefabHof.SetActive(false);
        prefabGeld.SetActive(false);
        prefabHinten.SetActive(false);

    }

    private void Update()
    {
        if (vidcont.id == 0 && !isFading)
        {
            StartCoroutine(bgm_script.WaitForSwitch(44.5f, 14));
            isFading = true;
        }
    }

    public void display()
    {
        axt.SetActive(false);
        bigger.SetActive(false);

        id = vidcont.id;

        state = (ButtonState)xml.getState(id);

        switch (state)
        {
            case ButtonState.none:
                setting.disableSettings();
                checkNext(id);
                break;
            case ButtonState.dialog:
                break;
            case ButtonState.hof:
                setting.setSettings();
                setHof();
                break;
            case ButtonState.geldbeutel:
                setting.setSettings();
                setGeld();
                break;
            case ButtonState.hinten:
                setting.setSettings();
                setHinten();
                break;

        }
    }


    public void setHof()
    {
        //changeSource.clip = musicChange.audioChangeImLevel[6];
        //if (!changeSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(bgSource, true));
        //    StartCoroutine(fade.FadeAudioIn(changeSource, true));
        //    bgSource.clip = musicChange.audioChangeImLevel[7];
        //}

        //StartCoroutine(bgm_script.SwitchTrack(14));


        if (id == 1 || id == 14)
        {
            Cursor.visible = true;
            prefabHof.SetActive(true);

            set_button(geld, xml.getBlock(id).option[0]);
            set_button_take(statue, xml.getBlock(id).option[4], 5);
            set_button(hinten, xml.getBlock(id).option[1]);
            set_button(tuere, xml.getBlock(id).option[2]);
        }
        else if (!statue.GetComponent<Button>().interactable)
        {
            vidcont.next = xml.getBlock(4).option[0].nextVideoID;
            Cursor.visible = false;
        }
        else
        {
            vidcont.next = xml.getBlock(4).option[1].nextVideoID;
            Cursor.visible = false;
        }
       


    }

    public void setGeld()
    {
        prefabGeld.SetActive(true);

        set_button(ausweis, xml.getBlock(id).option[1]);
        set_button(back_geld, xml.getBlock(id).option[0]);
        karte.GetComponent<Button>().onClick.AddListener(() => bigger.SetActive(true));

    }

    public void setHinten()
    {
        if(id == 8 || id == 13)
        {
            Cursor.visible = true;
            prefabHinten.SetActive(true);
            set_button(tuere_hinten, xml.getBlock(id).option[0]);
            set_button(back_hinten, xml.getBlock(id).option[3]);
            if (id==8)
            {
                set_button_interact(axt, xml.getBlock(id).option[1], 5);
            }
            
        }
        else if (!key)
        {
            vidcont.next = xml.getBlock(7).option[0].nextVideoID;
            Cursor.visible = false;
            //Debug.Log("TEST: " + vidcont.next);
        }
        else
        {
            vidcont.next = xml.getBlock(7).option[1].nextVideoID;
            Cursor.visible = false;
        }
        




    }

    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

    }

    public void set_button(GameObject button, Option o)
    {
        if (!button.GetComponent<Button>().interactable)
        {
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
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
        //button.SetActive(true);
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, inventoryIndex));
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
        button.GetComponent<Button>().onClick.AddListener(delegate { inventoryRene.setImage(inventoryIndex); });
    }

    public void set_button_interact(GameObject button, Option o, int interactIndex)
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
        //button.SetActive(true);
        //Debug.Log("test");
        

        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, interactIndex));
        button.GetComponent<Button>().onClick.AddListener(() => disable());
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
    }

    public void disable()
    {
        
        prefabHof.SetActive(false);
        prefabGeld.SetActive(false);
        prefabHinten.SetActive(false);
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
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        //button.SetActive(true);

        button.GetComponent<Button>().onClick.AddListener(() => checkAudio(button));

        button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
    }

    public void checkAudio(GameObject button)
    {
        if (!button.GetComponent<AudioSource>().isPlaying)
        {
            button.GetComponent<AudioSource>().Play();
        }
    }

}
