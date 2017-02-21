/*Copyright 2017 Nataniel Soares Rodrigues

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogController : MonoBehaviour {

	List<Dialog> mainDialoguesList;
	Dialog currentDialog;
	Dialog[] dialogOptions;
	bool checkInit;

	// Use this for initialization
	void Start () {
		mainDialoguesList = new DialogManagement().readXml ();

		checkInit = initializeDialog ();

		if (!checkInit)
			return;
		

	}
	
	// Update is called once per frame
	void Update () {
		if (!checkInit)
			return;
		
		if (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp(KeyCode.Return)) {
			checkDialog ();
		}

		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			selectOption (0);
		}

		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			selectOption (1);
		}
	}

	void checkDialog()
	{
		int numbersOfD = currentDialog.NextDialog.Length; //number of options for the player choose
		if (numbersOfD > 0) {
			// found more than zero option

			if (numbersOfD > 1) {
				//found more than one option

				dialogOptions = new Dialog[numbersOfD];

				int i = 0;
				foreach (int dialogId in currentDialog.NextDialog) {
					Dialog nxtDialog = findDialog (mainDialoguesList, dialogId);

					if (nxtDialog != null) {
						dialogOptions [i] = nxtDialog;
						Debug.Log ((i+1)+" - "+nxtDialog.message);
					} else
						Debug.LogError ("The Dialogue with id : '" + dialogId + "' is not found : Current Dialog id = '"+currentDialog.id+"'");

					i++;
				}
			} else { 
				//found only one option then move on

				loadCurrentDialog(currentDialog.NextDialog[0]);
			}

		} else 	{
			//did not found any option

			Debug.Log ("The dialogue cycle has been broken : Current Dialog id = '"+currentDialog.id+"'");
		}
	}

	void selectOption(int opt){
		//select a option that the player choose

		if (dialogOptions == null) {
			//if have no options move on

			checkDialog ();
		}
		else if((opt+1) > dialogOptions.Length)	{
			//if selected a option that doesn't exist

			return;
		}
		else {
			//selected a valid option

			int nOfresponseOpt = dialogOptions [opt].NextDialog.Length; //number of option redirects

			if (nOfresponseOpt > 1 || nOfresponseOpt == 0) {
				// the option must be have just a one option to redirect

				Debug.LogError ("A dialog option should have only one dialog redirect : Option Dialog id = '" + dialogOptions [opt].id + "'");
				return;
			}

			loadCurrentDialog (dialogOptions [opt].NextDialog[0]);
			//checkDialog ();
		}
	}

	void loadCurrentDialog(int id){
		//change a current dialog to a new dialog

		dialogOptions = null;
		Dialog nxtDialog = findDialog (mainDialoguesList, id);
		if (nxtDialog != null) {
			currentDialog = nxtDialog;
			Debug.Log (currentDialog.message);
		}
		else
			Debug.LogError ("The Dialogue with id : '" + id + "' is not found : Current Dialog id = '"+currentDialog.id+"'");
	}

	bool initializeDialog(){
		//initialize a currentDialog with the first item of the mainDialogList


		if (mainDialoguesList != null && mainDialoguesList.Count > 0) {
			currentDialog = mainDialoguesList [0];
			Debug.Log (currentDialog.message);
			return true;
		} else {
			Debug.LogError ("The Dialog List is Empty");
			return false;
		}
	}

	Dialog findDialog(List<Dialog> dialogList, int id)
	{
		//find a dialog in a list

		return dialogList.Find (x => x.id == id);
	}
}
