﻿using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Engine;
using EngineContracts.Interfaces;
using Game.Rules;

namespace Game
{
    public class SceneFactory : ISceneFactory
    {
        private readonly IActorFactory _actorFactory;

        private class SceneInfo
        {
            public IScene Scene;
            public ISceneTemplate Template;
            public bool IsGenerated;
            public bool CanRun;
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
                    currentSceneInfo = new SceneInfo { Scene = new Scene(), Template = new SceneTemplate() };
                    continue;
                }
                var pair = line.Split(':');
                MapRule rule;
                switch (pair[0].ToUpper())
                {
                    case "SIZE":
                        currentSceneInfo.Template.AddRule(new SizeRule(pair[1]));
                        break;
                    case "BORDERS":
                        currentSceneInfo.Template.AddRule(new BorderWallsRule());
                        break;
                    case "STARTPOINT":
                        currentSceneInfo.Template.AddRule(new StartPointRule(pair[1]));
                        break;
                    case "ENDPOINT":
                        currentSceneInfo.Template.AddRule(new EndPointRule(pair[1]));
                        break;
                    case "ROOM":
                        rule = new RoomWallRule();
                        rule.LoadParameters(pair[1]);
                        currentSceneInfo.Template.AddRule(rule);
                        break;
                    case "DUNGEON":
                        rule = new DungeonRule();
                        rule.LoadParameters(pair[1]);
                        currentSceneInfo.Template.AddRule(rule);
                        break;
                }

            }
        }

        public IScene GetScene(String ID, string previousSceneId)
        {
            var scene = Scenes[ID];
            if (!scene.IsGenerated)
            {
                scene.Scene = Generate(scene.Template);
                scene.IsGenerated = true;
            }

            PopulateScene(scene.Scene, scene.Template, previousSceneId);

            return scene.Scene;
        }

        public IScene Generate(ISceneTemplate template)
        {
            IScene scene = new Scene();

            ((IStage)scene).Map = _generator.Generate(template.GetRules().Where(r => r is MapRule).Select(s => s as MapRule).ToArray());

            return scene;
        }

        public void PopulateScene(IScene scene, ISceneTemplate template, string previousStage)
        {
            var startingPoints = (scene as IStage).Map.GetCells(x => x.Specials.Any(m => m is StartPoint), y => y.Specials.Where(m => m is StartPoint).FirstOrDefault().Description);
            Vector startingPoint;
            try
            {
                startingPoint = startingPoints[previousStage];
            }
            catch
            {
                Console.WriteLine("Didn't find starting point for {0}, using default", previousStage);
                startingPoint = startingPoints["*"];
            }
            var player = _actorFactory.GetPlayer();
            scene.AddActor(player);
            (scene as IStage).PlaceActorToGrid(player, startingPoint);

            var endPoints = (scene as IStage).Map.GetCells(x => x.Specials.Any(m => m is EndPoint), y => y.Specials.Where(m => m is EndPoint).FirstOrDefault().Description);
            foreach (var endPoint in endPoints)
            {
                var point = endPoint;
                scene.AddNextScene(endPoint.Key, s => ((IStage) s).Map.GetActorCoordinates(player).Equals(point.Value));
            }
        }
    }

    public class EndPoint : ICellSpecial
    {
        public string Description
        {
            get;
            private set;
        }

        public EndPoint(string s)
        {
            Description = s;
        }
    }

    public interface IActorFactory
    {
        IPlacableActor GetPlayer();
        IPlacableActor GetActor();
    }

    public class StartPoint : ICellSpecial
    {
        public string Description
        {
            get;
            private set;
        }

        public StartPoint(string s)
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
