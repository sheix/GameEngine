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
	    private Mock<IAct> act;

		[SetUp()]
		public void SetUp()
		{
            act = new Mock<IAct>();
			strategy = new Mock<IStrategy>();
			actor = new Actor(strategy.Object);
			scene = new Mock<IScene>();
			strategy.Setup(mn => mn.SelectAction(It.IsAny<List<IAct>>(),It.IsAny<IScene>())).Returns(act.Object);
			scene.Setup(mn => mn.GetPossibleActions(It.IsAny<IActor>())).Returns(new List<IAct>{act.Object});
		}

		[Test()]
		public void ActOnScene()
		{
			//arrange

			//act
			actor.Act(scene.Object);
			
			//assert
			act.Verify(m => m.Do(scene.Object));
		}

    }
}
