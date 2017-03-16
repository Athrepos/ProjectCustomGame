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

using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Views;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using UnityEngine;
using UnityEngine.UI;

namespace NatanielSoaresRodrigues.ProjectCustomGame.State
{
	public class ShowMessageState : StateGame
	{

		public override void execute(MainView main){
			if (main.isDialog) {
				//single dialog message

				Dialog dialog = main.dialogController.CurrentDialog;

				string message = dialog.Message;

				message = "\t" + dialog.Message;

				main.typewriterScript.showMessage(message);

				Debug.Log (dialog.Message);
			} else {

				//option dialog

				main.buttons.SetActive (true);
				int i = 0;
				Dialog[] dialogues = main.dialogController.DialogOptions;


				foreach (var d in dialogues) {
					Button btn = main.optionButtons [i];
					btn.gameObject.SetActive (true);

					Text txtBtn = btn.GetComponentInChildren<Text> ();
					txtBtn.text = "\t"+d.Message;

					Debug.Log (d.Message);
					i++;
				}
			}

			OnFinishState (new EventArgs());
		
		}


		public override void nextState(MainView main){

			main.currentState = new SelectActionState ();
		}

	}
}
