using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using EngineContracts.Interfaces;


namespace Engine
{
	public class SceneFactory : ISceneFactory
	{
        private class SceneInfo
        {
            public IScene Scene;
            public ISceneTemplate Template;
            public bool IsGenerated;
        }

	    private GridGenerator _generator;
        private Dictionary<string, SceneInfo> Scenes;
	    public SceneFactory()
        {
            _generator = new GridGenerator();
        }

		public IScene GetScene(String ID)
		{
		    var scene = Scenes[ID];
            if (!scene.IsGenerated)
            {
                scene.Scene = Generate(scene.Template);
                scene.IsGenerated = true;
            }
            else
            {
                UpdateScene(scene.Scene, scene.Template);
            }
			//if (ID == "Default")
			//	scene.AddNextScene("Home", m => (m as IStage).Map.GetActorCoordinates("Player")._x == 1);
			return scene.Scene;
		}

	    public IScene Generate(ISceneTemplate template)
	    {
	        IScene scene = new Scene();

	        ((IStage) scene).Map = _generator.Generate(template.GetRules().Where(r => r is MapRule).Select(s=>s as MapRule).ToArray());
            
	        return scene;
	    }

	    public void UpdateScene(IScene scene, ISceneTemplate template)
	    {
	        throw new NotImplementedException();
	    }
	}
}

