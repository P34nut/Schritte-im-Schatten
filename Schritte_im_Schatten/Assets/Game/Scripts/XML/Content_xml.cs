using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RenderHeads.Media.AVProVideo;


public class Content_xml : MonoBehaviour {
 
    public VideoContainer vid;
    public Video video;
    public Option opt;
    public Block text;
    public Preload pre;

    public string path;

    private void Awake()
    {
        vid = VideoContainer.Load(path);
        Debug.Log("loaded " + path);
    }

    // Use this for initialization
    void Start () {
        

    }

    public bool getSubtitle(int id)
    {
        return vid.videos[id].subtitle;
    }
	
    public int getBID(int id)
    {
        return vid.videos[id].blockID;
    }

    public VideoContainer getVidContainer()
    {
        return vid;
    }

    public Video getVideo(int id)
    {
        return vid.videos[id];
    }

    public float getTimer(int id)
    {
        return vid.videos[id].Timer;
    }

    public Block getSpecificBlock(int index)
    {
        return vid.block[index];
    }

    public Block getBlock(int id)
    {
        return vid.block[getBID(id)];
    }

    public bool getLoop(int id)
    {
        return vid.videos[id].loop;
    }

    public int getState(int id)
    {
        return vid.videos[id].state;
    }

    public int getVidCount()
    {
        return vid.videos.Count;
    }

    public int getVID(int id)
    {
        return vid.videos[id].ID;
    }

    public string getVidName(int id)
    {
        return vid.videos[id].name + ".mp4";
    }

    public string getName(int id)
    {
        return vid.videos[id].name;
    }

    public int getOptionCount(int id)
    {
        return getBlock(id).option.Count;
    }

    public int getPreCount(int id)
    {
        return vid.videos[id].preloads.Count;
    }





}
