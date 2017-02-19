using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("DialoguesCollection")]
public class DialogContainer {

	[XmlArray("dialogues")]
	[XmlArrayItem("dialog")]
	public List<Dialog> dialogues = new List<Dialog>();

}
