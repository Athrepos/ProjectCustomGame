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
using NatanielSoaresRodrigues.ProjectCustomGame.State;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Views
{
	public class MainView : MonoBehaviour {

		public DialogController dialogController{ get; private set; }
		public CharactersController charactersController{ get; private set; }

		public TypewriterScript typewriterScript { get; private set;}

		public GameObject buttons;
		public Button[] optionButtons { get; private set;}

		public ClickUIObject dialogBoxClick;
		public Image characterImage;

		public GameObject characterNameText;

		public StateGame currentState;

		public bool SameCharacter {
			get;
			private set;
		}

		public bool isDialog {
			get;
			private set;
		}

		public bool HasPreviousCharacter {
			get;
			private set;
		}

		public bool canSelect;

		public AnimationEventTrigger trigger { get; private set;}

		// Use this for initialization
		void Start () {

			canSelect = false;


			trigger = FindObjectOfType<AnimationEventTrigger> ();
			typewriterScript = FindObjectOfType<TypewriterScript> ();
			optionButtons = buttons.GetComponentsInChildren<Button> ();
			dialogBoxClick.clickUI += c_clickDialog;

			charactersController = new CharactersController ();
			dialogController = new DialogController (c_showDialogResponse);
			dialogController.initialize ();

		}

		void c_showDialogResponse(object sender, DialogEventArgs e){

			if (e.Dialogues.Length == 1) {
				
				// show a single message
				selectChar (dialogController.CurrentDialog.IdCharacter);

				isDialog = true;
			}
			else if (e.Dialogues.Length > 1) { 
				//show the options to the user
				SameCharacter = true;
				isDialog = false;
			}


			//execute the state game
			currentState = new StartGameState ();
			currentState.finishState += finishState;
			currentState.execute(this);
		}

		void finishState(object sender, EventArgs e){
			currentState.nextState (this);
			if (currentState != null) {
				currentState.finishState += finishState;
				currentState.execute (this);
			}
		}

		void selectChar(string idChar)
		{
			if (charactersController.CurrentCharacter != null)
				HasPreviousCharacter = true;
			else
				HasPreviousCharacter = false;

			if (idChar == null) {
				charactersController.clearCurrent ();
				SameCharacter = false;
				return;
			} else if (charactersController.CurrentCharacter != null &&
			         idChar == charactersController.CurrentCharacter.Id) {
				SameCharacter = true;
				return;
			}
			SameCharacter = false;
			charactersController.selectCurrent (idChar);
		}

		void c_clickDialog(object sender, EventArgs e){
			checkDialog ();
		}

		public void optionSelected(int option)
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
