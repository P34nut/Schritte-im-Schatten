using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_S8 : MonoBehaviour {

    int id;
    private bool interactive;
    public int kIndex;
    public int LIndex;
    public bool combinedPhantom;
    bool kopfActive;
    bool isFading;

    private CursorSettings cursorSettings;
    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryRene inventoryRene;
    private Inventar inventory;
    private GameObject bgMusic;
    //private Music musicChange;
    //private AudioSource bgSource;
    private BGMusic bgm_script;
    //public AudioSource changeSource;

    public GameObject prefabRaum;
    public GameObject prefabLeiche;
    public GameObject prefabOverlay;

    private GameObject foto;
    private GameObject zettel;
    private GameObject koerper;
    private GameObject drogen;
    private GameObject pizza;
    private GameObject back_raum;

    private GameObject karte;
    private GameObject besen;
    private GameObject kopf_phantom;
    private GameObject boden;
    private GameObject schluessel;
    private GameObject back_leiche;

    private GameObject weiter;
    private GameObject inhalt_overlay;


    public enum ButtonState { none, dialog, raum, leiche, overlay }
    public ButtonState state;

    private void Awake()
    {
        inventoryRene = GameObject.Find("Inventory").GetComponent<InventoryRene>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
    }

    // Use this for initialization
    void Start () {

        cursorSettings = GetComponent<CursorSettings>();
        setting = this.GetComponent<SettingsButton>();
        xml = this.GetComponent<Content_xml>();
        vidcont = this.GetComponent<VideoController>();
        bgMusic = GameObject.Find("BGMusic");
        bgm_script = bgMusic.GetComponent<BGMusic>();
        //musicChange = bgMusic.GetComponent<Music>();
        //bgSource = bgMusic.GetComponent<AudioSource>();
        //changeSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        prefabRaum = Instantiate<GameObject>(prefabRaum);
        prefabLeiche = Instantiate<GameObject>(prefabLeiche);
        prefabOverlay = Instantiate<GameObject>(prefabOverlay);

        foto = GameObject.Find("Foto");
        zettel = GameObject.Find("Zettel");
        koerper = GameObject.Find("Körper");
        drogen = GameObject.Find("Drogen");
        pizza = GameObject.Find("Pizza");
        back_raum = GameObject.Find("Back_Raum");

        karte = GameObject.Find("Karte");
        besen = GameObject.Find("Besen");
        kopf_phantom = GameObject.Find("Kopf_1");
        boden = GameObject.Find("Boden");
        schluessel = GameObject.Find("Schlüssel");
        back_leiche = GameObject.Find("Back_Leiche");

        inhalt_overlay = GameObject.Find("Inhalt_Overlay");
        weiter = GameObject.Find("Weiter");

        back_leiche.SetActive(false);
        back_raum.SetActive(false);
        koerper.SetActive(false);

        prefabRaum.SetActive(false);
        prefabLeiche.SetActive(false);
        prefabOverlay.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        if (vidcont.id == 0 && !isFading)
        {
            StartCoroutine(bgm_script.WaitForSwitch(12.5f, 17));
            isFading = true;
        }

	}

    public void display()
    {
        koerper.SetActive(false);

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
            case ButtonState.raum:
                setRaum();
                setting.setSettings();
                break;
            case ButtonState.leiche:
                setLeiche();
                setting.setSettings();
                break;
            case ButtonState.overlay:
                setOverlay();
                setting.setSettings();
                break;
        }
    }

    public void setOverlay()
    {
        prefabOverlay.SetActive(true);
        set_button(weiter, xml.getBlock(id).option[0]);
    }

    public void setLeiche()
    {
        prefabLeiche.SetActive(true);

        set_button(back_leiche, xml.getBlock(id).option[4]);

        //set_audio(besen);
        //besen.GetComponent<Button>().onClick.AddListener(() => LIndex = LIndex + 1);
        besen.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(waitForAudio(besen)));

        //set_audio(boden);
        //boden.GetComponent<Button>().onClick.AddListener(() => LIndex = LIndex + 1);
        boden.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(waitForAudio(boden)));

        karte.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(karte, xml.getBlock(id).option[3]);
        karte.GetComponent<Button>().onClick.AddListener(() => LIndex = LIndex + 1);

        kopf_phantom.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(kopf_phantom, xml.getBlock(id).option[2]);
        if (!kopfActive)
        {
            kopf_phantom.GetComponent<Button>().onClick.AddListener(() => LIndex = LIndex + 1);
            kopf_phantom.GetComponent<Button>().onClick.AddListener(() => kopfActive = true);
        }
        

        schluessel.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(schluessel, xml.getBlock(id).option[1]);
        schluessel.GetComponent<Button>().onClick.AddListener(() => LIndex = LIndex + 1);

    }

    public void setRaum()
    {
        //changeSource.clip = musicChange.audioChangeImLevel[8];
        //if (!changeSource.isPlaying)
        //{
        //    StartCoroutine(fade.FadeAudioOut(bgSource, true));
        //    StartCoroutine(fade.FadeAudioIn(changeSource, true));

        //    //bgSource.clip = musicChange.audioChangeImLevel[2];
        //}

        //StartCoroutine(bgm_script.SwitchTrack(17));

        prefabRaum.SetActive(true);

        if (kIndex == 4 && LIndex == 6)
        {
            set_button(back_raum, xml.getBlock(id).option[5]);
            back_raum.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(LoopMusicOut()));
            AudioManger.Instance.setAudio(2, true);
            //back_raum.GetComponent<AudioSource>().Play();
        }
        
        set_button(koerper, xml.getBlock(id).option[4]);
        

        foto.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(foto, xml.getBlock(id).option[1]);
        foto.GetComponent<Button>().onClick.AddListener(() => kIndex = kIndex + 1);

        zettel.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(zettel, xml.getBlock(id).option[3]);
        zettel.GetComponent<Button>().onClick.AddListener(() => kIndex = kIndex + 1);

        drogen.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(drogen, xml.getBlock(id).option[0]);
        drogen.GetComponent<Button>().onClick.AddListener(() => kIndex = kIndex + 1);

        pizza.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(pizza, xml.getBlock(id).option[2]);
        pizza.GetComponent<Button>().onClick.AddListener(() => kIndex = kIndex + 1);

    }

    public void checkNext(int id)
    {   
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;
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
        prefabLeiche.SetActive(false);
        prefabRaum.SetActive(false);
        prefabOverlay.SetActive(false);
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

    public void set_audio(GameObject button)
    {
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

    public IEnumerator waitForAudio(GameObject button)
    {
        if (!AudioManger.Instance.audioSource.isPlaying)
        {
            int index = button.GetComponent<AudioIndex>().audioIndex;
            AudioManger.Instance.setAudio(index, true);
            button.GetComponent<Button>().interactable = false;

            yield return new WaitForEndOfFrame();
       
            LIndex = LIndex + 1;
            button.SetActive(false);
            cursorSettings.setDefault();
        }
        
    }

    public IEnumerator LoopMusicOut()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(bgm_script.SwitchTrack(32));
    }

}
