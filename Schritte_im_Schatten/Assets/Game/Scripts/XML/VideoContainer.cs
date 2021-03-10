using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
using System.IO;

[XmlRoot("VideoCollection")]
public class VideoContainer {

    [XmlArray("Videos"), XmlArrayItem("Video")]
    public List<Video> videos = new List<Video>();

    [XmlArray("Textblock"), XmlArrayItem("Block")]
    public List<Block> block = new List<Block>();

    public static VideoContainer Load(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof (VideoContainer));

        StringReader reader = new StringReader(xml.text);

        VideoContainer vid = (VideoContainer)serializer.Deserialize(reader);

        reader.Close();

        return vid;
    }
     

}
