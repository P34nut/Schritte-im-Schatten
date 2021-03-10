using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using I2.Loc;

public class Inventar : MonoBehaviour
{

    //private SettingsButton settingsButton;
    bool isButton;
    bool pause = false;

    private int testIndex;
    private BGMusic bgm_script;

    private GameObject BGMusic;
    //private GameObject ChangeMusic;

    public GameObject inventoryElena;
    public GameObject inventoryRene;

    private InventoryElena inventoryBarElena;
    private InventoryRene inventoryBarRene;
    private VideoController vidcont;

    public GameObject from, to;

    public GameObject iconPrefab;
    private static GameObject hoverObject;
    public Canvas canvas;
    private float hoverOffsetY = -25;
    private float hoverOffsetX = 25;

    public AudioClip[] audioclipsRene;
    public AudioClip[] audioclipsElena;
    public AudioSource audioSource;

    public Text subText;

    private void Start()
    {
        BGMusic = GameObject.Find("BGMusic");
        //ChangeMusic = GameObject.Find("AudioSource");
        bgm_script = BGMusic.GetComponent<BGMusic>();

        inventoryBarElena = gameObject.GetComponent<InventoryElena>();
        inventoryBarRene = gameObject.GetComponent<InventoryRene>();
        subText.gameObject.SetActive(false);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(-1) && from != null)
            {
               

                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    moveItem(EventSystem.current.currentSelectedGameObject, -2);
                }
                else if (EventSystem.current.currentSelectedGameObject.tag == "notInteractable")
                {
                    moveItem(EventSystem.current.currentSelectedGameObject, -1);
                }
                else if (EventSystem.current.currentSelectedGameObject.tag == "Interactable")
                {
                    testIndex = EventSystem.current.currentSelectedGameObject.GetComponent<InteractIndex>().Index;
                    moveItem(EventSystem.current.currentSelectedGameObject, testIndex);
                    //vidcont.boolTest = true;
                }


            }
        }

        if (hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x - hoverOffsetX, position.y - hoverOffsetY);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }

        if (bgm_script.gameIsPaused && !pause)
        {
            pause = true;
            audioSource.Pause();
        }

        if (!bgm_script.gameIsPaused && pause)
        {
            pause = false;
            audioSource.GetComponent<AudioSource>().UnPause();
        }

    }

    public void moveItem(GameObject clicked, int checkIndex)
    {
        
        Debug.Log(checkIndex);
        if (from == null)
        {
            if (inventoryElena.activeInHierarchy || inventoryRene.activeInHierarchy)
            {
                if (clicked.transform.GetChild(0).GetComponent<Image>().sprite != null)
                {
                    from = clicked;
                    from.GetComponent<Image>().color = Color.gray;

                    hoverObject = (GameObject)Instantiate(iconPrefab);
                    hoverObject.GetComponent<Image>().sprite = clicked.transform.GetChild(0).GetComponent<Image>().sprite;
                    hoverObject.name = "Hover";

                    RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                    RectTransform clickedTransform = clicked.transform.GetChild(0).gameObject.GetComponent<RectTransform>();

                    hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                    hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                    hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
                    hoverObject.transform.localScale = clicked.transform.GetChild(0).localScale;

                    inventoryElena.SetActive(false);
                    inventoryRene.SetActive(false);
                    inventoryBarRene.active = false;
                    inventoryBarElena.active = false;
                    SettingsButton.Instance.prefabMenu.SetActive(false);
                }
            }

        }
        else if (to == null)
        {
            if (clicked != null)
            {
                if (clicked.GetComponent<Button>() != null && checkIndex == -1)
                {
                    clicked.GetComponent<Button>().interactable = false;

                }
            }
            else
            {

                isButton = true;

            }
            to = clicked;
            if (checkIndex == -1)
            {

                StartCoroutine(checkSound(to));
            }
            check(checkIndex);
            Destroy(GameObject.Find("Hover"));
            SettingsButton.Instance.prefabMenu.SetActive(true);
        }

        if ((to != null || isButton) && from != null)
        {
            from.GetComponent<Image>().color = Color.white;
            to = null;
            from = null;
            isButton = false;

        }

    }

    public IEnumerator checkSound(GameObject button)
    {
        

        if (inventoryBarElena.elenaScene)
        {
            int rand = Random.Range(0, 2);
            this.GetComponent<AudioSource>().clip = audioclipsElena[rand];
        }
        else
        {
            int rand = Random.Range(0, 3);
            this.GetComponent<AudioSource>().clip = audioclipsRene[rand];
        }

        if (Einstellungen.Instance.currentSubtitle)
        {
            subText.gameObject.SetActive(true);
            subText.text = LocalizationManager.GetTranslation("Subtitle/" + this.GetComponent<AudioSource>().clip.name);
        }
        this.GetComponent<AudioSource>().Play();

        while (this.GetComponent<AudioSource>().isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        subText.text = "";
        subText.gameObject.SetActive(false);

        if (button != null)
        {
            button.GetComponent<Button>().interactable = true;
        }

    }

    public void check(int checkIndex)
    {

        vidcont = Camera.main.GetComponent<VideoController>();
        if (inventoryBarElena.elenaScene)
        {
            if (checkIndex > -1 && checkIndex < 4)
            {
                if (inventoryBarElena.itemSprites[checkIndex] == from.transform.GetChild(0).GetComponent<Image>().sprite)
                {
                    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    switch (checkIndex)
                    {
                        case 0:
                            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
                            vidcont.SetSelectedOption(16);
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
                else
                {
                    to.GetComponent<Button>().interactable = false;
                    StartCoroutine(checkSound(to));
                }
            }


        }
        else if (checkIndex > -1 && checkIndex < 8)
        {
            if (inventoryBarRene.itemSprites[checkIndex] == from.transform.GetChild(0).GetComponent<Image>().sprite)
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                Debug.Log("TRUE");
                switch (checkIndex)
                {

                    case 0:
                        vidcont.SetSelectedOption(4);                      
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        inventoryBarRene.destroyImage(checkIndex);
                        vidcont.SetSelectedOption(9);
                        vidcont.didPhantom();
                        break;
                    case 4:

                        if (inventoryBarRene.isNadel)
                        {
                            inventoryBarRene.destroyImage(1);
                            inventoryBarRene.destroyImage(4);
                            vidcont.SetSelectedOption(49);
                        }
                        else
                        {
                            UI_S4.Instance.checkAudio(UI_S4.Instance.karte_audio);
                        }


                        break;
                    case 5:
                        inventoryBarRene.destroyImage(checkIndex);
                        inventoryBarRene.setImage(6);
                        vidcont.SetSelectedOption(11);
                        vidcont.setKey();
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                }
            }
            else
            {
                to.GetComponent<Button>().interactable = false;
                StartCoroutine(checkSound(to));
            }
        }
        else if (checkIndex == -3)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            if (inventoryBarRene.itemSprites[0] == from.transform.GetChild(0).GetComponent<Image>().sprite)
            {
                vidcont.SetSelectedOption(6);
            }
            else if (inventoryBarRene.itemSprites[6] == from.transform.GetChild(0).GetComponent<Image>().sprite)
            {
                vidcont.SetSelectedOption(17);
                
                StartCoroutine(bgm_script.SwitchTrack(15));
                
            }
            else
            {
                to.GetComponent<Button>().interactable = false;
                StartCoroutine(checkSound(to));
            }
        }
    }

    public void disableSubtitle()
    {
        StartCoroutine(disableSubText());
    }

    public IEnumerator disableSubText()
    {
        
        while (this.GetComponent<AudioSource>().isPlaying)
        {
            Debug.Log("DISABLE");
            yield return new WaitForEndOfFrame();
        }
        subText.text = "";
        subText.gameObject.SetActive(false);
        StopCoroutine(disableSubText());
    }

}
