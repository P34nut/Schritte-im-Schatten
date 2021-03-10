using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Einstellungen : MonoBehaviour {

    public static Einstellungen Instance;

    private Slider volumeSliderMusic;
    private Slider volumeSliderMaster;
    private Slider volumeSliderVoice;
    private Toggle fullscreenToggle;
    private Dropdown resolutionDropdown;
    private Toggle subtitleToggle;
    private Dropdown languageDropdown;

    public bool saving;
   

    public List<string> optionList;

    public int currentResolutionIndex;
    public int defaultResolution=-1;
    public float currentVolumeMusic;
    public float currentVolumeMaster;
    public float currentVolumeVoice;
    public bool currentFullscreen;
    public int currentLanguage;
    public bool currentSubtitle;



    public AudioMixer audioMixer;

    Resolution[] resolutions;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveLoadManager.LoadSettings(this);
        resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
        volumeSliderMusic = GameObject.Find("Slider").GetComponent<Slider>();
        volumeSliderMaster = GameObject.Find("SliderMaster").GetComponent<Slider>();
        volumeSliderVoice = GameObject.Find("SliderVoice").GetComponent<Slider>();
        fullscreenToggle = GameObject.Find("VollbildToggle").GetComponent<Toggle>();
        subtitleToggle = GameObject.Find("SubtitleToggle").GetComponent<Toggle>();
        languageDropdown = GameObject.Find("LanguageDropdown").GetComponent<Dropdown>();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        optionList = new List<string>();


        for (int i = 0; i < resolutions.Length; i++)
        {
            if (i > 0)
            {
                if (resolutions[i].width == resolutions[i - 1].width && resolutions[i].height == resolutions[i - 1].height)
                {

                }
                else
                {
                    string option = resolutions[i].width + "x" + resolutions[i].height;
                    optionList.Add(option);
                }
            }
            else
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                optionList.Add(option);
            }


            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {

                defaultResolution = i;
            }

        }

    }

    public void updateDropdown(Dropdown dropdown)
    {
        if (!saving)
        {
            currentResolutionIndex = defaultResolution;
        }
        dropdown.AddOptions(optionList);
        dropdown.value = currentResolutionIndex;
        Resolution resolution = resolutions[dropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        dropdown.RefreshShownValue();
    }

    public void updateValues(Slider v_Slider, Slider masterSlider, Slider voiceSlider, Toggle f_Toggle, Toggle s_Toggle, Dropdown l_Dropdown)
    {
        v_Slider.value = currentVolumeMusic;
        audioMixer.SetFloat("volume", v_Slider.value);
        masterSlider.value = currentVolumeMaster;
        audioMixer.SetFloat("volumeMaster", masterSlider.value);
        voiceSlider.value = currentVolumeVoice;
        audioMixer.SetFloat("volumeVoice", voiceSlider.value);
        f_Toggle.isOn = currentFullscreen;
        Screen.fullScreen = f_Toggle.isOn;
        s_Toggle.isOn = currentSubtitle;
        l_Dropdown.value = currentLanguage;

    }

    public void SetVolume(Slider currentSlider)
    {
        audioMixer.SetFloat("volume", currentSlider.value);
        currentVolumeMusic = currentSlider.value;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetVolumeMaster(Slider currentSlider)
    {
        audioMixer.SetFloat("volumeMaster", currentSlider.value);
        currentVolumeMaster = currentSlider.value;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetVolumeVoice(Slider currentSlider)
    {
        audioMixer.SetFloat("volumeVoice", currentSlider.value);
        currentVolumeVoice = currentSlider.value;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetFullscreen(Toggle currentToggle)
    {
        Screen.fullScreen = currentToggle.isOn;
        currentFullscreen = currentToggle.isOn;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetResolution(Dropdown dropdown)
    {
        Resolution resolution = resolutions[dropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        currentResolutionIndex = dropdown.value;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetSubtitle(Toggle currentToggle)
    {
        currentSubtitle = currentToggle.isOn;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetLanguage(Dropdown currentDropdown)
    {
        currentLanguage = currentDropdown.value;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetLanguageButton(int index)
    {
        currentLanguage = index;
        SaveLoadManager.SaveSettings(this);
    }

    public void SetDefault(Slider v_Slider, Slider masterSlider, Slider voiceSlider, Toggle f_Toggle, Dropdown r_Dropdown, Toggle s_Toggle, Dropdown l_Dropdown)
    {
        currentLanguage = 0;
        currentSubtitle = false;
        currentFullscreen = true;
        currentVolumeMusic = 0;
        currentVolumeMaster = 0;
        currentVolumeVoice = 0;
        currentResolutionIndex = defaultResolution;
        v_Slider.value = currentVolumeMusic;
        masterSlider.value = currentVolumeMaster;
        voiceSlider.value = currentVolumeVoice;
        f_Toggle.isOn = currentFullscreen;
        r_Dropdown.value = currentResolutionIndex;
        s_Toggle.isOn = currentSubtitle;
        l_Dropdown.value = currentLanguage; 
        SaveLoadManager.SaveSettings(this);
    }
}
