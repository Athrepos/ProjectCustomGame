using NatanielSoaresRodrigues.ProjectCustomGame.Xml;
using UnityEngine;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager
{
	public class ScenesManager : MyObjectManager
	{
		public Scene CurrentScene {
			get{ 
				return current as Scene;
			}
			private set{ }
		}


		public ScenesManager() : base()
		{
			SceneContainer container =  new XmlManagement ("scenes", typeof(SceneContainer)).openFile () as SceneContainer;
			mainList = container.scenes.ToArray ();
		}

		public Texture loadTexture(){
			if (CurrentScene == null)
				return null;
			if (CurrentScene.SceneImage == null)
				return null;

			Texture textureImage = Resources.Load<Texture> ("images/" + CurrentScene.SceneImage);

			return textureImage;

		}

		protected override void mainListOk ()
		{
			throw new System.NotImplementedException ();
		}
	}
}

