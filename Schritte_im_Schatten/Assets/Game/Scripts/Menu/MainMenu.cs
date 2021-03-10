using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using RenderHeads.Media.AVProVideo;

public class MainMenu : MonoBehaviour {

    public static MainMenu Instance;

    public int levelIndex;
    public bool hasSettings;

    public GameObject[] chapterButtons;

    public GameObject journaleButton;
    public GameObject Journale_Sub;
    public JournaleMain journaleSkript;
    public GameObject journale;


    public GameObject main;
    public GameObject option;
    public GameObject chapterWindow;
    public GameObject kapitel;
    public GameObject fortsetzen;
    public GameObject creditsVideo;
    public DisplayUGUI mediaDisplay;
    public GameObject WarnungPanel;
    public Button jaButton;
    public GameObject welcome;
    public GameObject flags;

    public Slider volumeSliderMusic;
    public Slider volumeSliderMaster;
    public Slider volumeSliderVoice;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Button setDefault;
    public Toggle subtitleToggle;
    public Dropdown languageDropdown;
    public Button germanButton;
    public Button englishButton;

    private Einstellungen einstellungen;
    private AudiFade audioFade;

    Resolution[] resolutions;

    private void Awake()
    {
        Instance = this;
        Load();
        Cursor.visible = true;
        
    }
    // Use this for initialization
    void Start () {

        einstellungen = GameObject.Find("EinstellungenSkript").GetComponent<Einstellungen>();
        audioFade = GameObject.Find("BGMusic").GetComponent<AudiFade>();
        //kapitel = GameObject.Find("Kapitel");
        //fortsetzen = GameObject.Find("Fortsetzen");

        if (levelIndex == 0)
        {
            fortsetzen.SetActive(false);
            kapitel.SetActive(false);
        }

        if (levelIndex !=13)
        {
            journaleButton.SetActive(false);
        }

        /*main = GameObject.Find("Main");
        option = GameObject.Find("OptionsMenu");
        chapterWindow = GameObject.Find("Chapter");
        creditsVideo = GameObject.Find("VideoDisplay");

        resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
        volumeSliderMusic = GameObject.Find("Slider").GetComponent<Slider>();
        volumeSliderMaster = GameObject.Find("SliderMaster").GetComponent<Slider>();
        volumeSliderVoice = GameObject.Find("SliderVoice").GetComponent<Slider>();
        fullscreenToggle = GameObject.Find("VollbildToggle").GetComponent<Toggle>();
        setDefault = GameObject.Find("Default").GetComponent<Button>();
        subtitleToggle = GameObject.Find("SubtitleToggle").GetComponent<Toggle>();
        languageDropdown = GameObject.Find("LanguageDropdown").GetComponent<Dropdown>();*/



        for (int i = 0; i < chapterButtons.Length; i++)
        {
            chapterButtons[i].GetComponent<Image>().enabled = false;
            chapterButtons[i].GetComponent<Button>().interactable = false;
        }

        einstellungen.updateDropdown(resolutionDropdown);
        einstellungen.updateValues(volumeSliderMusic, volumeSliderMaster, volumeSliderVoice, fullscreenToggle, subtitleToggle, languageDropdown);

        welcome.SetActive(false);
        //flags.SetActive(false);
        option.SetActive(false);
        chapterWindow.SetActive(false);
        creditsVideo.SetActive(false);
        WarnungPanel.SetActive(false);
        Journale_Sub.SetActive(false);

        Listeners();
    }
	
    void Listeners()
    {
        germanButton.onClick.AddListener(() => einstellungen.SetLanguageButton(1));
        englishButton.onClick.AddListener(() => einstellungen.SetLanguageButton(0));
        volumeSliderMusic.onValueChanged.AddListener(delegate { einstellungen.SetVolume(volumeSliderMusic); });
        volumeSliderMaster.onValueChanged.AddListener(delegate { einstellungen.SetVolumeMaster(volumeSliderMaster); });
        volumeSliderVoice.onValueChanged.AddListener(delegate { einstellungen.SetVolumeVoice(volumeSliderVoice); });
        fullscreenToggle.onValueChanged.AddListener(delegate { einstellungen.SetFullscreen(fullscreenToggle); });
        resolutionDropdown.onValueChanged.AddListener(delegate { einstellungen.SetResolution(resolutionDropdown); });
        subtitleToggle.onValueChanged.AddListener(delegate { einstellungen.SetSubtitle(subtitleToggle); });
        languageDropdown.onValueChanged.AddListener(delegate { einstellungen.SetLanguage(languageDropdown); });
        setDefault.onClick.AddListener(() => einstellungen.SetDefault(volumeSliderMusic, volumeSliderMaster, volumeSliderVoice, fullscreenToggle, resolutionDropdown, subtitleToggle, languageDropdown));

        jaButton.onClick.AddListener(() => doWarnung(levelIndex));
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (journale.activeInHierarchy)
            {
                journale.SetActive(false);
            }
            else 
            {
                backToMain();
            }
        }

        if (Input.anyKey && welcome.activeInHierarchy)
        {
            welcome.SetActive(false);
            //if (!hasSettings)
            //{
            //    SetFlags();
            //}
        }

        if (mediaDisplay._mediaPlayer.Control.IsFinished())
        {
            creditsVideo.SetActive(false);
            main.SetActive(true);
            mediaDisplay._mediaPlayer.Control.Pause();
            mediaDisplay._mediaPlayer.Control.Rewind();
        }
    }

    public void backToMain()
    {
        option.SetActive(false);
        chapterWindow.SetActive(false);
        creditsVideo.SetActive(false);
        Journale_Sub.SetActive(false);

        main.SetActive(true);
        mediaDisplay._mediaPlayer.Control.Pause();
        mediaDisplay._mediaPlayer.Control.Rewind();
    }

    public void Load()
    {
        levelIndex = SaveLoadManager.LoadGameState();
    }

    public void SetFlags()
    {
        flags.SetActive(true);
    }

    public void setOptions()
    {
        main.SetActive(false);
        option.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void setChapterWindow()
    {
        main.SetActive(false);
        chapterWindow.SetActive(true);

        for (int i = 0; i < (levelIndex); i++)
        {
            chapterButtons[i].GetComponent<Image>().enabled = true;
            chapterButtons[i].GetComponent<Button>().interactable = true;
        }


    }

    public void setWarnung(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        WarnungPanel.SetActive(true);
        
    }

    public void doWarnung(int levelIndex)
    {


        if (chapterWindow.activeInHierarchy)
        {
            LoadingScreenManager.LoadScene(levelIndex);
        }
        else if (main.activeInHierarchy)
        {
            newGame();
        }
    }

    public void newGame()
    {
        //StartCoroutine(audioFade.FadeAudioOut(audioFade.gameObject.GetComponent<AudioSource>()));
        LoadingScreenManager.LoadScene(1);
    }

    public void Credits()
    {
        main.SetActive(false);
        creditsVideo.SetActive(true);
        mediaDisplay._mediaPlayer.Play();
    }

    public void Fortsetzen()
    {
        //StartCoroutine(audioFade.FadeAudioOut(audioFade.gameObject.GetComponent<AudioSource>()));
        LoadingScreenManager.LoadScene(levelIndex);
    }

    public void Journale()
    {
        main.SetActive(false);
        Journale_Sub.SetActive(true);
    }

    public void JournalRene()
    {
        journaleSkript.display(true);
    }

    public void JournalElena()
    {
        journaleSkript.display(false);
    }

}
