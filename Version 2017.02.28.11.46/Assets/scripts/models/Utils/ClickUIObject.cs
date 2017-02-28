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
using UnityEngine.EventSystems;
using NatanielSoaresRodrigues.ProjectCustomGame.Views;
using System;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Utils
{
	public delegate void clickUiHandler(object source, EventArgs e);
	public class ClickUIObject : MonoBehaviour, IPointerClickHandler {

		public event clickUiHandler clickUI;

		public void OnPointerClick(PointerEventData eventData) // 3
		{
			OnClickUI (new EventArgs ());
		}

		protected virtual void OnClickUI(EventArgs e)
		{
			//show the message to the view

			if (clickUI != null)
				clickUI (this, e);
		}
	}
}
