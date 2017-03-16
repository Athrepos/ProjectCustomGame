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
using UnityEngine.UI;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Utils
{
	public class TypewriterScript : MonoBehaviour {

		private float delay = 0.025f;
		private string fullText;
		private string currentText = "";
		Text textBox;


		public void showMessage(string message)
		{
			
			this.fullText = message;
			StartCoroutine (ShowText());
		}

		public void showAllMessage()
		{
			StopCoroutine (ShowText ());
			textBox.text = fullText;

		}

		public void cleanText(){
			textBox = this.GetComponent<Text> ();
			textBox.text = "";
		}

		IEnumerator ShowText(){
			for (int i = 0; i < fullText.Length+1; i++) {
				currentText = fullText.Substring (0, i);

				textBox.text = currentText;

				yield return new WaitForSeconds (delay);
			}
		}
	}

}