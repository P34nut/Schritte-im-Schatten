using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_S10 : MonoBehaviour
{
    //public AudioSource audioSource;
    public AudioClip[] clips;
    public bool fotografiert;
    int id;
    bool interactive;
    int raumIndex;
    int kellerIndex;
    private bool isTrue;
    private bool isTrue1;
    private bool isTrue2;
    private bool isTrue3;
    private bool isRinging;
    private float time = 2;

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

    public GameObject prefabHaus;
    public GameObject prefabRaum;
    public GameObject prefabBild;
    public GameObject prefabHerd;
    public GameObject prefabKeller;
    public GameObject prefabVersteck;


    private GameObject klingel;
    private GameObject eingang;

    private GameObject bild;
    private GameObject kellertuer;
    private GameObject tisch;
    private GameObject kochstelle;

    private GameObject back_bild;
    private GameObject bild_rechts;
    private GameObject bild_links;
    private GameObject bigger;

    private GameObject brief;
    private GameObject back_herd;

    private GameObject buch;
    private GameObject back_keller;
    private GameObject wand;

    private GameObject unter_tisch;
    private GameObject sofa;
    private GameObject timer;

    public Sprite[] bigger_sprite;

    public enum ButtonState { none, dialog, haus, raum, keller, versteck, bild, herd }
    public ButtonState state;

    private void Awake()
    {
        inventoryElena = GameObject.Find("Inventory").GetComponent<InventoryElena>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
    }

    // Use this for initialization
    void Start()
    {

        setting = this.GetComponent<SettingsButton>();
        xml = this.GetComponent<Content_xml>();
        vidcont = this.GetComponent<VideoController>();
        bgMusic = GameObject.Find("BGMusic");
        bgm_script = bgMusic.GetComponent<BGMusic>();
        //musicChange = bgMusic.GetComponent<Music>();
        //bgSource = bgMusic.GetComponent<AudioSource>();
        //changeSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        prefabHaus = Instantiate<GameObject>(prefabHaus);
        prefabRaum = Instantiate<GameObject>(prefabRaum);
        prefabBild = Instantiate<GameObject>(prefabBild);
        prefabHerd = Instantiate<GameObject>(prefabHerd);
        prefabKeller = Instantiate<GameObject>(prefabKeller);
        prefabVersteck = Instantiate<GameObject>(prefabVersteck);

        klingel = GameObject.Find("Klingel");
        eingang = GameObject.Find("Eingang");

        bild = GameObject.Find("Bild");
        kellertuer = GameObject.Find("Keller_Tür");
        tisch = GameObject.Find("Tisch");
        kochstelle = GameObject.Find("Kochstelle");

        back_bild = GameObject.Find("Back_Bild");
        bild_links = GameObject.Find("Bild_Links");
        bild_rechts = GameObject.Find("Bild_Rechts");
        bigger = GameObject.Find("Bigger");

        brief = GameObject.Find("Brief");
        back_herd = GameObject.Find("Back_Herd");

        buch = GameObject.Find("Buch");
        back_keller = GameObject.Find("Back_Keller");
        wand = GameObject.Find("Wand");

        unter_tisch = GameObject.Find("Unter_Tisch");
        sofa = GameObject.Find("Sofa");
        timer = GameObject.Find("Timer");

        timer.SetActive(false);
        bigger.SetActive(false);
        eingang.SetActive(false);
        prefabHaus.SetActive(false);
        prefabRaum.SetActive(false);
        prefabBild.SetActive(false);
        prefabHerd.SetActive(false);
        prefabKeller.SetActive(false);
        prefabVersteck.SetActive(false);
    }

    private void Update()
    {
        if (vidcont.id == 23 && !isTrue)
        {
            //if (!bgSource.isPlaying)
            //{
            //    StartCoroutine(fade.FadeAudioOut(changeSource, true));
            //    StartCoroutine(fade.FadeAudioIn(bgSource, true));
            isTrue = true;
            //}

            StartCoroutine(bgm_script.SwitchTrack(21));

        }

        if (vidcont.id == 3 && !isTrue1 && isRinging)
        {
            
            if (time>0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                AudioManger.Instance.setAudio(0, true);
                StartCoroutine(bgm_script.WaitForSwitch(6.5f, 18));
                isTrue1 = true;
            }
            
           
            
        }
        if (vidcont.id == 6 && !isTrue2)
        {

            AudioManger.Instance.setAudio(1, true);
            isTrue2 = true;

        }

    }

    public void display()
    {


        eingang.SetActive(false);
        bigger.SetActive(false);
        timer.SetActive(false);

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
            case ButtonState.haus:
                setHaus();
                setting.setSettings();
                break;
            case ButtonState.raum:
                setRaum();
                setting.setSettings();
                break;
            case ButtonState.bild:
                setBild();
                setting.setSettings();
                break;
            case ButtonState.herd:
                setHerd();
                setting.setSettings();
                break;
            case ButtonState.keller:
                setKeller();
                setting.setSettings();
                break;
            case ButtonState.versteck:
                setVersteck();
                break;

        }
    }

    public void setVersteck()
    {
        prefabVersteck.SetActive(true);

        timer.SetActive(true);
        set_button(unter_tisch, xml.getBlock(id).option[1]);
        set_button(sofa, xml.getBlock(id).option[0]);
        vidcont.next = xml.getBlock(id).option[1].nextVideoID;
    }

    public void setKeller()
    {

        if(id == 14 || id == 17)
        {


            //if (!bgSource.isPlaying)
            //{
            //    StartCoroutine(fade.FadeAudioOut(changeSource, true));
            //    StartCoroutine(fade.FadeAudioIn(bgSource, true));
            //    changeSource.clip = musicChange.audioChangeImLevel[11];
            //}

            //StartCoroutine(bgm_script.SwitchTrack(19));

            prefabKeller.SetActive(true);

           

            buch.GetComponent<Button>().onClick.RemoveAllListeners();
            set_button_take(buch, xml.getBlock(id).option[0],1);
            buch.GetComponent<Button>().onClick.AddListener(() => kellerIndex = kellerIndex + 1);

            set_audio(wand);

            if (!wand.GetComponent<Button>().interactable && kellerIndex <2)
            {
                kellerIndex = kellerIndex + 1;
            }
            

            if (kellerIndex == 2)
            {
                back_keller.GetComponent<Button>().onClick.RemoveAllListeners();
                set_button(back_keller, xml.getBlock(id).option[2]);
                back_keller.GetComponent<Button>().onClick.AddListener(() => erKommt());
            }
            else
            {
                set_audio(back_keller);
            }

        }
        else if (!buch.GetComponent<Button>().interactable)
        {
            vidcont.next = xml.getBlock(16).option[1].nextVideoID;
            Cursor.visible = false;
        }
        else
        {
            vidcont.next = xml.getBlock(16).option[0].nextVideoID;
            Cursor.visible = false;

        }

    }

    public void erKommt()
    {
        //if (!changeSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(bgSource, true));
        //    StartCoroutine(fade.FadeAudioIn(changeSource, true));
        //    bgSource.clip = musicChange.audioChangeImLevel[12];
        //}

        StartCoroutine(bgm_script.SwitchTrack(20));

    }

    public void setHerd()
    {
        prefabHerd.SetActive(true);

        set_button(back_herd, xml.getBlock(id).option[1]);

        brief.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(brief, xml.getBlock(id).option[0]);
        brief.GetComponent<Button>().onClick.AddListener(() => raumIndex = raumIndex + 1);
    }

    public void setBild()
    {
        prefabBild.SetActive(true);

        back_bild.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(back_bild, xml.getBlock(id).option[0]);
        back_bild.GetComponent<Button>().onClick.AddListener(() => raumIndex = raumIndex + 1);

        bild_links.GetComponent<Button>().onClick.AddListener(() => set_show(0));
        bild_rechts.GetComponent<Button>().onClick.AddListener(() => set_show(1));

    }

    public void set_show(int index)
    {
        bigger.GetComponent<Image>().sprite = bigger_sprite[index];
        bigger.SetActive(true);

    }

    public void setRaum()
    {
        //changeSource.clip = musicChange.audioChangeImLevel[9];
        //if (!changeSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(bgSource, true));
        //    StartCoroutine(fade.FadeAudioIn(changeSource, true));
        //    bgSource.clip = musicChange.audioChangeImLevel[10];
        //}

        //StartCoroutine(bgm_script.SwitchTrack(18));

        prefabRaum.SetActive(true);

        
        set_button(bild, xml.getBlock(id).option[0]);

        tisch.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(tisch, xml.getBlock(id).option[1]);
        tisch.GetComponent<Button>().onClick.AddListener(() => raumIndex = raumIndex + 1);

        set_button(kochstelle, xml.getBlock(id).option[2]);

        if(raumIndex == 3)
        {
            set_button(kellertuer, xml.getBlock(id).option[3]);
            kellertuer.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.WaitForSwitch(8.5f, 19)));
        }
        else
        {
            set_audio(kellertuer);
        }

    }

    public void setHaus()
    {
        prefabHaus.SetActive(true);

        set_button(klingel, xml.getBlock(id).option[0]);

        if (!klingel.activeInHierarchy)
        {
            set_button(eingang, xml.getBlock(id).option[1]);
            eingang.GetComponent<Button>().onClick.AddListener(() => isRinging = true);
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
        button.GetComponent<Button>().onClick.RemoveAllListeners();
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
        button.GetComponent<Button>().onClick.AddListener(delegate { inventoryElena.setImage(inventoryIndex); });
    }

    public void set_button_interact(GameObject button, Option o)
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

        button.GetComponent<Button>().onClick.AddListener(() => disable());
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
    }

    public void disable()
    {
        prefabHaus.SetActive(false);
        prefabRaum.SetActive(false);
        prefabBild.SetActive(false);
        prefabHerd.SetActive(false);
        prefabKeller.SetActive(false);
        prefabVersteck.SetActive(false);
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

        button.GetComponent<Button>().onClick.AddListener(() => checkAudio(button));

        button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
    }

    public void checkAudio(GameObject button)
    {
        int index = button.GetComponent<AudioIndex>().audioIndex;
        bool hasSub = button.GetComponent<AudioIndex>().hasSubtitle;
        AudioManger.Instance.setAudio(index, hasSub);
    }

}
