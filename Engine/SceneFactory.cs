using System;
using Contracts;
using EngineContracts.Interfaces;


namespace Engine
{
	public class SceneFactory : ISceneFactory
	{
		public IScene GetScene(String ID)
		{
			var scene = new Scene(ID);
			var generator = new GridGenerator();
			scene.Map = generator.Generate(new GridRule[]{new SizeRule(20,20),new BorderWalls()});
			scene.SetEmptyNextScene();
			if (ID == "Default")
				scene.AddNextScene("Home", m => (m as IStage).Map.GetActorCoordinates("Player")._x == 1);
			return scene;
		}
	}
}

