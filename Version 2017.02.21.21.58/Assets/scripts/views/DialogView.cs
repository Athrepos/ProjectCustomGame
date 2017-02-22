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
using System.Collections;
using NatanielSoaresRodrigues.ProjectCustomGame.Controllers;
using System;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Views
{
	public class DialogView : MonoBehaviour {

		DialogController dialogController;


		// Use this for initialization
		void Start () {
			dialogController = new DialogController ();
			dialogController.showDialogResponse += c_showDialogResponse;
		}

		static void c_showDialogResponse(object sender, EventArgs e){
			Debug.Log ("teste");
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
