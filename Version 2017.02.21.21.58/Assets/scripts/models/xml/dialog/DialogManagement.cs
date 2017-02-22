/*
   Copyright 2017 Nataniel Soares Rodrigues

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/


using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using UnityEngine;
using System;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Xml
{
	public class DialogManagement {

		string path = Application.dataPath+"/content/xml/dialogues.xml";

		public List<Dialog> readXml(){
			var serializer = new XmlSerializer (typeof(DialogContainer));
			var stream = new FileStream (path, FileMode.Open);
			DialogContainer container = serializer.Deserialize (stream) as DialogContainer;
			stream.Close ();

			List<Dialog> listD = container.dialogues;
			if (!testDialogues (listD)) {
				throw new Exception ("There is Duplicate id's in your Dialogues XML file");
		
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

}