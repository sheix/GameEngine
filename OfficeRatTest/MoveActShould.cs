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
        private Mock<IStage> sceneMock;
        private IGrid grid;
        private Mock<IPlacableActor> anotherActor;
        private Mock<IPlacableActor> thisActor;

        [SetUp]
        public void SetUp()
        {
            sceneMock = new Mock<IStage>();
            grid = new Grid(20, 20);
            thisActor = new Mock<IPlacableActor>();
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
            anotherActor = new Mock<IPlacableActor>();
            grid.Grid[4][12].Actor = anotherActor.Object;

            var act = new MoveAct(new Vector(1, 1), new ConsoleKeyInfo());
            act.Self = thisActor.Object;

            ((Grid)grid).Render();

            var result = act.CanDo(sceneMock.Object);


            Assert.IsFalse(result);
        }

        [Test]
        public void ActuallyMoveActorWhenDone()
        {
            var act = new MoveAct(new Vector(1, 1), new ConsoleKeyInfo());
            act.Self = thisActor.Object;

            act.Do(sceneMock.Object);

            sceneMock.Verify(mn => mn.RemoveActor(It.Is<IPlacableActor>(m => m.Equals(thisActor.Object))));
            sceneMock.Verify(mn => mn.PlaceActorToGrid(It.Is<IPlacableActor>(m => m.Equals(thisActor.Object)), It.IsAny<Vector>()));
        }
    }
}
