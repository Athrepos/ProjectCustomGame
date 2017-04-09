﻿/*
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
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;
using UnityEngine.UI;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager;
using NatanielSoaresRodrigues.ProjectCustomGame.Controllers.State;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers
{
	public class MainController : MonoBehaviour {

		public DialogManager dialogManager{ get; private set; }
		public CharacterManager characterManager{ get; private set; }
		public ScenesManager sceneManager { get; private set;}

		public TypewriterScript typewriterScript { get; private set;}

		public GameObject buttons;
		public Button[] optionButtons { get; private set;}

		public ClickUIObject dialogBoxClick;
		public Image characterImage;

		public GameObject characterNameText;
		public GameObject backgroundImage;


		public StateGame currentState;

		public bool canSelect;


		// Use this for initialization
		void Start () {

			canSelect = false;

			typewriterScript = FindObjectOfType<TypewriterScript> ();
			optionButtons = buttons.GetComponentsInChildren<Button> ();
			dialogBoxClick.clickUI += c_clickDialog;

			//constrollers
			sceneManager = new ScenesManager();
			characterManager = new CharacterManager ();
			dialogManager = new DialogManager (c_showDialogResponse);
			dialogManager.initialize (); //initialize the dialg system

		}

		void c_showDialogResponse(object sender, DialogEventArgs e){

			characterManager.selectCurrent (dialogManager.CurrentDialog.IdCharacter); //select Char
			sceneManager.selectCurrent (dialogManager.CurrentDialog.IdScene); //select scene

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

		void c_clickDialog(object sender, EventArgs e){
			//click to check the dialog system

			checkDialog ();
		}

		public void optionSelected(int option)
		{
			//click to check the dialog option

			if (!canSelect)
				return;
			try {
				dialogManager.selectOption (option);
			} catch (Exception ex) {
				Debug.Log (ex.Message);
			}
		}

		void checkDialog()
		{
			if (!canSelect)
				return;
			
			try {
				dialogManager.checkDialog ();
			} catch (Exception ex) {
				Debug.Log (ex.Message);
			}
		}
		
		// Update is called once per frame
		void Update () {

		}
	}
}