using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_S13 : MonoBehaviour {

    private int id;
    private bool interactive;
    private bool isTrue;

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
    private GameObject checkImage;
    private GameObject image;
    private GameObject polizei;

    public Sprite[] pfeil;
    public Sprite haken;
    public Sprite kreuz;
    public bool clicked;
    public bool allowed;
    public float quickTime;

    public GameObject prefabPolizei;


    public enum ButtonState { none, dialog, qte }
    public ButtonState state;

    private void Awake()
    {
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

        prefabPolizei = Instantiate<GameObject>(prefabPolizei);
        prefabImage = Instantiate<GameObject>(prefabImage);

        polizei = GameObject.Find("Polizei");
        image = GameObject.Find("Pfeil_Image");
        checkImage = GameObject.Find("CheckImage");

        prefabPolizei.SetActive(false);
        image.SetActive(false);
        prefabImage.SetActive(false);
        
    }

    private void Update()
    {
        checkMouse();
        if (vidcont.id == 1 && !isTrue)
        {
            isTrue = true;
            StartCoroutine(bgm_script.SwitchTrack(25));
        }

        if (vidcont.id == 2 && isTrue)
        {
            isTrue = false;
            StartCoroutine(bgm_script.SwitchTrack(29));
        }

        if (vidcont.id == 4 && !isTrue)
        {
            isTrue = true;
            StartCoroutine(bgm_script.SwitchTrack(26));
        }

        if (vidcont.id == 5 && isTrue)
        {
            isTrue = false;
            StartCoroutine(bgm_script.SwitchTrack(30));

        }

        if (vidcont.id == 15 && !isTrue)
        {
            StartCoroutine(waiting(48, 27));  
            isTrue = true;
        }

        if (vidcont.id == 17 && isTrue)
        {
            isTrue = false;
            StartCoroutine(bgm_script.SwitchTrack(31));
        }

        if (vidcont.id == 16 && !isTrue)
        {    
            StartCoroutine(waiting(30, 28));
            isTrue = true;
        }

    }

    IEnumerator waiting(float i, int index)
    {
        //changeSource.volume = 0;
        //changeSource.Play();
        yield return new WaitForSeconds(i);
        StartCoroutine(bgm_script.SwitchTrack(index));
        
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
                setDialog(id);
                //checkNext(id);
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
            case 6:
                image.transform.localPosition = new Vector2(0, 0);
                checkImage.transform.localPosition = new Vector2(0, 0);
                break;
            case 9:
                image.transform.localPosition = new Vector2(188, 250);
                checkImage.transform.localPosition = new Vector2(188, 250);
                break;
            case 12:
                image.transform.localPosition = new Vector2(-524, 185);
                checkImage.transform.localPosition = new Vector2(-524, 185);
                break;
        }

    }

    public IEnumerator show()
    {
        image.SetActive(false);
        checkImage.SetActive(false);
        prefabImage.SetActive(false);
        

        if (xml.getState(id) == 2)
        {
            switch (id)
            {
                case 6:
                    image.GetComponent<Image>().sprite = pfeil[0];
                    break;
                case 9:
                    image.GetComponent<Image>().sprite = pfeil[1];
                    //image.SetActive(true);
                    break;
                case 12:
                    image.GetComponent<Image>().sprite = pfeil[2];
                    break;
            }

            if(id == 9)
            {
                Cursor.visible = true;
                prefabImage.SetActive(true);
                image.SetActive(true);
                allowed = true;
                yield return new WaitForSeconds(quickTime);  
            }
            else
            {
                yield return new WaitForSeconds(1f);
                prefabImage.SetActive(true);
                yield return new WaitWhile(() => vidcont.time <= vidcont.duration - 2f - quickTime);
                Cursor.visible = true;
                yield return new WaitWhile(() => vidcont.time <= vidcont.duration - quickTime);
                allowed = true;
                image.SetActive(true);
                yield return new WaitForSeconds(quickTime);
            }
            

            if (clicked)
            {
                vidcont.SetSelectedOption(xml.getBlock(id).option[0].nextVideoID);
                image.SetActive(false);
                checkImage.SetActive(false);
                allowed = false;
            }
            else
            {
                vidcont.SetSelectedOption(xml.getBlock(id).option[1].nextVideoID);
                image.SetActive(false);
                checkImage.SetActive(false);
                allowed = false;
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
                    case 6:
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
                    case 9:
                        if (Input.mousePosition.x > mouseDownPos.x && (Input.mousePosition.x - mouseDownPos.x) > 50)
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
                    case 12:
                        if (Input.mousePosition.y < mouseDownPos.y && Input.mousePosition.x < mouseDownPos.x &&
                            (mouseDownPos.y - Input.mousePosition.y) > 50 && (mouseDownPos.x - Input.mousePosition.x) > 50)
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

    public void set_button(GameObject button, Option o)
    {
        button.SetActive(true);
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.SetSelectedOption(o.nextVideoID); });
        button.GetComponent<Button>().onClick.AddListener(() => disable());
        //button.GetComponent<Button>().onClick.AddListener(() => inventory.moveItem(button,-1));
    }

    public void setDialog(int id)
    {
        Cursor.visible = true;
        prefabPolizei.SetActive(true);
        set_button(polizei, xml.getBlock(id).option[0]);

        vidcont.next = xml.getBlock(id).option[1].nextVideoID;
    }

    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;

    }

    public void disable()
    {
        prefabPolizei.SetActive(false);
    }
}
