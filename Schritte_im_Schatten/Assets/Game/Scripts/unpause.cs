using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unpause : MonoBehaviour {
    private VideoController vidcont;
    // Use this for initialization
    void Awake () {

        vidcont = Camera.main.GetComponent<VideoController>();

    }
	
    public void unPause()
    {
        vidcont.unpauseVideo();
    }
}
