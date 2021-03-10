using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class UI_S12 : MonoBehaviour {

    private bool interactive;
    int id;
    public int lookIndex;
    bool played;
    bool pause = false;

    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;
    private InventoryElena inventoryElena;
    private Inventar inventory;
    private GameObject bgMusic;
    //private Music musicChange;
    //private AudioSource bgSource;
    private BGMusic bgm_script;
    //public AudioSource changeSource;

    public Sprite[] pictures;
    public AudioClip[] audioclips;

    private GameObject notizbuch;
    private GameObject bild_lo;
    private GameObject bild_ro;
    private GameObject bild_lu;
    private GameObject bild_ru;
    private GameObject back;
    private GameObject bigger;
    private GameObject bigger_button;

    public GameObject prefabTisch;

    public enum ButtonState { none, dialog, tisch}
    public ButtonState state;

    private Text subText;

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

        prefabTisch = Instantiate<GameObject>(prefabTisch);

        back = GameObject.Find("Back");
        bild_lo = GameObject.Find("Bild_LO");
        bild_lu = GameObject.Find("Bild_LU");
        bild_ro = GameObject.Find("Bild_RO");
        bild_ru = GameObject.Find("Bild_RU");
        notizbuch = GameObject.Find("Notizbuch");
        bigger = GameObject.Find("Bigger");
        bigger_button = GameObject.Find("BiggerButton");
        subText = GameObject.Find("Subtitle").GetComponent<Text>();


        back.SetActive(false);
        bigger_button.SetActive(false);
        bigger.SetActive(false);

        prefabTisch.SetActive(false);
        
    }

    private void LateUpdate()
    {
        if(lookIndex == 5 && state == ButtonState.tisch && !bigger.activeInHierarchy && played)
        {
            set_button(back, xml.getBlock(1).option[1]);
        }

        

    }

    private void Update()
    {
        if (bgm_script.gameIsPaused && !pause)
        {
            pause = true;
            bigger.GetComponent<AudioSource>().Pause();
            prefabTisch.GetComponent<AudioSource>().Pause();
        }

        if (!bgm_script.gameIsPaused && pause)
        {
            pause = false;
            bigger.GetComponent<AudioSource>().UnPause();
            prefabTisch.GetComponent<AudioSource>().UnPause();
        }
    }


    public void display()
    {

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
            case ButtonState.tisch:
                setTisch();
                setting.setSettings();
                break;
        }
    }

    public void setTisch()
    {
        
        prefabTisch.SetActive(true);

        if (lookIndex == 5 && !prefabTisch.GetComponent<AudioSource>().isPlaying)
        {
            StartCoroutine(startGedanken());
        }

        notizbuch.GetComponent<Button>().onClick.RemoveAllListeners();
        set_button(notizbuch, xml.getBlock(id).option[0]);
        notizbuch.GetComponent<Button>().onClick.AddListener(()=> lookIndex=lookIndex+1 );

        bild_lo.GetComponent<Button>().onClick.RemoveAllListeners();
        bild_lo.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(setBigger(0)));
        bild_lo.GetComponent<Button>().onClick.AddListener(() => bild_lo.SetActive(false) );

        bild_lu.GetComponent<Button>().onClick.RemoveAllListeners();
        bild_lu.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(setBigger(1)));
        bild_lu.GetComponent<Button>().onClick.AddListener(() => bild_lu.SetActive(false));

        bild_ro.GetComponent<Button>().onClick.RemoveAllListeners();
        bild_ro.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(setBigger(2)));
        bild_ro.GetComponent<Button>().onClick.AddListener(() => bild_ro.SetActive(false));

        bild_ru.GetComponent<Button>().onClick.RemoveAllListeners();
        bild_ru.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(setBigger(3)));
        bild_ru.GetComponent<Button>().onClick.AddListener(() => bild_ru.SetActive(false));
    }

    public IEnumerator setBigger(int picIndex)
    {
        bigger_button.SetActive(false);
        lookIndex=lookIndex+1;
        bigger.GetComponent<Image>().sprite = pictures[picIndex];
        bigger.GetComponent<AudioSource>().clip = audioclips[picIndex];
        bigger.SetActive(true);
        if (Einstellungen.Instance.currentSubtitle)
        {
            StartCoroutine(subtitleOverTime(picIndex));
            //subText.text = LocalizationManager.GetTranslation("Subtitle/" + bigger.GetComponent<AudioSource>().clip.name);
        }
        while (bigger.GetComponent<AudioSource>().isPlaying || pause)
        {
            yield return new WaitForEndOfFrame();
        }

        bigger_button.SetActive(true);
        if (lookIndex == 5)
        {
            bigger_button.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(startGedanken()));
        }

    }

    public IEnumerator startGedanken()
    {
        if (Einstellungen.Instance.currentSubtitle)
        {
            StartCoroutine(subtitleOverTime(4));
            //subText.text = LocalizationManager.GetTranslation("Subtitle/" + prefabTisch.GetComponent<AudioSource>().clip.name);
        }
        prefabTisch.GetComponent<AudioSource>().Play();

        while (prefabTisch.GetComponent<AudioSource>().isPlaying || pause)
        {
            yield return new WaitForEndOfFrame();
        }
        //subText.text = "";
        played = true;
        StopCoroutine(startGedanken());
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
        prefabTisch.SetActive(false);
        
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
        if (!button.GetComponent<AudioSource>().isPlaying)
        {
            button.GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator subtitleOverTime(int index)
    {
        string clipName = bigger.GetComponent<AudioSource>().clip.name;
        string clipNameGedanken = prefabTisch.GetComponent<AudioSource>().clip.name;

        switch (index)
        {
            case 0:
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_01");
                yield return new WaitForSecondsRealtime(1.3f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(1.3f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_02");
                yield return new WaitForSecondsRealtime(1.3f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.9f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_03");
                yield return new WaitForSecondsRealtime(1.5f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(1f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_04");
                yield return new WaitForSecondsRealtime(2.1f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(1f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_05");
                yield return new WaitForSecondsRealtime(1.5f);
                subText.text = "";
                break;
            case 1:
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_01");
                yield return new WaitForSecondsRealtime(3.5f);
                subText.text = "";
                break;
            case 2:
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_01");
                yield return new WaitForSecondsRealtime(2.2f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(1.9f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_02");
                yield return new WaitForSecondsRealtime(1f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.5f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_03");
                yield return new WaitForSecondsRealtime(2.3f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.9f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_04");
                yield return new WaitForSecondsRealtime(1.4f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_05"); ;
                yield return new WaitForSecondsRealtime(1.8f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(1.1f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_06"); ;
                yield return new WaitForSecondsRealtime(2.5f);
                subText.text = "";
                break;
            case 3:
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_01");
                yield return new WaitForSecondsRealtime(1.8f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(1.4f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipName + "_02");
                yield return new WaitForSecondsRealtime(1.5f);
                subText.text = "";
                break;
            case 4:
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipNameGedanken + "_01");
                yield return new WaitForSecondsRealtime(1.2f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.7f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipNameGedanken + "_02");
                yield return new WaitForSecondsRealtime(4.9f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.5f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipNameGedanken + "_03");
                yield return new WaitForSecondsRealtime(4.6f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.1f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipNameGedanken + "_04");
                yield return new WaitForSecondsRealtime(1.9f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.6f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipNameGedanken + "_05");
                yield return new WaitForSecondsRealtime(2.7f);
                subText.text = "";
                yield return new WaitForSecondsRealtime(0.15f);
                subText.text = LocalizationManager.GetTranslation("Subtitle/" + clipNameGedanken + "_06");
                yield return new WaitForSecondsRealtime(2.2f);
                subText.text = "";
                break;
        }
    }




}
