using System;
using NUnit.Framework;
using Moq;
using Engine;
using Contracts;

namespace EngineTest
{
	[TestFixture()]
	public class SceneShould
	{
		Mock<IActor> actor;
		Scene scene;

		[SetUp()]
		public void Setup()
		{
			actor = new Mock<IActor>();
			scene = new Scene("TestId");
			scene.AddActor(actor.Object);
		}

		[Test()]
		public void OnTickDecreaseActorInitiative()
		{
			//arrange
			actor.Setup(m => m.GetInitiative()).Returns(11);
			//act
			scene.Tick();
			//assert
			actor.Verify(m => m.DecreaseInitiative());
		}

		[Test()]
		public void LetActAllActorsWithZeroInitiative()
		{
			//arrange
			actor.Setup(m => m.GetInitiative()).Returns(0);
			//act
			scene.Tick();
			//assert
			actor.Verify(m => m.Act(It.IsAny<IScene>()));
		}

		[Test()]
		public void NotLetActAllActorsWithZeroInitiative()
		{
			//arrange
			actor.Setup(m => m.GetInitiative()).Returns(98);
			//act
			scene.Tick();
			//assert
			actor.Verify(m => m.Act(It.IsAny<IScene>()),Times.Never());
		}

	}
}

