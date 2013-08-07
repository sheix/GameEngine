using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Engine;

namespace EngineTest
{
	[TestFixture()]
 	public class ActorShould
    {
		Mock<IScene> scene;
		Actor actor;
		Mock<IStrategy> strategy;
		[SetUp()]
		public void SetUp()
		{
			strategy = new Mock<IStrategy>();
			actor = new Actor(strategy.Object);
			scene = new Mock<IScene>();
			strategy.Setup(mn => mn.SelectAction(It.IsAny<List<IAct>>(),It.IsAny<IScene>())).Returns(new Act("Act 1",actor,null ));
			scene.Setup(mn => mn.GetPossibleActions(It.IsAny<IActor>())).Returns(new List<IAct>{new Act("Act 1",actor,null ),new Act("Act 2", actor,null)});
		}

		[Test()]
		public void GetPossibleActionsFromScene()
		{
			//arrange

			//act
			actor.Act(scene.Object);
			var actions = actor.AllActions;

			//assert
			Assert.AreNotEqual(0, actions.Count);
		}
    }
}
