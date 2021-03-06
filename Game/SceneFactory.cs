﻿using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Contracts;
using Engine;
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
		}

		private readonly GridGenerator _generator;
		private Dictionary<string, SceneInfo> Scenes;

		public SceneFactory (IActorFactory actorFactory)
		{
			_actorFactory = actorFactory;
			_generator = new GridGenerator ();
			Scenes = new Dictionary<string, SceneInfo> ();
			ParseScenes (Scenes);
		}

		private void ParseScenes (Dictionary<string, SceneInfo> scenes)
		{
			String currentSceneName = null;
			SceneInfo currentSceneInfo = null;
			var lines = System.IO.File.ReadAllLines (FileSystemHelper.PathToResources + "Levels" + FileSystemHelper.FileSystemSeparator +
				"Levels.txt");
			foreach (var line in lines) {
				if (line.StartsWith ("[") && line.EndsWith ("]")) {
					if (!String.IsNullOrEmpty (currentSceneName)) {
						scenes.Add (currentSceneName.ToUpper(), currentSceneInfo);
					}
					currentSceneName = line.Substring (1, line.Length - 2);
					currentSceneInfo = new SceneInfo { Scene = new BaseScene(currentSceneName), Template = new SceneTemplate() };
					continue;
				}
				var pair = line.Split (':');
				MapRule rule = null;
				switch (pair [0].ToUpper ()) {
				case "SIZE":
					rule = new SizeRule ();
					break;
				case "BORDERS":
					rule = new BorderWallsRule ();
					break;
				case "STARTPOINT":
					rule = new StartPointRule ();
					break;
				case "ENDPOINT":
					rule = new EndPointRule ();
					break;
				case "ROOM":
					rule = new RoomWallRule ();
					break;
				case "DUNGEON":
					rule = new DungeonRule ();
					break;
				}
				if (rule == null)
					continue;
				rule.LoadParameters (pair [1]);
				currentSceneInfo.Template.AddRule (rule);
			}
			scenes.Add (currentSceneName.ToUpper(), currentSceneInfo);
		}

		public IScene GetScene (string ID, string previousSceneId)
		{
			Console.WriteLine ("Next scene: {0}, Previous: {1}", ID, previousSceneId);
			var scene = Scenes [ID];
			if (!scene.IsGenerated) {
				scene.Scene = Generate (scene.Template, ID);
				scene.IsGenerated = true;
			}

			PopulateScene (scene.Scene, scene.Template, previousSceneId);

			return scene.Scene;
		}

		public IScene Generate (ISceneTemplate template, String ID)
		{
			Console.WriteLine ("Generating scene:" + ID);
			var scene = new GameScene (ID);
			scene.SetMap (_generator.Generate (template.GetRules ().Where (r => r is MapRule).Select (s => s as MapRule).ToArray ()));
			return scene;
		}

		private void PopulateScene (IScene scene, ISceneTemplate template, string previousStage)
		{
			Console.WriteLine ("Populating scene:" + scene.Name);
			var gamescene = scene as GameScene;
			var startingPoints = gamescene.GetStartingPoints ();
			Vector startingPoint;
			try {
				startingPoint = startingPoints [previousStage];
			} catch {
				Console.WriteLine ("Didn't find starting point for {0}, using default", previousStage);
				startingPoint = startingPoints ["*"];
			}
            
			var player = _actorFactory.GetPlayer ();
			gamescene.RemoveActor (player);
			gamescene.AddActor (player);
			Console.WriteLine ("Added Player to scene");
			gamescene.PlaceActorToGrid (player, startingPoint);
			Console.WriteLine ("Placed Player to grid");

			var endPoints = gamescene.GetEndingPoints ();
			foreach (var endPoint in endPoints) {
				var point = endPoint;
				gamescene.AddNextScene (endPoint.Key, s => (s as GameScene).GetPlayerCoordinates ().Equals (point.Value));
			}
		}
	}

	public class EndPoint : ICellSpecial
	{
		public string Description {
			get;
			private set;
		}

		public EndPoint (string s)
		{
			Description = s;
		}
	}

	public interface IActorFactory
	{
		IPlacableActor GetPlayer ();

		IPlacableActor GetActor ();
	}

	public class StartPoint : ICellSpecial
	{
		public string Description {
			get;
			private set;
		}

		public StartPoint (string s)
		{
			Description = s;
		}
	}

	internal class SceneTemplate : ISceneTemplate
	{
		public List<IRule> GetRules ()
		{
			return Rules;
		}

		public void AddRule (IRule rule)
		{
			Rules.Add (rule);
		}

		protected List<IRule> Rules = new List<IRule> ();
	}
}

