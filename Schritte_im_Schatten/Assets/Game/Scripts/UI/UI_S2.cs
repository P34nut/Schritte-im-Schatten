using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_S2 : MonoBehaviour {

    #region Declaration

    //public AudioSource[] audios;
    
    //Boolean
    public bool ringing = false;
    private bool interactive;
    private bool radio_active = false;

    //andere Skripte
    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryRene inventoryRene;
    private Inventar inventory;
    private GameObject bgm;
    //private Music musicChange;
    //private AudioSource bgSource;
    private BGMusic bgm_script;
    //public AudioSource changeSource;

    //Schreibtisch
    private GameObject back_tisch;
    private GameObject nadel;
    private GameObject dietrich;
    public GameObject notizbuch;
    private GameObject telefon;

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
    private GameObject messer;
    private GameObject radio;
    private GameObject back_kueche;

    //Audio
    private GameObject audio_radio;
    public GameObject telephone_audio;

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
    private GameObject karte_audio;

    //Prefabs
    public GameObject prefabTisch;
    public GameObject prefabRaum;
    public GameObject prefabKueche;
    public GameObject prefabOrange;
    public GameObject prefabSchrank;
    public GameObject prefabOverlay;
    public GameObject prefabRegal;
    public GameObject prefabPhantom;
    public GameObject prefabKarte;
 
    //States -- welches Layout
    public enum ButtonState { none, dialog, raum, tisch, regal, schrank, kueche, orange, phantom, karte}
    public ButtonState state;
    #endregion

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
        
        

        //Instantiate
        prefabTisch = Instantiate<GameObject>(prefabTisch);
        prefabRaum = Instantiate<GameObject>(prefabRaum);
        prefabKueche = Instantiate<GameObject>(prefabKueche);
        prefabOrange = Instantiate<GameObject>(prefabOrange);
        prefabSchrank = Instantiate<GameObject>(prefabSchrank);
        prefabOverlay = Instantiate<GameObject>(prefabOverlay);
        prefabRegal = Instantiate<GameObject>(prefabRegal);
        prefabPhantom = Instantiate<GameObject>(prefabPhantom);
        prefabKarte = Instantiate<GameObject>(prefabKarte);

        //Finden aller Buttons
        back_tisch = GameObject.Find("Back_tisch");
        nadel = GameObject.Find("Nadel");
        dietrich = GameObject.Find("Dietrich");
        notizbuch = GameObject.Find("Notizbuch");
        telefon = GameObject.Find("Telefon");

        schreibtisch = GameObject.Find("Schreibtisch");
        kueche = GameObject.Find("Kueche");
        regal = GameObject.Find("Regal");
        schrank = GameObject.Find("Schrank");
        schlafzimmer = GameObject.Find("Schlafzimmer");
        eingang = GameObject.Find("Eingang");
        karte = GameObject.Find("Karte");

        messer = GameObject.Find("Messer");
        kuehlschrank = GameObject.Find("Kuehlschrank");
        radio = GameObject.Find("Radio");
        back_kueche = GameObject.Find("Back_kueche");

        audio_radio = GameObject.Find("Audio_Radio");
        telephone_audio = GameObject.Find("Telephone_Audio");

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

        bgm = GameObject.Find("BGMusic");
        bgm_script = bgm.GetComponent<BGMusic>();
        //musicChange = bgm.GetComponent<Music>();
        //bgSource = bgm.GetComponent<AudioSource>();
        //changeSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        prefabTisch.SetActive(false);
        prefabRaum.SetActive(false);
        prefabKueche.SetActive(false);
        prefabOrange.SetActive(false);
        prefabSchrank.SetActive(false);
        prefabOverlay.SetActive(false);
        prefabRegal.SetActive(false);
        prefabPhantom.SetActive(false);
        karte_audio.SetActive(false);
        prefabKarte.SetActive(false);



        StartCoroutine(telephoneCheck());
    }

    private void Update()
    {
        if ((vidcont.id == 1 || vidcont.id == 5) && ringing)
        {
            set_tel(telefon);
        }

    }

    public void display()
    {
        int id = vidcont.id;

        state = (ButtonState)xml.getState(id);

        telefon.SetActive(false);
        notizbuch.SetActive(false);
        nadel.SetActive(false);
        dietrich.SetActive(false);
        back_tisch.SetActive(false);
        bigger.SetActive(false);



        switch (state)
        {
            case ButtonState.none:
                checkNext(id);
                break;
            case ButtonState.dialog:
                break;
            case ButtonState.raum:
                setRaum(id);
                break;
            case ButtonState.tisch:
                setTisch(id);
                break;
            case ButtonState.regal:
                setRegal(id);
                break;
            case ButtonState.schrank:
                setSchrank(id);
                break;
            case ButtonState.kueche:
                setKueche(id);
                break;
            case ButtonState.orange:
                setOrange(id);
                break;
            case ButtonState.phantom:
                setPhantom(id);
                break;
            case ButtonState.karte:
                setKarte();
                break;

        }
        if (xml.getLoop(id))
        {
            setting.setSettings();
        }

    }

    public void setKarte()
    {
        prefabKarte.SetActive(true);
        set_button(back_karte, xml.getBlock(vidcont.id).option[0]);
    }

    IEnumerator telephoneCheck()
    {

        while (notizbuch.GetComponent<Button>().interactable == true || dietrich.GetComponent<Button>().interactable == true || messer.GetComponent<Button>().interactable == true)
        {
            yield return new WaitForSeconds(0.25f);
        }

        
        telephone_audio.GetComponent<AudioSource>().PlayDelayed(10);
        yield return new WaitForSecondsRealtime(10);
        ringing = true;
        //while (vidcont.id != 5)
        //{
        //    yield return new WaitForSeconds(0.01f);
        //}
        //vidcont.showOpened();
        //vidcont.openSpecificVideo(28);
        //yield return new WaitForEndOfFrame();
        //yield return new WaitForEndOfFrame();
        //yield return new WaitForEndOfFrame();
        //set_tel(telefon);



        while (vidcont.id != 28)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(1.3f);
        //StartCoroutine(fade.FadeAudioOut(bgSource,true));
        //StartCoroutine(fade.FadeAudioOut(audio_radio.GetComponent<AudioSource>(), true));
        StartCoroutine(bgm_script.SwitchTrack(32));
        telephone_audio.GetComponent<AudioSource>().Stop();

        StopCoroutine(telephoneCheck());

    }


    public void checkNext(int id)
    {
        if (id == 15)
        {
            prefabOverlay.SetActive(true);
            inhalt_overlay.SetActive(true);
            set_button(weiter, xml.getBlock(id).option[0]);
        }
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

    }

    public void setPhantom(int id)
    {
        prefabPhantom.SetActive(true);
        //StartCoroutine(fade.FadeAudioOut(bgSource,true));
        //bgSource.clip = musicChange.audioChangeImLevel[1];
        //StartCoroutine(fade.FadeAudioIn(changeSource,true));
        StartCoroutine(bgm_script.SwitchTrack(4));
    }

    public void setRegal(int id)
    {
        prefabRegal.SetActive(true);
        bild_links.GetComponent<Button>().onClick.AddListener(() => set_show(0));
        bild_rechts.GetComponent<Button>().onClick.AddListener(() => set_show(1));
        set_button(polizei, xml.getBlock(id).option[1]);
        set_button(back_regal, xml.getBlock(id).option[0]);
    }

    public void setSchrank(int id)
    {
        prefabSchrank.SetActive(true);
        set_button(ordner, xml.getBlock(id).option[1]);
        set_button(back_schrank, xml.getBlock(id).option[0]);
    }

    public void setOrange(int id)
    {
        prefabOrange.SetActive(true);
        set_button(saft, xml.getBlock(id).option[0]);
        set_audio(alkohol);
    }

    public void setKueche(int id)
    {
        

        if(id == 19 || id == 24)
        {
            Cursor.visible = true;
            prefabKueche.SetActive(true);
          
                set_radio(radio);
                set_button(back_kueche, xml.getBlock(id).option[0]);
                set_button(kuehlschrank, xml.getBlock(id).option[1]);
                set_button_take(messer, xml.getBlock(id).option[2],2);
            
        }
        else if (!messer.GetComponent<Button>().interactable)
        {
            vidcont.next = 24;
            Cursor.visible = false;
        }
        else
        {
            vidcont.next = 19;
            Cursor.visible = false;
        }
        
    }

    public void setRaum(int id)
    {
        prefabRaum.SetActive(true);

        set_audio(schlafzimmer);
        set_audio(eingang);
        set_button(schreibtisch, xml.getBlock(id).option[0]);
        set_button(regal, xml.getBlock(id).option[1]);
        set_button(schrank, xml.getBlock(id).option[2]);
        set_button(kueche, xml.getBlock(id).option[3]);
        set_button(karte, xml.getBlock(id).option[4]);
        

    }

    public void setTisch(int id)
    {
        if(id == 1 || id == 5)
        {
            Cursor.visible = true;
            prefabTisch.SetActive(true);
            
            if (ringing)
            {
                set_tel(telefon);
                Debug.Log("es schellt");
            }

            set_button(back_tisch, xml.getBlock(id).option[0]);
            set_button_take(dietrich, xml.getBlock(id).option[1],0);
            set_button_take(nadel, xml.getBlock(id).option[2],1);
            set_button(notizbuch, xml.getBlock(id).option[3]);

            
        }
        else if (!notizbuch.GetComponent<Button>().interactable)
        {
            vidcont.next = xml.getBlock(3).option[0].nextVideoID;
            Cursor.visible = false;
        }
        else
        {
            vidcont.next = xml.getBlock(3).option[1].nextVideoID;
            Cursor.visible = false;

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
        prefabKarte.SetActive(false);
        prefabTisch.SetActive(false);
        prefabRaum.SetActive(false);
        prefabKueche.SetActive(false);
        prefabOrange.SetActive(false);
        prefabSchrank.SetActive(false);
        prefabOverlay.SetActive(false);
        prefabRegal.SetActive(false);
        prefabPhantom.SetActive(false);
        setting.disableSettings();
    }

    IEnumerator stopRadio()
    {
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
            vidcont.openSpecificVideo(26);
            button.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(stopRadio())); 
            button.GetComponent<Button>().onClick.AddListener(() => radio_active = false);
            button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(26); });
            
            
        }
        else
        {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            vidcont.openSpecificVideo(25);
            //button.GetComponent<Button>().onClick.AddListener(() => audio_radio.GetComponent<AudioSource>().PlayDelayed(2.3f));
            button.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.WaitForSwitch(2.3f,3)));
            button.GetComponent<Button>().onClick.AddListener(() => radio_active = true);
            button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(25); });

            
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

    public void set_tel(GameObject button)
    {
        //Debug.Log("test"); 
        button.SetActive(true);
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(28); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
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
