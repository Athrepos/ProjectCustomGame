using NatanielSoaresRodrigues.ProjectCustomGame.Views;
using System;
using UnityEngine;


namespace NatanielSoaresRodrigues.ProjectCustomGame.State
{
	public class ShowSceneState : StateGame
	{
		public override void execute (MainView main)
		{
			main.backgroundImage.GetComponent<Renderer> ().material.mainTexture = main.scenesController.loadTexture();
			OnFinishState (new EventArgs());
		}

		public override void nextState (MainView main)
		{
			main.currentState = new CharacterEnteredState ();
		}
	}
}

