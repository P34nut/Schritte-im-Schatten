using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    public static SettingsButton Instance;

    private Content_xml xml;
    private VideoController vidcont;
    private PauseMenu pause;
    private Journal journalMenu;
    private UI_S2 ui2;

    //Settings
    private GameObject inventar;
    private GameObject journal;
    private GameObject settings;
    private InventoryRene invRene;
    private InventoryElena invElena;
    private GameObject invBarRene;
    private GameObject invBarElena;

    public GameObject prefabMenu;

    private void Awake()
    {
        Instance = this;
        invRene = GameObject.Find("Inventory").GetComponent<InventoryRene>();
        invElena = GameObject.Find("Inventory").GetComponent<InventoryElena>();



    }

    // Use this for initialization
    void Start () {

        prefabMenu = Instantiate<GameObject>(prefabMenu);
        prefabMenu.name = "Menu";
        vidcont = this.GetComponent<VideoController>();
        pause = this.GetComponent<PauseMenu>();
        journalMenu = this.GetComponent<Journal>();
        ui2 = Camera.main.GetComponent<UI_S2>();

        //Finden aller Buttons
        inventar = GameObject.Find("Inventar");
        journal = GameObject.Find("Journal");
        settings = GameObject.Find("Settings");
        

        prefabMenu.SetActive(false);
        inventar.SetActive(false);
        journal.SetActive(false);
        settings.SetActive(false);
    }

    private void Update()
    {


        if (invRene.active || invElena.active)
        {
            disableInventory();
        }
        else
        {
            set_inventar(inventar);
        }
    }

    public void disableInventory()
    {
        inventar.GetComponent<Button>().onClick.AddListener(() => invRene.deactivate());
        inventar.GetComponent<Button>().onClick.AddListener(() => invElena.deactivate());
    }
 

    public void disableSettings()
    {
        prefabMenu.SetActive(false);
        invRene.deactivate();
        invElena.deactivate();
    }

    public void setSettings()
    {
        prefabMenu.SetActive(true);

        set_inventar(inventar);
        set_journal(journal);
        set_settings(settings);

    }

    public void set_inventar(GameObject button)
    {
        button.SetActive(true);
        //Inventar!!!!
             
        if(vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 || 
            vidcont.currentSceneIndex == 10 || vidcont.currentSceneIndex == 12)
        {
            button.GetComponent<Button>().onClick.AddListener(() => invElena.activate());
            invElena.elenaScene = true;
        }
        else
        {
            button.GetComponent<Button>().onClick.AddListener(() => invRene.activate());
            invElena.elenaScene = false;
        }

            
        
        
    }

    public void set_journal(GameObject button)
    {
        if(vidcont.currentSceneIndex == 2)
        {
            if (!ui2.notizbuch.GetComponent<Button>().interactable)
            {
                button.SetActive(true);
                button.GetComponent<Button>().onClick.AddListener(delegate { journalMenu.display(); });
            }
        }
        else
        {
            button.SetActive(true);
            button.GetComponent<Button>().onClick.AddListener(delegate { journalMenu.display(); });
        }

        

        
        //Journal!!!!
        
    }

    public void set_settings(GameObject button)
    {
        button.SetActive(true);
        //Settings!!!!
        button.GetComponent<Button>().onClick.AddListener(delegate { vidcont.pauseVideo(); }    );
        button.GetComponent<Button>().onClick.AddListener(delegate { pause.display(); });
    }

}
