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
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers.State
{
	public class ShowSceneState : StateGame
	{
		public override void execute (MainController main)
		{
			var scene = main.sceneManager.CurrentScene;

			if (scene == null) {
				//none character selected
				OnFinishState (new EventArgs());
				return;			
			}

			if (main.sceneManager.IsSameObject) {
				OnFinishState (new EventArgs());
				return;	
			}

			Texture texture = main.sceneManager.loadTexture();
				
			if (texture != null) {
				//	main.backgroundImage.SetActive(true);
				main.backgroundImage.GetComponent<Renderer> ().material.mainTexture = texture;
				main.backgroundImage.GetComponent<AnimationEventTrigger> ().finishAnim += finishSceneAnimation;
				main.backgroundImage.GetComponent<Animator> ().enabled = true;
				main.backgroundImage.GetComponent<Animator> ().Play ("sceneAnimationIn");
			} else {
				OnFinishState (new EventArgs ());
			}
		

		}

		void finishSceneAnimation(object sender, EventArgs e){
			//the animation finish
			OnFinishState (new EventArgs ());
		}

		public override void nextState (MainController main)
		{
			main.backgroundImage.GetComponent<AnimationEventTrigger>().finishAnim -= finishSceneAnimation; //you do not want to remove this line, trust me or errors will pop up
			main.currentState = new CharacterEnteredState ();
		}
	}
}

