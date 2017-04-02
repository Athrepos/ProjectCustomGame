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
using UnityEngine;
using NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager;
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers.State
{
	public class StartGameState : StateGame
	{

		bool playAnimationChar = false;
		bool playAnimationScene = false;
		bool lastFinsih = false;

		public override void execute(MainController main){
			Debug.Log ("Started the state");

		
			//disable dialog
			if(main.dialogManager.IsDialog)
				main.typewriterScript.cleanText();

			//disable buttons
			main.canSelect = false;
			main.buttons.SetActive (false);
			foreach (var btn in main.optionButtons) {
				btn.gameObject.SetActive(false);
			}

			//disable scene
			playAnimationScene = playAnimation(main.sceneManager);

			if (playAnimationScene) {
				main.backgroundImage.GetComponent<Animator> ().enabled = true;
				main.backgroundImage.GetComponent<AnimationEventTrigger> ().finishAnim += finishSceneAnimation;
				main.backgroundImage.GetComponent<Animator> ().Play ("sceneAnimationExit");
			} else {
				lastFinsih = true;
			}


			//disable character
			playAnimationChar = playAnimation(main.characterManager, main.characterNameText);

			if (playAnimationChar) {
				main.characterImage.GetComponent<Animator> ().enabled = true;
				main.characterImage.GetComponent<AnimationEventTrigger> ().finishAnim += finishCharacterAnimation;
				main.characterImage.GetComponent<Animator> ().Play ("characterAnimationExit");
			} else {
				lastFinsih = true;
			}

			if(!playAnimationChar && !playAnimationScene)
				OnFinishState (new EventArgs());

		}

		bool playAnimation(MyObjectManager manager)
		{
			return playAnimation (manager, null);
		}

		bool playAnimation(MyObjectManager manager, GameObject obj){

			//disable obj
			if (!manager.IsSameObject) {
				if(obj != null)
					obj.SetActive (false); //disable the object

				if (manager.HasPreviousObject) {
					return true; //play the animation
				} else
					return false;
			} else
				return false;
		}

		void finishCharacterAnimation(object sender, EventArgs e){
			//the char animation finish
			if (lastFinsih)
				OnFinishState (new EventArgs ());
			else
				lastFinsih = true;
		}

		void finishSceneAnimation(object sender, EventArgs e){
			//the scene animation finish
			if (lastFinsih)
				OnFinishState (new EventArgs ());
			else
				lastFinsih = true;
		}

		public override void nextState (MainController main){
			main.characterImage.GetComponent<AnimationEventTrigger>().finishAnim -= finishCharacterAnimation; //you do not want to remove this line, trust me or errors will pop up
			main.backgroundImage.GetComponent<AnimationEventTrigger>().finishAnim -= finishSceneAnimation; //you do not want to remove this line, trust me or errors will pop up
			main.currentState = new ShowSceneState ();
		}

	}
}

