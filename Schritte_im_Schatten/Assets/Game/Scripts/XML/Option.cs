using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;


public class Option
{
    [XmlArray("Texte"), XmlArrayItem("Text")]
    public string[] textXML;

    [XmlElement("nextVideoID")]
    public int nextVideoID;

    [XmlElement("interactive")]
    public bool interactive;

}