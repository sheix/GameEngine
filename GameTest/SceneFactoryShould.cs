using NUnit.Framework;
using System;
using Game;
using Moq;

namespace GameTest
{
	[TestFixture()]
	public class SceneFactoryShould
	{
		Mock<IActorFactory> ActorFactory = new Mock<IActorFactory>();

		[Test()]
		public void GenerateNewScene ()
		{
			SceneFactory factory = new SceneFactory (ActorFactory.Object);

			var scene = factory.Generate (new SceneTemplate());

			Assert.NotNull (scene);
		}
	}
}

