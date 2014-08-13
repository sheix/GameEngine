using System;
using Engine;
using Contracts;
using EngineContracts.Interfaces;

namespace LevelGeneratorTester
{
	class MainClass
	{
		private string FileName = "Levels.txt";
		public static void Main (string[] args)
		{
			ISceneFactory sceneGenerator = new SceneFactory ();
			Console.WriteLine ("Hello World!");
		}
	}
}
