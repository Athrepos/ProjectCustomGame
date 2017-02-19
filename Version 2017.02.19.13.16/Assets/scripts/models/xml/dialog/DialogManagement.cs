using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class DialogManagement {

	string path = Application.dataPath+"/content/xml/dialogues.xml";

	public List<Dialog> readXml(){
		var serializer = new XmlSerializer (typeof(DialogContainer));
		var stream = new FileStream (path, FileMode.Open);
		DialogContainer container = serializer.Deserialize (stream) as DialogContainer;
		stream.Close ();

		List<Dialog> listD = container.dialogues;
		if (!testDialogues (listD)) {
			
			Debug.LogError ("There is Duplicate id's in your Dialogues XML file");
	
			listD = null;
		}

		return listD;
	}

	bool testDialogues(List<Dialog> listD)
	{

		for (int i = 0; i < listD.Count; i++) {
			for (int j = i+1; j < listD.Count; j++) {
				if (listD [i].id == listD [j].id)
					return false;
			}
		}
		return true;
	}

}
