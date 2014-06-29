using System;
using Contracts;
using EngineContracts.Interfaces;


namespace Engine
{
	public class SceneFactory : ISceneFactory
	{
	    private GridGenerator _generator;

        private SceneFactory()
        {
            _generator = new GridGenerator();
        }

		public IScene GetScene(String ID)
		{
			var scene = new Scene(ID);
			scene.Map = _generator.Generate(new GridRule[]{new SizeRule(20,20),new BorderWalls()});
			scene.SetEmptyNextScene();
			if (ID == "Default")
				scene.AddNextScene("Home", m => (m as IStage).Map.GetActorCoordinates("Player")._x == 1);
			return scene;
		}

	    public IScene Generate(ISceneTemplate template)
	    {
	        throw new NotImplementedException();
	    }

	    public void UpdateScene(IScene scene, ISceneTemplate template)
	    {
	        throw new NotImplementedException();
	    }
	}
}

