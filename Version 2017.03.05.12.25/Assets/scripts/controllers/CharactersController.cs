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

using System.Collections.Generic;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using NatanielSoaresRodrigues.ProjectCustomGame.Xml;
using System.IO;
using UnityEngine;
using System;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers
{
	public class CharactersController : MyObjectController
	{
		public Character CurrentCharacter {
			get{ 
				return current as Character;
			}
			private set{ }
		}


		public CharactersController() : base()
		{
			CharacterContainer container =  new XmlManagement ("characters",typeof(CharacterContainer)).openFile () as CharacterContainer;
			mainList = container.characters.ToArray ();

		}

		public override void selectCurrent (string id)
		{
			base.selectCurrent (id);
		}


		protected override void mainListOk ()
		{
			throw new System.NotImplementedException ();
		}

		public Sprite loadImage(){
			if (CurrentCharacter.CharacterImage == null)
				return null;

			Sprite spriteImage = Resources.Load<Sprite> ("images/" + CurrentCharacter.CharacterImage);

			return spriteImage;

		}


	}
}

