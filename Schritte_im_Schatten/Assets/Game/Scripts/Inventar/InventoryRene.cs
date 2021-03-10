using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRene : MonoBehaviour {

    private SettingsButton settingsButton;

    public Sprite[] itemSprites;
    public GameObject[] itemImage;

    public bool[] isActive;
    public int[] spriteIndexRene;
    

    public AudioClip[] audioClip;
    public GameObject inventoryBarRene;
    public GameObject inventoryBarElena;
    public bool active;
    public bool isNadel;
    public bool tookNadelS2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isActive = new bool[8];
        spriteIndexRene = new int[8];
    }

    // Use this for initialization
    void Start () {

        Load();

        for (int i = 0; i < 8; i++)
        {
            if (isActive[i])
            {
                itemImage[i].SetActive(true);
                itemImage[i].GetComponent<Image>().sprite = itemSprites[spriteIndexRene[i]];
                itemImage[i].GetComponent<AudioIndex>().audioIndex = spriteIndexRene[i];
                if (spriteIndexRene[i]==1)
                {
                    isNadel = true;
                }
            }
            else
            {
                itemImage[i].SetActive(false);
            }
        }

        inventoryBarRene.SetActive(false);
        active = false;
    }

    public void updateInventory(int sceneIndex)
    {
        if (sceneIndex<=2)
        {
            destroyAll();
        }
        else if (sceneIndex <= 4)
        {
            if (tookNadelS2 && !isNadel)
            {
                setImage(1);
            }
            else
            {
                destroyImage(1);
            }
            destroyImage(4);
            destroyImage(5);
            destroyImage(6);
            destroyImage(7);
        }
        else if (sceneIndex <= 6)
        {
            destroyImage(5);
            destroyImage(6);
        }
    }

    public void Load()
    {
        SaveLoadManager.LoadInventarRene(this);
    }

    public void setImage(int spriteIndex)
    {
        for (int i = 0; i < itemImage.Length; i++)
        {
            if (itemImage[i].GetComponent<Image>().sprite == null)
            {
                itemImage[i].GetComponent<Image>().sprite = itemSprites[spriteIndex];
                itemImage[i].GetComponent<AudioIndex>().audioIndex = spriteIndex;
                spriteIndexRene[i] = spriteIndex;
                isActive[i] = true;
                if (spriteIndexRene[i] == 1)
                {
                    if (VideoController.Instance.currentSceneIndex == 2)
                    {
                        tookNadelS2 = true;
                    }
                    isNadel = true;
                }
                itemImage[i].SetActive(true);
                SaveLoadManager.SaveInventarRene(this);
                return;
            }
        }
    }

    public void destroyImage(int spriteIndex)
    {
        for (int i = 7; i > -1; i--)
        {
            if(itemImage[i].GetComponent<Image>().sprite == itemSprites[spriteIndex])
            {
                itemImage[i].GetComponent<Image>().sprite = null;
                itemImage[i].GetComponent<AudioIndex>().audioIndex = 0;
                if (spriteIndexRene[i] == 1)
                {
                    isNadel = false;
                }
                spriteIndexRene[i] = 0;
                isActive[i] = false;
                
                itemImage[i].SetActive(false);
                sortInventory();
                SaveLoadManager.SaveInventarRene(this);
                return;
            }
        }
    }

    public void destroyAll()
    {
        for (int i = 7; i > -1; i--)
        {

            itemImage[i].GetComponent<Image>().sprite = null;
            itemImage[i].GetComponent<AudioIndex>().audioIndex = 0;
            spriteIndexRene[i] = 0;
            isActive[i] = false;
            isNadel = false;
            tookNadelS2 = false;
            itemImage[i].SetActive(false);
            SaveLoadManager.SaveInventarRene(this);

        }
    }

    public void activate()
    {
        inventoryBarRene.SetActive(true);
        active = true;
    }

    public void deactivate()
    {
        inventoryBarRene.SetActive(false);
        active = false;
            
    }

    public void sortInventory()
    {
        for (int i = 0; i < 7; i++)
        {
            if (isActive[i] == false && isActive[i+1] == true)
            {
                setImage(spriteIndexRene[i + 1]);
                destroyImage(spriteIndexRene[i]);
                //Debug.Log("KLAPPT");
                return;
            }
        }
    }


}
