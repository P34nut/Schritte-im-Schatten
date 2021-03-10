using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

    public static TimerBar Instance { get; private set; }

    Image timerBar;
    public float timeAmt;
    float time;
    private VideoController vidcont;
    public bool isPaused;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {

        vidcont = Camera.main.GetComponent<VideoController>();
        timeAmt = vidcont.answerTime;
        timerBar = this.GetComponent<Image>();
        time = timeAmt;
        
	}
	
	// Update is called once per frame
	void Update () {
		if(time > 0 && !isPaused)
        {
            
            time -=  Time.deltaTime;
            timerBar.fillAmount = time / timeAmt;
        }
	}

    private void OnEnable()
    {
        time = timeAmt;
    }

   
}
