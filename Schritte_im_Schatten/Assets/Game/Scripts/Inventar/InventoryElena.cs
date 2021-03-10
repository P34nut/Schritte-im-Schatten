using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElena : MonoBehaviour {

    private SettingsButton settingsButton;

    public Sprite[] itemSprites;
    public GameObject[] itemImage;
    public bool[] isActive;
    public int[] spriteIndexElena;
    public AudioClip[] audioClip;
    public GameObject inventoryBarElena;
    public GameObject inventoryBarRene;
    public bool active;
    public bool elenaScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isActive = new bool[8];
        spriteIndexElena = new int[2];
    }

    // Use this for initialization
    void Start () {

        Load();

        for (int i = 1; i < 8; i++)
        {
            if (isActive[i])
            {
                itemImage[i].SetActive(true);
                itemImage[i].GetComponent<Image>().sprite = itemSprites[spriteIndexElena[i]];
                itemImage[i].GetComponent<AudioIndex>().audioIndex = spriteIndexElena[i];
            }
            else
            {
                itemImage[i].SetActive(false);
            }

        }

        inventoryBarElena.SetActive(false);
        active = false;
    }

    public void updateInventory(int sceneIndex)
    {
        destroyImage(1);
    }

    public void Load()
    {
        SaveLoadManager.LoadInventarElena(this );
    }

    public void setImage(int spriteIndex)
    {
        for (int i = 0; i < itemImage.Length; i++)
        {
            if (itemImage[i].GetComponent<Image>().sprite == null)
            {
                itemImage[i].GetComponent<Image>().sprite = itemSprites[spriteIndex];
                itemImage[i].GetComponent<AudioIndex>().audioIndex = spriteIndex;
                spriteIndexElena[i] = spriteIndex;
                isActive[i] = true;
                itemImage[i].SetActive(true);
                SaveLoadManager.SaveInventarElena(this);
                return;
            }
        }
    }

    public void destroyImage(int spriteIndex)
    {
        for (int i = 7; i > 0; i--)
        {
            if(itemImage[i].GetComponent<Image>().sprite == itemSprites[spriteIndex])
            {
                itemImage[i].GetComponent<Image>().sprite = null;
                itemImage[i].GetComponent<AudioIndex>().audioIndex = 0;
                spriteIndexElena[i] = 0;
                isActive[i] = false;
                itemImage[i].SetActive(false);
                SaveLoadManager.SaveInventarElena(this);
                return;
            }
        }
    }

    public void activate()
    {
        inventoryBarElena.SetActive(true);
        active = true;
    }

    public void deactivate()
    {
        inventoryBarElena.SetActive(false);
        active = false;
            
    }

}
