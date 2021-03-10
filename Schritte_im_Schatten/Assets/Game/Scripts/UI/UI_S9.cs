using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_S9 : MonoBehaviour {

    private Content_xml xml;
    private VideoController vidcont;
    private SettingsButton setting;

    public enum ButtonState { none, dialog}
    public ButtonState state;

    // Use this for initialization
    void Start () {
        setting = this.GetComponent<SettingsButton>();
        xml = this.GetComponent<Content_xml>();
        vidcont = this.GetComponent<VideoController>();
    }

    public void display()
    {
        Debug.Log("test");
        int id = vidcont.id;

        checkNext(id);
    }

    public void checkNext(int id)
    {
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;
        
    }

    public void disable()
    {
        return;
    }

}
