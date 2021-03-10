using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

public class Video {

    [XmlAttribute("name")]
    public string name;

    [XmlElement("Timer")]
    public float Timer;

    [XmlElement("VideoID")]
    public int ID;

    [XmlElement("BlockID")]
    public int blockID;

    [XmlElement("Loop")]
    public bool loop;

    [XmlElement("Subtitle")]
    public bool subtitle;

    [XmlElement("State")]
    public int state;

    [XmlArray("Preloads"), XmlArrayItem("Preload")]
    public List<Preload> preloads = new List<Preload>();

}
