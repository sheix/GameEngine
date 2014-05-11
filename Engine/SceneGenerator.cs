using System;
using Contracts;

namespace Engine
{
	public class SceneGenerator
	{
		public SceneGenerator ()
		{

		}

		public IScene GenerateScene(String ID)
		{
			var scene = new Scene(ID);
			var generator = new GridGenerator();
			scene.Map = generator.Generate(new GridRule[]{new SizeRule(20,20),new BorderWalls()});
			return scene;
		}
	}
}

