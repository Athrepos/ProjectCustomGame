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
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Views
{
	public class DialogView : MonoBehaviour {

		DialogController dialogController;
		public TypewriterScript typewriterScript;


		// Use this for initialization
		void Start () {
			dialogController = new DialogController (c_showDialogResponse);
		}

		void c_showDialogResponse(object sender, DialogEventArgs e){
			if (e.dialogues.Length == 1) {
				// show a single message
				typewriterScript.showMessage(e.dialogues [0].message);
				Debug.Log (e.dialogues [0].message);
			}
			else if (e.dialogues.Length > 1) { 
				//show the options to the user
				string allM = "";
				foreach (var dialog in e.dialogues) {
					allM += dialog.message+"\n";
					Debug.Log (dialog.message);
				}
				typewriterScript.showMessage(allM);
			}
				
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp(KeyCode.Return)) {
				try {
					dialogController.checkDialog ();
				} catch (Exception ex) {
					Debug.Log (ex.Message);
				}

			}

			if (Input.GetKeyUp (KeyCode.Alpha1)) {
				try {
					dialogController.selectOption (0);
				} catch (Exception ex) {
					Debug.Log (ex.Message);
				}

			}

			if (Input.GetKeyUp (KeyCode.Alpha2)) {
				try {
					dialogController.selectOption (1);
				} catch (Exception ex) {
					Debug.Log (ex.Message);
				}

			}
		}
	}
}
