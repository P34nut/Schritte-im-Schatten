using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demoscreen : MonoBehaviour {

    private GameObject inventar;

    // Use this for initialization
    void Start () {
        inventar = GameObject.Find("Inventory");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

	}

    public void EndGame()
    {
        Destroy(inventar);
        Application.Quit();
    }

}
