using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class UI_S11 : MonoBehaviour {

    private int id;
    private bool interactive;

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

    private Vector3 mouseDownPos;

    public GameObject prefabImage;
    private GameObject image;
    private GameObject checkImage;

    public Sprite[] pfeil;
    public Sprite haken;
    public Sprite kreuz;
    public bool clicked;
    public bool allowed;
    public float quickTime = 1.5f;

    public GameObject prefabDialog;

    private GameObject dialog_1;
    private GameObject dialog_2;
    private GameObject dialog_3;
    private GameObject timer;
    private Text dialog_text;

    private bool isTrue;

    public enum ButtonState { none, dialog, qte }
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

        prefabDialog = Instantiate<GameObject>(prefabDialog);
        prefabImage = Instantiate<GameObject>(prefabImage);

        image = GameObject.Find("Pfeil_Image");
        checkImage = GameObject.Find("CheckImage");

        dialog_1 = GameObject.Find("Dialog_1");
        dialog_2 = GameObject.Find("Dialog_2");
        dialog_3 = GameObject.Find("Dialog_3");
        timer = GameObject.Find("Timer");
        dialog_text = GameObject.Find("Dialog_Subtitle").GetComponent<Text>();

        prefabDialog.SetActive(false);
        image.SetActive(false);
        prefabImage.SetActive(false);
    }

    private void Update()
    {
        checkMouse();

        if (id == 11 && !isTrue)
        {
            //bgSource.loop = false;
            //bgSource.clip = musicChange.audioChangeImLevel[13];
            //if (!bgSource.isPlaying)
            //{
            //    StartCoroutine(fade.FadeAudioOut(changeSource, true));
            //    StartCoroutine(fade.FadeAudioIn(bgSource, true));
            isTrue = true;
            //}

            StartCoroutine(bgm_script.SwitchTrack(22));

        }

        if (vidcont.id == 23 && isTrue)
        {
            StartCoroutine(bgm_script.SwitchTrack(32));
            isTrue = false;
        }

    }

    public void display()
    {

        id = vidcont.id;
        
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
                setDialog(id);
                checkNext(id);
                break;
            case ButtonState.qte:

                setQTE();
                break;

        }
    }

    public void setQTE()
    {
        //allowed = true;
        image.SetActive(false);
        checkImage.SetActive(false);
        StartCoroutine(show());
        changeImagePosition();
    }

    public void changeImagePosition()
    {
        switch (id)
        {
            case 11:
                image.transform.localPosition = new Vector2(350, 250);
                checkImage.transform.localPosition = new Vector2(350, 250);
                break;
            case 14:
                image.transform.localPosition = new Vector2(0, 182);
                checkImage.transform.localPosition = new Vector2(0, 182);
                break;
            case 17:
                image.transform.localPosition = new Vector2(0, 0);
                checkImage.transform.localPosition = new Vector2(0, 0);
                break;
            case 20:
                image.transform.localPosition = new Vector2(0, 0);
                checkImage.transform.localPosition = new Vector2(0, 0);
                break;
        }
        
    }

    public IEnumerator show()
    {
        image.SetActive(false);
        prefabImage.SetActive(false);

        if (xml.getState(id) == 2)
        {
            switch (id)
            {
                case 11:
                    image.GetComponent<Image>().sprite = pfeil[0];
                    break;
                case 14:
                    image.GetComponent<Image>().sprite = pfeil[1];
                    break;
                case 17:
                    image.GetComponent<Image>().sprite = pfeil[2];
                    break;
                case 20:
                    image.GetComponent<Image>().sprite = pfeil[3];
                    break;
            }
            //yield return new WaitForSeconds(1f);
            prefabImage.SetActive(true);
            yield return new WaitWhile(() => vidcont.time <= vidcont.duration - quickTime);
            allowed = true;
            image.SetActive(true);
            yield return new WaitForSeconds(quickTime);

            if (clicked)
            {
                //if (id == 17 || id == 20)
                //{
                //    vidcont.SetSelectedOption(id+1);
                //}
                //else
                //{
                    vidcont.SetSelectedOption(xml.getBlock(id).option[0].nextVideoID);
                image.SetActive(false);
                checkImage.SetActive(false);
                allowed = false;
                //}


            }
            else
            {

                //if (id == 17 || id == 20)
                //{
                //    vidcont.SetSelectedOption(19);
                //}
                //else
                //{

                vidcont.SetSelectedOption(xml.getBlock(id).option[1].nextVideoID);
                image.SetActive(false);
                checkImage.SetActive(false);
                allowed = false;
                //}
            }
        }
        else
        {
            clicked = false;
        }
    }

    public void checkMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (allowed)
            {
                switch (id)
                {
                    case 11:
                        if (Input.mousePosition.y > mouseDownPos.y && Input.mousePosition.x > mouseDownPos.x && 
                            (Input.mousePosition.y - mouseDownPos.y) > 50 && (Input.mousePosition.x - mouseDownPos.x) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;
                        case 14:
                        if (Input.mousePosition.y > mouseDownPos.y && (Input.mousePosition.y - mouseDownPos.y) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;
                    case 17:
                        if (Input.mousePosition.y > mouseDownPos.y && Input.mousePosition.x < mouseDownPos.x && 
                            (Input.mousePosition.y - mouseDownPos.y) > 50 && (mouseDownPos.x - Input.mousePosition.x) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;
                    
                    case 20:
                        if (Input.mousePosition.y < mouseDownPos.y && (mouseDownPos.y - Input.mousePosition.y) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;
                }
            }
        }
    }

    public void setDialog(int id)
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

    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

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

    public void disable()
    {
        prefabDialog.SetActive(false);
    }




}
