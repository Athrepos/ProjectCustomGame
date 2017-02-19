using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

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
				Debug.Log ("The nextDialog is not defined in the dialog with id = '" + this.id + "'");
				return new int[0];
			}
			else
				if(this.nextDialog.Length == 0)
					Debug.Log ("The nextDialog is empty in the dialog with id = '"+this.id+"'");
			
			return nextDialog;

		} 
		set
		{ 
			nextDialog = value;
		}
	}

}
