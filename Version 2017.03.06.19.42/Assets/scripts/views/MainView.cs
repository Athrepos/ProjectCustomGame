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
		public Image characterImage;

		public GameObject characterNameText;

		// Use this for initialization
		void Start () {


			typewriterScript = FindObjectOfType<TypewriterScript> ();
			optionButtons = buttons.GetComponentsInChildren<Button> ();
			dialogBoxClick.clickUI += c_clickDialog;

			charactersController = new CharactersController ();
			dialogController = new DialogController (c_showDialogResponse);


		}

		void disableComponents(){
			buttons.SetActive (false);
			foreach (var btn in optionButtons) {
				btn.gameObject.SetActive(false);
			}
		}

		void c_showDialogResponse(object sender, DialogEventArgs e){
			
			if (e.Dialogues.Length == 1) {
				// show a single message
				Dialog d = e.Dialogues [0];
				showMessage (d);
				showCharacter (d.IdCharacter);

			}
			else if (e.Dialogues.Length > 1) { 
				//show the options to the user
				showOptions (e.Dialogues);
			}
		}

		void showOptions(Dialog[] dialogues){
			buttons.SetActive (true);
			int i = 0;
			foreach (var dialog in dialogues) {
				Button btn = optionButtons [i];
				btn.gameObject.SetActive (true);

				Text txtBtn = btn.GetComponentInChildren<Text> ();
				txtBtn.text = "\t"+dialog.Message;

				Debug.Log (dialog.Message);
				i++;
			}
		}

		void showMessage(Dialog d){
			disableComponents ();
			string message = d.Message;
		
			//message = "\t" + charactersController.CurrentCharacter.Name + ":\n\t" + d.Message;

			message = "\t" + d.Message;

			typewriterScript.showMessage(message);
			Debug.Log (d.Message);
		}

		void showCharacter(string idChar){
			if (idChar == null) {
				characterImage.GetComponent<Animator> ().Play ("characterAnimationExit");
				characterNameText.SetActive (false);
				charactersController.clearCurrent();
				characterNameText.GetComponentInParent<GameObject> ().SetActive (false);

				//characterImage.gameObject.SetActive (false);
				return;			
			}

			if (charactersController.CurrentCharacter != null &&
				idChar == charactersController.CurrentCharacter.Id)
				return;

			//verificar se ja existia um personagem aberto antes

			//characterImage.gameObject.SetActive (true);
			charactersController.selectCurrent (idChar);
			characterNameText.SetActive (true);
			characterNameText.GetComponentInChildren<Text>().text = charactersController.CurrentCharacter.Name;
			characterImage.overrideSprite = charactersController.loadImage();

			characterImage.GetComponent<Animator> ().Play ("characterAnimation");
		
			Debug.Log (charactersController.CurrentCharacter.Name);
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

		}
	}
}
