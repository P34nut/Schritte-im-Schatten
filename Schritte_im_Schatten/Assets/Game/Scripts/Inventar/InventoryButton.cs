using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using I2.Loc;

public class InventoryButton : MonoBehaviour, IPointerClickHandler {

    private Inventar inventory;
    private AudioSource audioSource;
    private Image[] itemImage;
    private AudioIndex audioInd;
    private InventoryElena inventoryElena;
    private InventoryRene inventoryRene;
    private Text subText;

    private void Awake()
    {
        //audioSource = this.GetComponentInChildren<AudioSource>();
        itemImage = GetComponentsInChildren<Image>();
        audioInd = GetComponentInChildren<AudioIndex>();
    }

    private void Start()
    {
        
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
        audioSource = inventory.GetComponent<AudioSource>();
        inventoryElena = inventory.GetComponent<InventoryElena>();
        inventoryRene = inventory.GetComponent<InventoryRene>();
        subText = inventory.subText;
        
    }

    private void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {

            inventory.moveItem(EventSystem.current.currentSelectedGameObject, -1);


        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
             Debug.Log("Middle click");

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            
            if (itemImage[1].sprite != null)
            {
                if (inventoryElena.elenaScene)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = inventoryElena.audioClip[audioInd.audioIndex];
                        if (Einstellungen.Instance.currentSubtitle)
                        {
                            subText.gameObject.SetActive(true);
                            subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSource.clip.name);
                        }
                        audioSource.Play();
                        //StartCoroutine(inventory.disableSubText());
                        inventory.disableSubtitle();
                    }
                }
                else
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = inventoryRene.audioClip[audioInd.audioIndex];
                        if (Einstellungen.Instance.currentSubtitle)
                        {
                            subText.gameObject.SetActive(true);
                            subText.text = LocalizationManager.GetTranslation("Subtitle/" + audioSource.clip.name);
                        }
                        audioSource.Play();
                        //StartCoroutine(inventory.disableSubText());
                        inventory.disableSubtitle();
                    }
                }
            }




        }
    }


}



