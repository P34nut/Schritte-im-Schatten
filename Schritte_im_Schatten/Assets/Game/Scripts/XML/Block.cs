using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

public class Block
{
    [XmlAttribute("id")]
    public int id;

    [XmlElement("Option")]
    public List<Option> option = new List<Option>();
}