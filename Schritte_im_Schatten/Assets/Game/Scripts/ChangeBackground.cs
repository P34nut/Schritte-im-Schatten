using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour {

    private VideoController vidcont;
    public Sprite[] backgrounds;

	// Use this for initialization
	void Start () {

        vidcont = Camera.main.GetComponent<VideoController>();
		if(vidcont.currentSceneIndex == 3 || vidcont.currentSceneIndex == 5 || vidcont.currentSceneIndex == 7 ||
            vidcont.currentSceneIndex == 10 || vidcont.currentSceneIndex == 12)
        {
            GetComponent<Image>().sprite = backgrounds[0];
        }
        else
        {
            GetComponent<Image>().sprite = backgrounds[1];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
