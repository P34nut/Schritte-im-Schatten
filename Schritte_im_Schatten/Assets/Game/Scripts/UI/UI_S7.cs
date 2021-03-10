using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class UI_S7 : MonoBehaviour {

    private bool interactive;
    public int second =0;
    int id;
    bool isFading;
    bool isFading2;

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

    private GameObject dialog_1;
    private GameObject dialog_2;
    private GameObject dialog_3;
    private GameObject timer;
    private Text dialog_text;

    private GameObject telefon;

    public GameObject prefabDialog;
    public GameObject prefabTelefon;

    public enum ButtonState { none, dialog, telefon }
    public ButtonState state;

    private void Awake()
    {
        inventoryElena = GameObject.Find("Inventory").GetComponent<InventoryElena>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
    }

    void Start () {

        setting = this.GetComponent<SettingsButton>();
        xml = this.GetComponent<Content_xml>();
        vidcont = this.GetComponent<VideoController>();
        bgMusic = GameObject.Find("BGMusic");
        bgm_script = bgMusic.GetComponent<BGMusic>();
        //musicChange = bgMusic.GetComponent<Music>();
        //bgSource = bgMusic.GetComponent<AudioSource>();
        //changeSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        prefabDialog = Instantiate<GameObject>(prefabDialog);
        prefabTelefon = Instantiate<GameObject>(prefabTelefon);

        dialog_1 = GameObject.Find("Dialog_1");
        dialog_2 = GameObject.Find("Dialog_2");
        dialog_3 = GameObject.Find("Dialog_3");
        timer = GameObject.Find("Timer");
        dialog_text = GameObject.Find("Dialog_Subtitle").GetComponent<Text>();

        telefon = GameObject.Find("Telefon");

        prefabDialog.SetActive(false);
        prefabTelefon.SetActive(false);

        dialog_1.SetActive(false);
        dialog_2.SetActive(false);
        dialog_3.SetActive(false);
        timer.SetActive(false);
    }

    private void Update()
    {
        if (vidcont.id == 7 && !isFading)
        {
            StartCoroutine(bgm_script.WaitForSwitch(5.5f, 12));
            isFading = true;
        }
        if (vidcont.id == 9 && !isFading2)
        {
            StartCoroutine(bgm_script.WaitForSwitch(32.5f, 12));
            isFading2 = true;
        }
    }

    public void display()
    {

        id = vidcont.id;

        if (id == 1)
        {
            second++;
        }

        state = (ButtonState)xml.getState(id);

        dialog_1.SetActive(false);
        dialog_2.SetActive(false);
        dialog_3.SetActive(false);
        timer.SetActive(false);

        switch (state)
        {
            case ButtonState.none:
                setting.disableSettings();
                checkNext(id);
                break;
            case ButtonState.dialog:
                setting.disableSettings();
                setDialog(id);
                checkNext(id);
                break;
            case ButtonState.telefon:
                setTelefon();
                setting.setSettings();
                break;
        }
    }

    public void setTelefon()
    {
        prefabTelefon.SetActive(true);
        set_button(telefon, xml.getBlock(id).option[0]);
        telefon.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(bgm_script.SwitchTrack(32)));
    }


    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

    }

    public void setDialog(int id)
    {
        if (id == 2)
        {
            if (second == 1)
            {
                if (xml.getVideo(id).preloads.Count >= 1)
                {
                    prefabDialog.SetActive(true);
                    timer.SetActive(true);
                    vidcont.subtitlesUI._text.text = "";
                    vidcont.subtitlesUI.ChangeTextObject(dialog_text);
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
            else if (second >= 2)
            {
                Debug.Log("second");
                if (xml.getVideo(id).preloads.Count >= 1)
                {
                    prefabDialog.SetActive(true);
                    timer.SetActive(true);
                    for (int i = 0; i < 2; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                set_dialog(dialog_1, xml.getSpecificBlock(5).option[i]);
                                break;
                            case 1:
                                set_dialog(dialog_2, xml.getSpecificBlock(5).option[i]);
                                break;

                        }
                    }
                }
            }
        }
        else
        {
            if (xml.getVideo(id).preloads.Count >= 1)
            {
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
        
        

    }

    public void set_dialog(GameObject button, Option o)
    {
        button.SetActive(true);
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
        if (button == dialog_1)
        {
            dialog_2.SetActive(false);
            dialog_3.SetActive(false);
        }
        else if (button == dialog_2)
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

        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.SetActive(true);
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button, -1));
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
        prefabTelefon.SetActive(false);
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

    public void checkAudio(GameObject button)
    {
        if (!button.GetComponent<AudioSource>().isPlaying)
        {
            button.GetComponent<AudioSource>().Play();
        }
    }


}
