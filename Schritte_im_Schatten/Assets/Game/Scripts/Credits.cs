using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class Credits : MonoBehaviour {

    public MediaPlayer player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (player.Control.IsFinished())
        {
            LoadingScreenManager.LoadScene(0);
        }

	}
}
