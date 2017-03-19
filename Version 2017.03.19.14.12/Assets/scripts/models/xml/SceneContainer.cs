using System.Xml.Serialization;
using System.Collections.Generic;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Xml
{
	[XmlRoot("ScenesCollection")]
	public class SceneContainer
	{
		[XmlArray("Scenes")]
		[XmlArrayItem("Scene")]
		public List<Scene> scenes = new List<Scene>();
	}
}

