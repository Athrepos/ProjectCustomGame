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

namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers
{
	public class MyObjectController
	{
		public MyObjectController ()
		{
			
		}

		protected MyObject findMyObject<T>(List<T> myObjectList, string id) where T : MyObject
		{
			//find a object in a list
			MyObject o = myObjectList.Find (x => x.Id == id);
			if(o == null)
				throw new Exception("The Object with id : '" + id + "' is not found ");
			return o;
		}
	}
}

