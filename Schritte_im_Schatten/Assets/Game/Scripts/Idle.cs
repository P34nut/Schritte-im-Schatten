using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour {

    private VideoController vidcont;
    private Content_xml xml;

    public float timeLeft;
    private float timer = 60f;
    bool catchCursor = true;
    float cursorPosition;

    public AudioClip[] audioclips;

	void Start () {
        vidcont = Camera.main.GetComponent<VideoController>();
        xml = Camera.main.GetComponent<Content_xml>();
    }

    private void Update()
    {
        if (catchCursor)
        {
            catchCursor = false;
            cursorPosition = Input.GetAxis("Mouse X");
        }

        if (Input.GetAxis("Mouse X") == cursorPosition)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft<0)
            {
                playSound();
                timeLeft = timer;
                catchCursor = true;
            }
        }
        else
        {
            timeLeft = timer;
        }

    }

    public void playSound()
    {
        //switch für szene/im Video damit nicht immer gespielt wird
    }


}
