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

		bool canSelect;
		bool waitAnim;
		string idChar;
		Dialog dialog;

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
				canSelect = false;
				// show a single message
				dialog = e.Dialogues [0];

				idChar = dialog.IdCharacter;
				showCharacter ();

			}
			else if (e.Dialogues.Length > 1) { 
				//show the options to the user
				showOptions (e.Dialogues);
			}
		}

		void showOptions(Dialog[] dialogues){
			buttons.SetActive (true);
			int i = 0;
			foreach (var d in dialogues) {
				Button btn = optionButtons [i];
				btn.gameObject.SetActive (true);

				Text txtBtn = btn.GetComponentInChildren<Text> ();
				txtBtn.text = "\t"+d.Message;

				Debug.Log (d.Message);
				i++;
			}
			canSelect = true;
		}

		void showMessage(){
			disableComponents ();
			string message = dialog.Message;
		
			//message = "\t" + charactersController.CurrentCharacter.Name + ":\n\t" + d.Message;

			message = "\t" + dialog.Message;

			typewriterScript.showMessage(message);

			canSelect = true;
			Debug.Log (dialog.Message);
		}

		void showCharacter(){

			AnimationEventTrigger trigger = FindObjectOfType<AnimationEventTrigger> ();
			trigger.finishAnim += finishCharacterAnimation;
			Animator anim = characterImage.GetComponent<Animator> ();

			if (idChar == null) {
				//no character selected

				waitAnim = false;
				anim.Play("characterAnimationExit");

				characterNameText.SetActive (false);
				charactersController.clearCurrent();
				showMessage ();
				return;			
			}

			if (charactersController.CurrentCharacter != null &&
			    idChar == charactersController.CurrentCharacter.Id) {
				//same selected character 
				showMessage ();
				return;
			}

			if (charactersController.CurrentCharacter == null) {
				//new character to select and no previous character
				waitAnim = false;
				selectChar();

				return;
			}

			//new character to select and overrite previous character
			waitAnim = true;
			anim.Play("characterAnimationExit");
		
			Debug.Log (charactersController.CurrentCharacter.Name);
		}
		void selectChar(){
			Animator anim = characterImage.GetComponent<Animator> ();
			charactersController.selectCurrent (idChar);
			characterNameText.SetActive (true);
			characterNameText.GetComponentInChildren<Text>().text = charactersController.CurrentCharacter.Name;
			characterImage.overrideSprite = charactersController.loadImage();

			showMessage ();

			waitAnim = false;
			anim.Play ("characterAnimation");


		}

		void finishCharacterAnimation(object sender, EventArgs e){

			if (waitAnim)
				selectChar();

		}

		public void testEvent(int i){
			Debug.Log ("Animação terminou");
		}


		void c_clickDialog(object sender, EventArgs e){
			checkDialog ();
		}

		public void selectOption(int option)
		{
			if (!canSelect)
				return;
			try {
				dialogController.selectOption (option);
			} catch (Exception ex) {
				Debug.Log (ex.Message);
			}
		}

		void checkDialog()
		{
			if (!canSelect)
				return;
			
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
