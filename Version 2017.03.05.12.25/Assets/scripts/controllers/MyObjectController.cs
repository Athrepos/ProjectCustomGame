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

using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using System.Collections.Generic;
using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Xml;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers
{
	public abstract class MyObjectController
	{
		protected MyObject current;
		protected MyObject[] mainList;

		public MyObjectController ()
		{
			
		}

		public virtual void selectCurrent(string id){
			current = findMyObject (id);
		}

		protected MyObject findMyObject(string id)
		{
			//find a object in a list
			List<MyObject> aux =  new List<MyObject>(mainList);

			MyObject o = aux.Find (x => x.Id == id);
			if(o == null)
				throw new Exception("The Object with id : '" + id + "' is not found ");
			return o;
		}

		protected void initialize(){
			//initialize  with the first item of the mainlist

			if (mainList != null && mainList.Length > 0) {
				current = mainList [0];

				mainListOk ();

			} else {
				throw new Exception ("The List is Empty");
			}
		}

		protected abstract void mainListOk ();

	}
}

