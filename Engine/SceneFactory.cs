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

	    private readonly GridGenerator _generator;
        private Dictionary<string, SceneInfo> Scenes;
	    public SceneFactory()
        {
            _generator = new GridGenerator();
            Scenes = new Dictionary<string, SceneInfo>();
	        ParseScenes(Scenes);
        }

	    private void ParseScenes(Dictionary<string, SceneInfo> scenes)
	    {
            String currentSceneName = null;
            SceneInfo currentSceneInfo = null;
	        var lines = System.IO.File.ReadAllLines(FileSystemHelper.PathToResources + "Levels" + FileSystemHelper.FileSystemSeparator +
	                                    "Levels.txt");
	        foreach (var line in lines)
	        {
	            if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (!String.IsNullOrEmpty(currentSceneName))
                    {
                        scenes.Add(currentSceneName, currentSceneInfo);
                    }
                    currentSceneName = line.Substring(1, line.Length - 2);
                    currentSceneInfo = new SceneInfo {Scene = new Scene(), Template = new SceneTemplate()};
                    continue;
                }
	            var pair = line.Split(':');
	            switch (pair[0].ToUpper())
	            {
                    case "Size":
	                    currentSceneInfo.Template.AddRule(new SizeRule(pair[1]));
                        break;
                    case "Borders":
                        currentSceneInfo.Template.AddRule(new BorderWalls());
                        break;
	                    
	            }

	        }
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

    internal class SceneTemplate : ISceneTemplate
    {
        public List<IRule> GetRules()
        {
            return Rules;
        }

        public void AddRule(IRule rule)
        {
            Rules.Add(rule);
        }

        protected List<IRule> Rules;
    
    }
}

