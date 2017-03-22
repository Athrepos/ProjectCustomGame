using NatanielSoaresRodrigues.ProjectCustomGame.Views;
using System;
using UnityEngine;


namespace NatanielSoaresRodrigues.ProjectCustomGame.State
{
	public class ShowSceneState : StateGame
	{
		public override void execute (MainView main)
		{
			Texture texture = main.scenesController.loadTexture();
				
			if(texture != null)
			{
				main.backgroundImage.SetActive(true);
				main.backgroundImage.GetComponent<Renderer> ().material.mainTexture = texture;
			}
			OnFinishState (new EventArgs());
		}

		public override void nextState (MainView main)
		{
			main.currentState = new CharacterEnteredState ();
		}
	}
}

