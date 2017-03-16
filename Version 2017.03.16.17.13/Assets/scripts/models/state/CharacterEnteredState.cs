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

using NatanielSoaresRodrigues.ProjectCustomGame.Views;
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;
using UnityEngine;
using System;
using UnityEngine.UI;


namespace NatanielSoaresRodrigues.ProjectCustomGame.State
{
	public class CharacterEnteredState : StateGame
	{

		public override void execute(MainView main){
			
			var character = main.charactersController.CurrentCharacter;

			if (character == null) {
				//none character selected
				OnFinishState (new EventArgs());
				return;			
			}

			if (main.SameCharacter) {
				//same selected character 
				OnFinishState (new EventArgs());
				return;
			}

			//new character to select and overrite previous character
			main.characterImage.sprite = main.charactersController.loadImage();
			main.characterNameText.SetActive(true);
			main.characterNameText.GetComponentInChildren<Text>().text = character.Name;
			main.trigger.finishAnim += finishCharacterAnimation;
			main.characterImage.GetComponent<Animator> ().Play("characterAnimation");

			Debug.Log (character.Name);
		}

		void finishCharacterAnimation(object sender, EventArgs e){
			//the animation finish
			OnFinishState (new EventArgs());
		}

		public override void nextState(MainView main){

			main.currentState = new ShowMessageState ();
		}


	}
}

