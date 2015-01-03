using NUnit.Framework;
using System;
using Game;

namespace GameTest
{
	[TestFixture()]
	public class SceneFactoryShould
	{
		[Test()]
		public void GenerateNewScene ()
		{
			SceneFactory factory = new SceneFactory ();

			var scene = factory.Generate (new SceneTemplate());

			Assert.NotNull (scene);
		}
	}
}

