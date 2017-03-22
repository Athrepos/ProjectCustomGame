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

using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;

//utilizar hashset, para não repetir objetos, para identificar se é igual é preciso implementar o metodo Equals da classe

namespace NatanielSoaresRodrigues.ProjectCustomGame.Xml
{
	public class XmlManagement {

		Type type;
		string fileName;

		public XmlManagement(string fileName, Type type){
			this.fileName = fileName;
			this.type = type;

		}

		public object openFile(){
			TextAsset xml = (TextAsset) Resources.Load("xml/"+fileName);

			object container;

			using(MemoryStream ms = new MemoryStream(xml.bytes)){
				var serializer = new XmlSerializer (type);
				container = serializer.Deserialize (ms);
			}

			return container;
		}

		void testDialogues(List<Dialog> listD)
		{

			for (int i = 0; i < listD.Count; i++) {
				for (int j = i+1; j < listD.Count; j++) {
					if (listD [i].Id == listD [j].Id)
						throw new Exception ("There is Duplicate id's in your Dialogues XML file");

					if(listD[i].NextDialog != null && 
						listD[i].NextDialog.Length > 5)
						throw new Exception ("The Maximum number of options is 5");
				}
			}
		}

	}

}