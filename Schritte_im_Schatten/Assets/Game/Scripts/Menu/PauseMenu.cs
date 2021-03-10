using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

    //private TimerBar timerBar
    private BGMusic bgm_script;

    private Content_xml xml;
    private SettingsButton settingsButton;
    private VideoController vidcont;
    private Journal journal;
    public GameObject inventory;
    private GameObject journalGameObject;
    private CursorSettings cursorSettings;

    private GameObject weiter;
    private GameObject options;
    private GameObject exit;

    private GameObject pause;
    private GameObject optionsMenu;

    private Slider volumeSliderMusic;
    private Slider volumeSliderMaster;
    private Slider volumeSliderVoice;
    private Toggle fullscreenToggle;
    private Dropdown resolutionDropdown;
    private Toggle subtitleToggle;
    private Dropdown languageDropdown;

    public GameObject prefabPauseMenu;
    //public AudioMixer audioMixer;
    private Button setDefault;
    private Button backButton;


    private Einstellungen einstellungen;
    Resolution[] resolutions;


    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            display();
            vidcont.pauseVideo();
            bgm_script.PauseBGM();
            if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 || vidcont.currentSceneIndex == 11)
            {
                TimerBar.Instance.isPaused = true;
            }

            if (vidcont.currentSceneIndex == 4)
            {
               
                Schloss.Instance.isPaused = true;
            }

            
        }
    }

    private void Awake()
    {
        inventory = GameObject.Find("Inventory");

    }

    // Use this for initialization
    void Start () {

       

        //timerBar = GameObject.Find("Timer").GetComponent<TimerBar>();

        xml = GetComponent<Content_xml>();
        einstellungen = GameObject.Find("EinstellungenSkript").GetComponent<Einstellungen>();
        bgm_script = GameObject.Find("BGMusic").GetComponent<BGMusic>();
        cursorSettings = Camera.main.GetComponent<CursorSettings>();

        prefabPauseMenu = Instantiate<GameObject>(prefabPauseMenu);
        prefabPauseMenu.name = "PauseMenu";
        setDefault = GameObject.Find("Default").GetComponent<Button>();
        backButton = GameObject.Find("Button").GetComponent<Button>();

        

        journal = this.GetComponent<Journal>();
        vidcont = this.GetComponent<VideoController>();
        settingsButton = this.GetComponent<SettingsButton>();
        journalGameObject = journal.prefabJournal;

        
        weiter = GameObject.Find("Weiterspielen");
        options = GameObject.Find("Optionen");
        exit = GameObject.Find("Exit");

        volumeSliderMusic = GameObject.Find("Slider").GetComponent<Slider>();
        volumeSliderMaster = GameObject.Find("SliderMaster").GetComponent<Slider>();
        volumeSliderVoice = GameObject.Find("SliderVoice").GetComponent<Slider>();
        fullscreenToggle = GameObject.Find("VollbildToggle").GetComponent<Toggle>();
        resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
        subtitleToggle = GameObject.Find("SubtitleToggle").GetComponent<Toggle>();
        languageDropdown = GameObject.Find("LanguageDropdown").GetComponent<Dropdown>();


        pause = GameObject.Find("Pause");
        optionsMenu = GameObject.Find("OptionsMenu");

        prefabPauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        resolutionDropdown.ClearOptions();

        einstellungen.updateDropdown(resolutionDropdown);
        einstellungen.updateValues(volumeSliderMusic, volumeSliderMaster, volumeSliderVoice, fullscreenToggle, subtitleToggle, languageDropdown);

        //weiter.SetActive(false);
        //options.SetActive(false);
        //exit.SetActive(false);
        Listeners();
    }

    void Listeners()
    {
        weiter.GetComponent<Button>().onClick.AddListener(() => vidcont.unpauseVideo());
        weiter.GetComponent<Button>().onClick.AddListener(() => disable());
        weiter.GetComponent<Button>().onClick.AddListener(() => bgm_script.UnPauseBGM());
        exit.GetComponent<Button>().onClick.AddListener(() => BacktoMain());
        options.GetComponent<Button>().onClick.AddListener(() => setOptions());

        volumeSliderMusic.onValueChanged.AddListener(delegate { einstellungen.SetVolume(volumeSliderMusic); });
        volumeSliderMaster.onValueChanged.AddListener(delegate { einstellungen.SetVolumeMaster(volumeSliderMaster); });
        volumeSliderVoice.onValueChanged.AddListener(delegate { einstellungen.SetVolumeVoice(volumeSliderVoice); });
        fullscreenToggle.onValueChanged.AddListener(delegate { einstellungen.SetFullscreen(fullscreenToggle); });
        resolutionDropdown.onValueChanged.AddListener(delegate { einstellungen.SetResolution(resolutionDropdown); });
        subtitleToggle.onValueChanged.AddListener(delegate { einstellungen.SetSubtitle(subtitleToggle); });
        subtitleToggle.onValueChanged.AddListener(delegate { vidcont.SubtitleControl(); });
        languageDropdown.onValueChanged.AddListener(delegate { einstellungen.SetLanguage(languageDropdown); });
        languageDropdown.onValueChanged.AddListener(delegate { vidcont.SubtitleControl(); });
        setDefault.onClick.AddListener(() => einstellungen.SetDefault(volumeSliderMusic, volumeSliderMaster, volumeSliderVoice, fullscreenToggle, resolutionDropdown, subtitleToggle, languageDropdown));
        backButton.onClick.AddListener(() => display());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (pause.activeInHierarchy)
            {
                disable();
                vidcont.unpauseVideo();
                bgm_script.UnPauseBGM();
                if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 || vidcont.currentSceneIndex == 11)
                {
                    TimerBar.Instance.isPaused = false;
                }
                if (vidcont.currentSceneIndex == 4)
                {
                    
                    Schloss.Instance.isPaused = false;
                }
            }
            else if (optionsMenu.activeInHierarchy)
            {
                display();
            }
            else if (journal.prefabJournal.activeInHierarchy)
            {
                journal.prefabJournal.SetActive(false);
                vidcont.unpauseVideo();
                bgm_script.UnPauseBGM();
                if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 || vidcont.currentSceneIndex == 11)
                {
                    TimerBar.Instance.isPaused = false;
                }
                if (vidcont.currentSceneIndex == 4)
                {
                    
                    Schloss.Instance.isPaused = false;
                }
            }
            else
            {

                display();
                vidcont.pauseVideo();
                bgm_script.PauseBGM();
                if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 || vidcont.currentSceneIndex == 11)
                {
                    TimerBar.Instance.isPaused = true;
                }
                if (vidcont.currentSceneIndex == 4)
                {
                    
                    Schloss.Instance.isPaused = true;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (pause.activeInHierarchy)
        {
            cursorSettings.setMenu();
            Cursor.visible = true;
        }
    }

    public void display()
    {
        

        optionsMenu.SetActive(false);
        prefabPauseMenu.SetActive(true);
        pause.SetActive(true);

        journalGameObject.SetActive(false);
        inventory.GetComponent<Inventar>().inventoryElena.SetActive(false);
        inventory.GetComponent<Inventar>().inventoryRene.SetActive(false);
        inventory.GetComponent<InventoryElena>().active = false;
        inventory.GetComponent<InventoryRene>().active = false;


        if (vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 || vidcont.currentSceneIndex == 11)
        {
            weiter.GetComponent<Button>().onClick.AddListener(() => TimerBar.Instance.isPaused = false);
        }
        if (vidcont.currentSceneIndex == 4)
        {
            weiter.GetComponent<Button>().onClick.AddListener(() => Schloss.Instance.isPaused = false);
        }

        

        
    }

    public void BacktoMain()
    {
        LoadingScreenManager.LoadScene(0);
        bgm_script.UnPauseBGM();
    }


    public void disable()
    {
        prefabPauseMenu.SetActive(false);
        cursorSettings.setDefault();
        if ((!xml.getLoop(vidcont.id) && xml.getState(vidcont.id) != 1)|| vidcont.currentSceneIndex == 13)
        {
            Cursor.visible = false;
        }
    }

    public void setOptions()
    {
        pause.SetActive(false);
        optionsMenu.SetActive(true);
        
    }

}
