using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using Engine;
using Contracts;

namespace EngineTest
{
	[TestFixture]
 	public class ActorShould
    {
		Mock<IScene> scene;
		Actor actor;
		Mock<IStrategy> strategy;
	    private Mock<IAct> act;

		[SetUp]
		public void SetUp()
		{
            act = new Mock<IAct>();
			strategy = new Mock<IStrategy>();
			actor = new Actor("Actor", strategy.Object);
			scene = new Mock<IScene>();
			strategy.Setup(mn => mn.SelectAction(It.IsAny<List<IAct>>(),It.IsAny<IScene>())).Returns(act.Object);
			scene.Setup(mn => mn.GetPossibleActions(It.IsAny<IActor>())).Returns(new List<IAct>{act.Object});
		    act.Setup(m => m.Do(scene.Object)).Returns(10);
		}

		[Test]
		public void ActOnScene()
		{
			//arrange

			//act
			actor.Act(scene.Object);
			
			//assert
			act.Verify(m => m.Do(scene.Object));
		}

        [Test]
        public void DecreaseInitiativeByActAmount()
        {
            int initial = actor.GetInitiative();
            actor.Act(scene.Object);

            int ending = actor.GetInitiative();

            Assert.AreEqual(initial, ending-10);
        }

    }
}
