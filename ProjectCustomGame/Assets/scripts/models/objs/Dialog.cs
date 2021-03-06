﻿/*
   The MIT License (MIT)

Copyright (c) 2017 Nataniel Soares Rodrigues

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Objs
{
	public class Dialog : MyObject
	{

		public string Message {get; set;}

		private string[] nextDialog;

		[XmlArray("NextDialog")]
		[XmlArrayItem("value")]
		public string[] NextDialog 
		{ 
			get
			{ 
				return nextDialog;
			} 
			set
			{ 
				nextDialog = value;
			}
		}

		public string IdCharacter {
			get;
			set;
		}

		public string IdScene {
			get;
			set;
		}

		public string Impact {
			get;
			set;
		}
//
//		public override bool Equals (Object obj)
//		{
//			Dialog aux = obj as Dialog;
//			if (aux.Id == this.Id)
//				return true;
//			else
//				return false;
//		}
//
	}

}