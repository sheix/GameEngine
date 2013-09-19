using System;
using CursesTest.Acts;
using CursesTest.OfficeRatScene;
using Engine;
using Engine.Interfaces;
using Moq;
using NUnit.Framework;

namespace OfficeRatTest
{
    [TestFixture]
    class MoveActShould
    {
        private Mock<IOfficeRatScene> sceneMock;
        private IGrid grid;
        private Mock<IActor> anotherActor;
        private Mock<IActor> thisActor;
            
        [SetUp]
        public void SetUp()
        {
            sceneMock = new Mock<IOfficeRatScene>();
            grid = new Grid(20,20);
            thisActor = new Mock<IActor>();
            grid.Grid[3][11].Actor = thisActor.Object;
            sceneMock.Setup(m => m.Grid1).Returns(grid);
        }


        [Test]
        public void ReturnCanDoWhenNoOtherActorInItsWay()
        {
            var act = new MoveAct(new Vector(1, 1), new ConsoleKeyInfo());
            act.Self = thisActor.Object;
            var result = act.CanDo(sceneMock.Object);

            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnCantDoWhenNoOtherActorInItsWay()
        {
            anotherActor = new Mock<IActor>();
            grid.Grid[4][12].Actor = anotherActor.Object;

            var act = new MoveAct(new Vector(1, 1), new ConsoleKeyInfo());
            act.Self = thisActor.Object;
            
			((Grid)grid).Render();

            var result = act.CanDo(sceneMock.Object);


            Assert.IsFalse(result);
        }

		//[Test]
		//public void ReturnTrueIfMockIsInterfaceImplementation()
		//{
		//	Assert.IsTrue(sceneMock is IOfficeRatScene);
		//}
    }
}
