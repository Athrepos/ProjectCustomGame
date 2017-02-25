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
using System.Xml;
using System.Xml.Serialization;
using System;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Objs
{
	public class Dialog {

		public int id {get;	set;}

		public string message {get; set;}

		private int[] nextDialog;

		[XmlArray("NextDialog")]
		[XmlArrayItem("value")]
		public int[] NextDialog 
		{ 
			get
			{ 
				if (this.nextDialog == null) {
					throw new Exception ("The nextDialog is not defined in the dialog with id = '" + this.id + "'");
				}
				else
					if(this.nextDialog.Length == 0)
						throw new Exception ("The nextDialog is empty in the dialog with id = '"+this.id+"'");
				
				return nextDialog;

			} 
			set
			{ 
				nextDialog = value;
			}
		}

		public override bool Equals (Object obj)
		{
			Dialog aux = obj as Dialog;
			if (aux.id == this.id)
				return true;
			else
				return false;
		}

	}

}