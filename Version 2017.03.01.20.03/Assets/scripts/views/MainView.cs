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
using NatanielSoaresRodrigues.ProjectCustomGame.Controllers;
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;
using UnityEngine.UI;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using System;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Views
{
	public class MainView : MonoBehaviour {

		private DialogController dialogController;
		private CharactersController charactersController;

		private TypewriterScript typewriterScript;

		public GameObject buttons;
		private Button[] optionButtons;

		public ClickUIObject dialogBoxClick;


		// Use this for initialization
		void Start () {


			typewriterScript = FindObjectOfType<TypewriterScript> ();
			optionButtons = buttons.GetComponentsInChildren<Button> ();
			dialogBoxClick.clickUI += c_clickDialog;

			charactersController = new CharactersController ();
			dialogController = new DialogController (c_showDialogResponse);


		}

		void disableButtons(){
			buttons.SetActive (false);
			foreach (var btn in optionButtons) {
				btn.gameObject.SetActive(false);
			}
		}

		void c_showDialogResponse(object sender, DialogEventArgs e){
			
			if (e.Dialogues.Length == 1) {
				// show a single message
				disableButtons ();

				Dialog d = e.Dialogues [0];
				string message;
				if (d.IdCharacter != null) {
					//show the character name

					charactersController.selectCurrent (d.IdCharacter);
					message = "\t" + charactersController.CurrentCharacter.Name + ":\n\t" + d.Message;
					Debug.Log (charactersController.CurrentCharacter.Name);

				} else
					message = "\t"+d.Message;

				typewriterScript.showMessage(message);
				Debug.Log (d.Message);
			}
			else if (e.Dialogues.Length > 1) { 
				//show the options to the user
				buttons.SetActive (true);
				int i = 0;
				foreach (var dialog in e.Dialogues) {
					Button btn = optionButtons [i];
					btn.gameObject.SetActive (true);

					Text txtBtn = btn.GetComponentInChildren<Text> ();
					txtBtn.text = "\t"+dialog.Message;

					Debug.Log (dialog.Message);
					i++;
				}
			}
		}

		void c_clickDialog(object sender, EventArgs e){
			checkDialog ();
		}

		public void selectOption(int option)
		{
			try {
				dialogController.selectOption (option);
			} catch (Exception ex) {
				Debug.Log (ex.Message);
			}
		}

		void checkDialog()
		{
			try {
				dialogController.checkDialog ();
			} catch (Exception ex) {
				Debug.Log (ex.Message);
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyUp (KeyCode.Space) ||
				Input.GetKeyUp(KeyCode.Return)
			) {
				
				checkDialog ();
			}

			if (Input.GetKeyUp (KeyCode.Alpha1)) {
				selectOption (0);

			}

			if (Input.GetKeyUp (KeyCode.Alpha2)) {
				selectOption (1);
			}
		}
	}
}
