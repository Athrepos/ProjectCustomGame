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

using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Views;
using UnityEngine;

namespace NatanielSoaresRodrigues.ProjectCustomGame.State
{
	public class StartGameState : StateGame
	{
		public override void execute(MainView main){
			Debug.Log ("Started the state");
			//disable dialog
			if(main.isDialog)
				main.typewriterScript.cleanText();

			//disable buttons
			main.canSelect = false;
			main.buttons.SetActive (false);
			foreach (var btn in main.optionButtons) {
				btn.gameObject.SetActive(false);
			}

			//disable character
			if (!main.SameCharacter) {
				main.characterNameText.SetActive (false);

				if (main.HasPreviousCharacter) {
					main.trigger.finishAnim += finishCharacterAnimation;

					main.characterImage.GetComponent<Animator> ().Play("characterAnimationExit");
				}
				else
					OnFinishState (new EventArgs());
			}
			else
				OnFinishState (new EventArgs());

		}

		void finishCharacterAnimation(object sender, EventArgs e){
			//the animation finish

			OnFinishState (new EventArgs ());
		}

		public override void nextState (MainView main){
			main.trigger.finishAnim -= finishCharacterAnimation;
			main.currentState = new CharacterEnteredState ();
		}

	}
}

