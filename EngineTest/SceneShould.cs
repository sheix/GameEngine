using System;
using NUnit.Framework;
using Moq;
using Engine;
using Engine.Contracts;

namespace EngineTest
{
	[TestFixture()]
	public class SceneShould
	{
		Mock<IActor> actor;
		BaseScene scene;

		[SetUp]
		public void Setup()
		{
			actor = new Mock<IActor>();
			scene = new BaseScene("Default");
			scene.AddActor(actor.Object);
		}

		[Test]
		public void OnTickDecreaseActorInitiative()
		{
			//arrange
			actor.Setup(m => m.GetInitiative()).Returns(11);
			//act
			scene.Tick();
			//assert
			actor.Verify(m => m.DecreaseInitiative());
		}

		[Test]
		public void LetActAllActorsWithZeroInitiative()
		{
			//arrange
			actor.Setup(m => m.GetInitiative()).Returns(0);
			//act
			scene.Tick();
			//assert
			actor.Verify(m => m.Act(It.IsAny<IScene>()));
		}

		[Test]
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

