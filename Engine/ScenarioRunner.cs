using System;
using Contracts;
using System.Collections.Generic;

namespace Engine
{
	public class ScenarioRunner : IScenarioRunner
	{
		const string defaultSceneId = "Default";

		IScenarioLoader _scenarioLoader;
		Dictionary<string, IScene> scenario;

		public ScenarioRunner (IScenarioLoader scenarioLoader)
		{
			_scenarioLoader = scenarioLoader;
		}

		public void LoadScenario()
		{
			scenario = _scenarioLoader.Load();
		}

		public void Run(string sceneId = null)
		{
			LoadScenario();

			if (sceneId == null)
				scenario[defaultSceneId].Play();
			else
				scenario[sceneId].Play();

		}
	}
}

