using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_S4 : MonoBehaviour
{

    public static UI_S4 Instance;

    #region Declaration

    public AudioSource audioSource;

    //Boolean
    private bool interactive;
    private bool radio_active = false;
    private int id;
    int larsIndex;
    bool isTrue1;
    bool isTrue2;
    bool isFading;

    //andere Skripte
    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryRene inventoryRene;
    private Inventar inventory;
    private GameObject bgMusic;
    private BGMusic bgm_script;
    //public AudioSource changeSource;

    //Schreibtisch
    private GameObject back_tisch;
    private GameObject nadel;

    //Raum
    private GameObject schreibtisch;
    private GameObject schrank;
    private GameObject regal;
    private GameObject kueche;
    private GameObject eingang;
    private GameObject schlafzimmer;
    private GameObject karte;

    //Küche
    private GameObject kuehlschrank;
    private GameObject radio;
    private GameObject back_kueche;

    //Audio
    private GameObject audio_radio;

    //Kühlschrank
    private GameObject saft;
    private GameObject alkohol;

    //Schrank
    private GameObject ordner;
    private GameObject back_schrank;

    //Overlay
    private GameObject weiter;
    private GameObject inhalt_overlay;

    //Regal
    private GameObject bild_links;
    private GameObject bild_rechts;
    private GameObject polizei;
    private GameObject bigger;
    private GameObject back_regal;
    public Sprite[] bigger_sprite;

    //Karte
    private GameObject back_karte;
    public GameObject karte_audio;

    //Haustüre
    private GameObject tuere;
    private GameObject klingel;

    //Zimmer
    private GameObject pinnwand;
    private GameObject figur;
    private GameObject bett;
    private GameObject back_zimmer;

    //Pinnwand
    private GameObject back_pinnwand;
    private GameObject zettel;
    private GameObject einkausliste;

    //Einkaufsliste
    private GameObject back_einkaufen;
    private GameObject einkaufsliste_sound;

    //Zettel
    private GameObject back_zettel;
    private GameObject zettel_sound;

    //Bett
    private GameObject back_bett;
    private GameObject visitenkarte;
    private GameObject karton;
    private GameObject brief;

    //Karton
    private GameObject back_karton;
    private GameObject karton_sound;

    //Prefabs
    public GameObject prefabTisch;
    public GameObject prefabRaum;
    public GameObject prefabKueche;
    public GameObject prefabOrange;
    public GameObject prefabSchrank;
    public GameObject prefabOverlay;
    public GameObject prefabRegal;
    public GameObject prefabKarte;
    public GameObject prefabHaustür;
    public GameObject prefabSchloss;
    public GameObject prefabZimmer;
    public GameObject prefabPinnwand;
    public GameObject prefabEinkaufsliste;
    public GameObject prefabZettel;
    public GameObject prefabBett;
    public GameObject prefabKarton;
    public GameObject prefabKartenSpiel;

    //States -- welches Layout
    public enum ButtonState { none, dialog,
        haustüre, schloss, zimmer, pinnwand, zettel, bett, einkaufsliste, karton,
        raum, tisch, regal, schrank, kueche, orange, karte, kartenspiel }
    public ButtonState state;
    #endregion

    private void Awake()
    {
        Instance = this;
        inventoryRene = GameObject.Find("Inventory").GetComponent<InventoryRene>();
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

        //Instantiate
        prefabTisch = Instantiate<GameObject>(prefabTisch);
        prefabRaum = Instantiate<GameObject>(prefabRaum);
        prefabKueche = Instantiate<GameObject>(prefabKueche);
        prefabOrange = Instantiate<GameObject>(prefabOrange);
        prefabSchrank = Instantiate<GameObject>(prefabSchrank);
        prefabOverlay = Instantiate<GameObject>(prefabOverlay);
        prefabRegal = Instantiate<GameObject>(prefabRegal);
        prefabKarte = Instantiate<GameObject>(prefabKarte);
        prefabHaustür = Instantiate<GameObject>(prefabHaustür);
        prefabSchloss = Instantiate<GameObject>(prefabSchloss);
        prefabZimmer = Instantiate<GameObject>(prefabZimmer);
        prefabPinnwand = Instantiate<GameObject>(prefabPinnwand);
        prefabEinkaufsliste = Instantiate<GameObject>(prefabEinkaufsliste);
        prefabZettel = Instantiate<GameObject>(prefabZettel);
        prefabBett = Instantiate<GameObject>(prefabBett);
        prefabKarton = Instantiate<GameObject>(prefabKarton);
        prefabKartenSpiel = Instantiate<GameObject>(prefabKartenSpiel);

        //Finden aller Buttons
        back_tisch = GameObject.Find("Back_tisch");
        nadel = GameObject.Find("Nadel");

        schreibtisch = GameObject.Find("Schreibtisch");
        kueche = GameObject.Find("Kueche");
        regal = GameObject.Find("Regal");
        schrank = GameObject.Find("Schrank");
        schlafzimmer = GameObject.Find("Schlafzimmer");
        eingang = GameObject.Find("Eingang");
        karte = GameObject.Find("Karte");

        kuehlschrank = GameObject.Find("Kuehlschrank");
        radio = GameObject.Find("Radio");
        back_kueche = GameObject.Find("Back_kueche");

        audio_radio = GameObject.Find("Audio_Radio");

        saft = GameObject.Find("Saft");
        alkohol = GameObject.Find("Alkohol");

        ordner = GameObject.Find("Ordner");
        back_schrank = GameObject.Find("Back_Schrank");

        inhalt_overlay = GameObject.Find("Inhalt_Overlay");
        weiter = GameObject.Find("Weiter");

        bild_links = GameObject.Find("Bild_Links");
        bild_rechts = GameObject.Find("Bild_Rechts");
        back_regal = GameObject.Find("Back_Regal");
        bigger = GameObject.Find("Bigger_Look");
        polizei = GameObject.Find("Polizei");

        karte_audio = GameObject.Find("Karte_audio");
        back_karte = GameObject.Find("Karte_back");

        tuere = GameObject.Find("Türe");
        klingel = GameObject.Find("Klingel");

        pinnwand = GameObject.Find("Pinnwand");
        figur = GameObject.Find("Figur");
        bett = GameObject.Find("Bett");
        back_zimmer = GameObject.Find("Back_Zimmer");

        zettel = GameObject.Find("Zettel");
        back_pinnwand = GameObject.Find("Back_Pinnwand");
        einkausliste = GameObject.Find("Einkaufsliste");

        back_einkaufen = GameObject.Find("Back_Einkaufen");
        einkaufsliste_sound = GameObject.Find("Einkaufsliste_Sound");

        back_zettel = GameObject.Find("Back_Zettel");
        zettel_sound = GameObject.Find("Zettel_Sound");

        back_bett = GameObject.Find("Back_Bett");
        karton = GameObject.Find("Karton");
        brief = GameObject.Find("Brief");
        visitenkarte = GameObject.Find("Visitenkarte");

        back_karton = GameObject.Find("Back_Karton");
        karton_sound = GameObject.Find("Karton_Sound");

        //changeSource.clip = musicChange.audioChangeImLevel[3];

        back_zimmer.SetActive(false);
        schreibtisch.SetActive(false);

        prefabTisch.SetActive(false);
        prefabRaum.SetActive(false);
        prefabKueche.SetActive(false);
        prefabOrange.SetActive(false);
        prefabSchrank.SetActive(false);
        prefabOverlay.SetActive(false);
        prefabRegal.SetActive(false);
        prefabKarte.SetActive(false);
        prefabHaustür.SetActive(false);
        prefabSchloss.SetActive(false);
        prefabZimmer.SetActive(false);
        prefabPinnwand.SetActive(false);
        prefabEinkaufsliste.SetActive(false);
        prefabZettel.SetActive(false);
        prefabBett.SetActive(false);
        prefabKarton.SetActive(false);
        prefabKartenSpiel.SetActive(false);

    }

    private void Update()
    {
        if (vidcont.id == 0 && !isFading)
        {
            StartCoroutine(bgm_script.WaitForSwitch(25.5f, 9));
            isFading = true;
        }
        if (vidcont.id == 6 && isFading)
        {
            StartCoroutine(bgm_script.WaitForSwitch(12.5f, 10));
            isFading = false;
        }
        if (vidcont.id == 2 || vidcont.id == 3)
        {
            isTrue1 = true;
        }
        if (vidcont.id == 1 && isTrue1)
        {
            AudioManger.Instance.setAudio(6, true);
            isTrue1 = false;
        }

    }

    public void display()
    {

        id = vidcont.id;

        state = (ButtonState)xml.getState(id);

        schreibtisch.SetActive(false);
        back_zimmer.SetActive(false);
        nadel.SetActive(false);
        back_tisch.SetActive(false);
        bigger.SetActive(false);



        switch (state)
        {
            case ButtonState.none:
                checkNext(id);
                break;
            case ButtonState.haustüre:
                setHaustuer();
                setting.setSettings();
                break;
            case ButtonState.schloss:
                setSchloss();
                setting.setSettings();
                break;
            case ButtonState.zimmer:
                setZimmer();
                setting.setSettings();
                break;
            case ButtonState.pinnwand:
                setPinnwand();
                setting.setSettings();
                break;
            case ButtonState.einkaufsliste:
                setEinkaufen();
                setting.setSettings();
                break;
            case ButtonState.zettel:
                setZettel();
                setting.setSettings();
                break;
            case ButtonState.bett:
                setBett();
                setting.setSettings();
                break;
            case ButtonState.karton:
                setKarton();
                setting.setSettings();
                break;
            case ButtonState.raum:
                setRaum(id);
                setting.setSettings();
                break;
            case ButtonState.tisch:
                setTisch(id);
                setting.setSettings();
                break;
            case ButtonState.regal:
                setRegal(id);
                setting.setSettings();
                break;
            case ButtonState.schrank:
                setSchrank(id);
                setting.setSettings();
                break;
            case ButtonState.kueche:
                setKueche(id);
                setting.setSettings();
                break;
            case ButtonState.orange:
                setOrange(id);
                setting.setSettings();
                break;
            case ButtonState.karte:
                setKarte();
                setting.setSettings();
                break;
            case ButtonState.kartenspiel:
                setKartenSpiel();
                
                break;

        }
        

    }

    public void setKartenSpiel()
    {
        prefabKartenSpiel.SetActive(true);
        StartCoroutine(bgm_script.SwitchTrack(4));
    }

    public void setKarton()
    {
        prefabKarton.SetActive(true);

        set_button(back_karton, xml.getBlock(id).option[0]);
        set_audio(karton_sound);
    }

    public void setBett()
    {
        if (id == 21 || id == 15)
        {
            Cursor.visible = true;
            prefabBett.SetActive(true);

            set_button(back_bett, xml.getBlock(id).option[3]);

            brief.GetComponent<Button>().onClick.RemoveAllListeners();
            set_button_take(brief, xml.getBlock(id).option[2], 4);
            brief.GetComponent<Button>().onClick.AddListener(() => larsIndex += 1);

            visitenkarte.GetComponent<Button>().onClick.RemoveAllListeners();
            set_button_take(visitenkarte, xml.getBlock(id).option[0], 7);
            visitenkarte.GetComponent<Button>().onClick.AddListener(() => larsIndex += 1);

            set_button(karton, xml.getBlock(id).option[1]);


        }
        else if (!brief.GetComponent<Button>().interactable)
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

    public void setZettel()
    {
        prefabZettel.SetActive(true);

        set_button(back_zettel, xml.getBlock(id).option[0]);
        set_audio(zettel_sound);
    }

    public void setEinkaufen()
    {
        prefabEinkaufsliste.SetActive(true);

        set_button(back_einkaufen, xml.getBlock(id).option[0]);
        set_audio(einkaufsliste_sound);
    }

    public void setPinnwand()
    {
        prefabPinnwand.SetActive(true);

        set_button(back_pinnwand, xml.getBlock(id).option[2]);
        set_button(zettel, xml.getBlock(id).option[0]);
        set_button(einkausliste, xml.getBlock(id).option[1]);

    }

    public void setZimmer()
    {

        //if (!bgSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(changeSource, true));
        //    StartCoroutine(fade.FadeAudioIn(bgSource, true));
        //    changeSource.clip = musicChange.audioLevelStart[2];
        //}

        //StartCoroutine(bgm_script.SwitchTrack(10));

        prefabZimmer.SetActive(true);

        if (larsIndex == 2)
        {
            set_button(back_zimmer, xml.getBlock(id).option[3]);
            back_zimmer.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.WaitForSwitch(12.5f, 2)));
            AudioManger.Instance.setAudio(7, true);
        }

        set_button(pinnwand, xml.getBlock(id).option[1]);
        set_button(figur, xml.getBlock(id).option[0]);
        set_button(bett, xml.getBlock(id).option[2]);
        
    }

    public void setSchloss()
    {
        prefabSchloss.SetActive(true);
    }

    public void setHaustuer()
    {

        //if (!changeSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(bgSource, true));
        //    StartCoroutine(fade.FadeAudioIn(changeSource, true));
        //    bgSource.clip = musicChange.audioChangeImLevel[4];
        //}

        //StartCoroutine(bgm_script.SwitchTrack(9));

        prefabHaustür.SetActive(true);

        tuere.SetActive(false);

        set_button(klingel, xml.getBlock(id).option[0]);
        if (!klingel.GetComponent<Button>().interactable)
        {
            set_button(tuere, xml.getBlock(id).option[1]);
        }
        
    }

    public void setKarte()
    {
        prefabKarte.SetActive(true);
        set_button(back_karte, xml.getBlock(vidcont.id).option[1]);
        
    }

    public void checkNext(int id)
    {
        if (id == 36)
        {
            prefabOverlay.SetActive(true);
            inhalt_overlay.SetActive(true);
            set_button(weiter, xml.getBlock(id).option[0]);
        }
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

    }

    public void setRegal(int id)
    {
        prefabRegal.SetActive(true);
        bild_links.GetComponent<Button>().onClick.AddListener(() => set_show(0));
        bild_rechts.GetComponent<Button>().onClick.AddListener(() => set_show(1));
        set_button(polizei, xml.getBlock(id).option[0]);
        set_button(back_regal, xml.getBlock(id).option[1]);
    }

    public void setSchrank(int id)
    {
        prefabSchrank.SetActive(true);
        set_button(ordner, xml.getBlock(id).option[0]);
        set_button(back_schrank, xml.getBlock(id).option[1]);
    }

    public void setOrange(int id)
    {
        prefabOrange.SetActive(true);
        set_button(saft, xml.getBlock(id).option[0]);
        set_audio(alkohol);
    }

    public void setKueche(int id)
    {
            
            prefabKueche.SetActive(true);

            set_radio(radio);
            set_button(back_kueche, xml.getBlock(id).option[3]);
            set_button(kuehlschrank, xml.getBlock(id).option[0]);
    }

    public void setRaum(int id)
    {
        //if (!changeSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(bgSource, true));
        //    StartCoroutine(fade.FadeAudioIn(changeSource, true));
        //    bgSource.clip = musicChange.audioChangeImLevel[5];
        //}

        //StartCoroutine(bgm_script.SwitchTrack(2));

        prefabRaum.SetActive(true);

        //if (!inventoryRene.isNadel)
        //{
            set_button(schreibtisch, xml.getBlock(id).option[0]);
        //}
        set_audio(schlafzimmer);
        set_audio(eingang);
        set_button(regal, xml.getBlock(id).option[1]);
        set_button(schrank, xml.getBlock(id).option[2]);
        set_button(kueche, xml.getBlock(id).option[3]);
        set_button(karte, xml.getBlock(id).option[4]);


    }

    public void setTisch(int id)
    {

            prefabTisch.SetActive(true);

            set_button(back_tisch, xml.getBlock(id).option[1]);
        if (!inventoryRene.isNadel)
        {
            set_button_take(nadel, xml.getBlock(id).option[0], 1);
        }
            

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

    public void disable()
    {
        prefabKartenSpiel.SetActive(false);
        prefabKarte.SetActive(false);
        prefabBett.SetActive(false);
        prefabKarton.SetActive(false);
        prefabZettel.SetActive(false);
        prefabEinkaufsliste.SetActive(false);
        prefabPinnwand.SetActive(false);
        prefabZimmer.SetActive(false);
        prefabSchloss.SetActive(false);
        prefabHaustür.SetActive(false);
        prefabTisch.SetActive(false);
        prefabRaum.SetActive(false);
        prefabKueche.SetActive(false);
        prefabOrange.SetActive(false);
        prefabSchrank.SetActive(false);
        prefabOverlay.SetActive(false);
        prefabRegal.SetActive(false);
        setting.disableSettings();
    }

    IEnumerator stopRadio()
    {
        //bgSource.clip = musicChange.audioLevelStart[2];
        yield return new WaitForSecondsRealtime(2);
        //audio_radio.GetComponent<AudioSource>().Stop();
        StartCoroutine(bgm_script.SwitchTrack(2));
        StopCoroutine(stopRadio());

    }

    public void set_radio(GameObject button)
    {
        button.SetActive(true);

        if (radio_active)
        {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(stopRadio()));
            button.GetComponent<Button>().onClick.AddListener(() => radio_active = false);
            button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(45); });


        }
        else
        {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => audio_radio.GetComponent<AudioSource>().PlayDelayed(2.3f));
            button.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.WaitForSwitch(2.3f,3)));
            button.GetComponent<Button>().onClick.AddListener(() => radio_active = true);
            button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(44); });


        }

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
        //button.SetActive(true);
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button,inventoryIndex));
        button.GetComponent<Button>().onClick.AddListener(delegate { checkInteractive(button, o); });
        button.GetComponent<Button>().onClick.AddListener(delegate { inventoryRene.setImage(inventoryIndex); });
    }

    public void set_audio(GameObject button)
    {
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        //button.SetActive(true);

        button.GetComponent<Button>().onClick.AddListener(() => checkAudio(button));
        button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
    }

    public void set_show(int index)
    {
        bigger.GetComponent<Image>().sprite = bigger_sprite[index];
        bigger.SetActive(true);

    }

    public void checkAudio(GameObject button)
    {
        int index = button.GetComponent<AudioIndex>().audioIndex;
        bool hasSub = button.GetComponent<AudioIndex>().hasSubtitle;
        AudioManger.Instance.setAudio(index, hasSub);
    }
}
