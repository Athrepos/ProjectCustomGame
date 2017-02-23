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
using System;
using System.Collections;
using NatanielSoaresRodrigues.ProjectCustomGame.Controllers;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Views
{
	public class DialogView : MonoBehaviour {

		DialogController dialogController;


		// Use this for initialization
		void Start () {
			dialogController = new DialogController (c_showDialogResponse);
		}

		void c_showDialogResponse(object sender, DialogEventArgs e){
			if (e.dialogues.Length == 1) {
				// show a single message

				Debug.Log (e.dialogues [0].message);
			}
			else if (e.dialogues.Length > 1) { 
				//show the options to the user

				foreach (var dialog in e.dialogues) {
					Debug.Log (dialog.message);
				}
			}
				
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp(KeyCode.Return)) {
				dialogController.checkDialog ();
			}

			if (Input.GetKeyUp (KeyCode.Alpha1)) {
				dialogController.selectOption (0);
			}

			if (Input.GetKeyUp (KeyCode.Alpha2)) {
				dialogController.selectOption (1);
			}
		}
	}
}
