using System;
using System.Linq;
using Contracts;

namespace Engine
{
	public class SceneGenerator
	{
	    public IScene GenerateScene(String ID)
		{
			var scene = new Scene(ID);
			var generator = new GridGenerator();
			scene.Map = generator.Generate(new GridRule[]{new SizeRule(20,20),new BorderWalls()});
		    scene.SetEmptyNextScene();
            if (ID == "Default")
                scene.AddNextScene("Home", m => (m as IStage).Map.GetActorCoordinates("Player")._x == 2);
			return scene;
		}
	}
}

