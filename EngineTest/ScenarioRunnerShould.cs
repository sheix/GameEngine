using System;
using NUnit;
using Contracts;
using NUnit.Framework;
using Engine;
using Moq;

namespace EngineTest
{
	[TestFixture]
	public class ScenarioRunnerShould
	{
		ScenarioRunner scenarioRunner;
		Mock<IScenarioLoader> _scenarioLoader;
		Mock<IScene> _defaultScene;
		Mock<IScene> _secondScene;

		[TestFixtureSetUp]
		public void Setup()
		{
			_scenarioLoader = new Mock<IScenarioLoader>();
			_secondScene = new Mock<IScene>();
			_defaultScene = new Mock<IScene>();
			scenarioRunner = new ScenarioRunner(_scenarioLoader.Object);
		}

		[Test]
		public void CallScenarioLoaderToLoadAllScenarios()
		{
			scenarioRunner.LoadScenario();

			_scenarioLoader.Verify(m => m.Load(It.IsAny<string>()));
		}

		[Test]
		public void OnRunShouldRunFirstScene()
		{
			_scenarioLoader.Setup(m=>m.Load(It.IsAny<string>())).Returns(new System.Collections.Generic.Dictionary<string, IScene>()
				{ 
					{"SecondScene", _secondScene.Object},
					{"Default", _defaultScene.Object} 
				}
			 );

			scenarioRunner.Run();

			_defaultScene.Verify(m => m.Play());
		}
	}
}

