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
using System.Collections.Generic;
using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using NatanielSoaresRodrigues.ProjectCustomGame.Xml;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers
{
	public delegate void ShowDialogResponseHandler(object source, DialogEventArgs e);
	public class DialogController : MyObjectController
	{

		event ShowDialogResponseHandler showDialogResponse;

		List<Dialog> mainDialoguesList;
		Dialog currentDialog;
		Dialog[] dialogOptions;


		public DialogController(ShowDialogResponseHandler showDialogResponse){
			
			mainDialoguesList = new XmlManagement().readXmlDialog (); //read the xml

			this.showDialogResponse = showDialogResponse; //set the event listner

			initializeDialog (); //initialize the Dialog system

		}


		public void checkDialog()
		{
			if (currentDialog.NextDialog == null) //next dialog is not found
				return;
			
			int numbersOfD = currentDialog.NextDialog.Length; //number of options for the player choose
			if (numbersOfD > 0) {
				// found more than zero option

				if (numbersOfD > 1) {
					//found more than one option

					dialogOptions = new Dialog[numbersOfD];

					int i = 0;
					foreach (string dialogId in currentDialog.NextDialog) {
						Dialog nxtDialog = findMyObject (mainDialoguesList, dialogId) as Dialog;
						dialogOptions [i] = nxtDialog;

						i++;
					}

					OnShowDialogResponse (new DialogEventArgs (dialogOptions)); //call the event to show the message

				} else { 
					//found only one option then move on

					loadCurrentDialog(currentDialog.NextDialog[0]);
				}

			} else 	{
				//did not found any option
				throw new Exception ("The dialogue cycle has been broken : Current Dialog id = '"+currentDialog.Id+"'");
			}
		}

		public void selectOption(int opt){
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
					throw new Exception ("A dialog option should have only one dialog redirect : Option Dialog id = '" + dialogOptions [opt].Id + "'");
					return;
				}

				loadCurrentDialog (dialogOptions [opt].NextDialog[0]);
			}
		}

		void loadCurrentDialog(string id){
			//change a current dialog to a new dialog

			dialogOptions = null;
			Dialog nxtDialog = findMyObject (mainDialoguesList, id) as Dialog;
			currentDialog = nxtDialog;
			OnShowDialogResponse (new DialogEventArgs (currentDialog)); //call the event to show the message

		}

		public void initializeDialog(){
			//initialize a currentDialog with the first item of the mainDialogList


			if (mainDialoguesList != null && mainDialoguesList.Count > 0) {
				currentDialog = mainDialoguesList [0];

				OnShowDialogResponse (new DialogEventArgs (currentDialog)); //call the event to show the message

			} else {
				throw new Exception ("The Dialog List is Empty");
			}
		}

		protected virtual void OnShowDialogResponse(DialogEventArgs e)
		{
			//show the message to the view

			if (showDialogResponse != null)
				showDialogResponse (this, e);
		}

	}
}