using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using EngineContracts.Interfaces;


namespace Engine
{
	public class SceneFactory : ISceneFactory
	{
	    private readonly IActorFactory _actorFactory;

	    private class SceneInfo
        {
            public IScene Scene;
            public ISceneTemplate Template;
            public bool IsGenerated;
        }

	    private readonly GridGenerator _generator;
        private Dictionary<string, SceneInfo> Scenes;

	    public SceneFactory(IActorFactory actorFactory)
        {
	        _actorFactory = actorFactory;
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
                    case "SIZE":
	                    currentSceneInfo.Template.AddRule(new SizeRule(pair[1]));
                        break;
                    case "BORDERS":
                        currentSceneInfo.Template.AddRule(new BorderWalls());
                        break;
                    case "STARTPOINT":
	                    currentSceneInfo.Template.AddRule(new StartPointRule(pair[1]));
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

            PopulateScene(scene.Scene, scene.Template);
            
			//if (ID == "Default")
			//	scene.AddNextScene("Home", m => (m as IStage).Map.GetActorCoordinates("Player")._x == 1);
			return scene.Scene;
		}

	    public IScene Generate(ISceneTemplate template)
	    {
	        IScene scene = new Scene();

	        ((IStage) scene).Map = _generator.Generate(template.GetRules().Where(r => r is MapRule).Select(s=>s as MapRule).ToArray());
            
            //template.GetRules().Where()

	        return scene;
	    }

	    public void PopulateScene(IScene scene, ISceneTemplate template)
	    {
	        var startingPoints = (scene as IStage).Map.GetItemCoordinates(x => x is StartPointItem);

	    }
	}

    public interface IActorFactory
    {
        IPlacableActor GetPlayer();
        IPlacableActor GetActor();
    }

    internal class StartPointRule : MapRule
    {
        private StartPointItem startingPointItem;
        private Vector startingPointVector;
        public StartPointRule(string s)
        {
            var startPoint = s.Split('-');
            startingPointItem = new StartPointItem(startPoint[0]);
            startingPointVector = Vector.Parse(startPoint[1]);
        }

        public override void Process(Grid grid)
        {
            grid.At(startingPointVector).AddItem(startingPointItem);

        }
    }

    internal class StartPointItem : Item
    {
        public StartPointItem(string s) 
        {
            Description = s;
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

        protected List<IRule> Rules = new List<IRule>();
    
    }
}

