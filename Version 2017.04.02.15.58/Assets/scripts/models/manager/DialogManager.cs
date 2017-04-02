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


namespace NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager
{
	public delegate void ShowDialogResponseHandler(object source, DialogEventArgs e);
	public class DialogManager : MyObjectManager
	{

		public Dialog CurrentDialog {
			get{ 
				return current as Dialog;
			}
			private set{ }
		}

		public bool IsDialog {
			get;
			private set;
		}


		event ShowDialogResponseHandler showDialogResponse;

		public Dialog[] DialogOptions { get; private set;}


		public DialogManager(ShowDialogResponseHandler showDialogResponse) : base()
		{

			DialogContainer container = new XmlManagement("dialogues", typeof(DialogContainer)).openFile () as DialogContainer; //read the xml
			mainList = container.dialogues.ToArray(); 

			this.showDialogResponse = showDialogResponse; //set the event listner


		}


		public void checkDialog()
		{
			Dialog currentDialog = current as Dialog;

			if (currentDialog.NextDialog == null) //next dialog is not found
				return;
			
			int numbersOfD = currentDialog.NextDialog.Length; //number of options for the player choose
			if (numbersOfD > 0) {
				// found more than zero option

				if (numbersOfD > 1) {
					//found more than one option

					DialogOptions = new Dialog[numbersOfD];

					int i = 0;
					foreach (string dialogId in currentDialog.NextDialog) {
						Dialog nxtDialog = findMyObject (dialogId) as Dialog;
						DialogOptions [i] = nxtDialog;

						i++;
					}

					OnShowDialogResponse (new DialogEventArgs (DialogOptions)); //call the event to show the message

				} else { 
					//found only one option then move on

					selectCurrent(currentDialog.NextDialog[0]);
				}

			} else 	{
				//did not found any option
				throw new Exception ("The dialogue cycle has been broken : Current Dialog id = '"+current.Id+"'");
			}
		}

		public void selectOption(int opt){
			//select a option that the player choose

			if (DialogOptions == null) {
				//if have no options move on

				checkDialog ();
			}
			else if((opt+1) > DialogOptions.Length)	{
				//if selected a option that doesn't exist

				return;
			}
			else {
				//selected a valid option

				int nOfresponseOpt = DialogOptions [opt].NextDialog.Length; //number of option redirects

				if (nOfresponseOpt > 1 || nOfresponseOpt == 0) {
					// the option must be have just a one option to redirect
					throw new Exception ("A dialog option should have only one dialog redirect : Option Dialog id = '" + DialogOptions [opt].Id + "'");

				}

				selectCurrent (DialogOptions [opt].NextDialog[0]);
			}
		}
			
		public override void selectCurrent (string id)
		{
			base.selectCurrent (id);
			DialogOptions = null;
			OnShowDialogResponse (new DialogEventArgs (current as Dialog)); //call the event to show the message
		}


		protected override void mainListOk ()
		{
			OnShowDialogResponse (new DialogEventArgs (current as Dialog)); //call the event to show the message
		}

		protected virtual void OnShowDialogResponse(DialogEventArgs e)
		{
			if (e.Dialogues.Length == 1)
				IsDialog = true;
			else if (e.Dialogues.Length > 1)
				IsDialog = false;
				
			//show the message to the view

			if (showDialogResponse != null)
				showDialogResponse (this, e);
		}

	}
}