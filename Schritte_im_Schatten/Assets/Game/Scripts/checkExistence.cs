using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkExistence : MonoBehaviour {

    public GameObject bgMusicPrefab;
    public GameObject inventoryPrefab;
    public GameObject einstellungenPrefab;

    private GameObject bgMusic;
    private GameObject inventory;
    private GameObject einstellungen;

    private void Awake()
    {
        if (GameObject.Find("BGMusic"))
        {
            Debug.Log("exist" + bgMusicPrefab);
        }
        else
        {
            Debug.Log("not exist" + bgMusicPrefab);
            bgMusic = Instantiate<GameObject>(bgMusicPrefab);
            bgMusic.name = "BGMusic";
        }
        if (GameObject.Find("Inventory"))
        {
            Debug.Log("exist" + inventoryPrefab);
        }
        else
        {
            Debug.Log("not exist" + inventoryPrefab);
            inventory = Instantiate<GameObject>(inventoryPrefab);
            inventory.name = "Inventory";
        }

        //Nicht in Demo

        if (GameObject.Find("EinstellungenSkript"))
        {
            Debug.Log("exist" + einstellungenPrefab);
        }
        else
        {
            Debug.Log("not exist" + einstellungenPrefab);
            einstellungen = Instantiate<GameObject>(einstellungenPrefab);
            einstellungen.name = "EinstellungenSkript";
        }
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
